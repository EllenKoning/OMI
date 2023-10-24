using System;
using System.Globalization;
using System.IO;

class Model
{
    Filer filer;
    Demographic demographic;
    Disease disease;
    Policies policies;

    ///start values population
    decimal population;
    decimal nr_Susceptible = 9999999;
    decimal nr_Infected = 1;
    decimal nr_Recovered = 0;
    decimal nr_Dead = 0;
    decimal deaths_Per_Day = 0;
    decimal carrier_Density = 0;
    decimal stochastic_Rate;
    decimal reproduction_Number;
    decimal[] stats
    {
        get { return new decimal[8] { nr_Susceptible, nr_Infected, nr_Recovered, nr_Dead, deaths_Per_Day, carrier_Density, reproduction_Number, stochastic_Rate }; }
    }

    public Model(Filer _filer, Demographic _demographic, Disease _disease, Policies _policies)
    {
        filer = _filer;
        demographic = _demographic;
        disease = _disease;
        policies = _policies;

    }

    /// <summary>
    /// Updates the model for n loops. Can save .csv files to storage.
    /// </summary>
    /// <param name="n">number of days</param>
    /// <param name="save">save raw data to csv file</param>
    public void run(int n, bool save = false)
    {
        for (int i = 0; i < n; i++)
        {
            if (save)
                filer.saveData(stats, i);
            calcStats();
            printStats(i);
        }
        Console.WriteLine(nr_Susceptible + nr_Infected + nr_Recovered + nr_Dead);
        Console.ReadLine();
    }

    void calcStats()
    {
        population = nr_Susceptible + nr_Infected + nr_Recovered;
        carrier_Density = decimal.Divide(nr_Infected, population);
        decimal lockdown_Factor = policies.Lockdown_Factor(carrier_Density);
        decimal isolation_Factor = policies.Isolation_Factor(carrier_Density);
        decimal adjusted_Carrier_Density = decimal.Divide((nr_Infected * isolation_Factor), (population - (nr_Infected - nr_Infected * isolation_Factor)));
        decimal not_Infection_Chance = (decimal)Math.Pow((double)(1 - (adjusted_Carrier_Density * demographic.std_Infection_Chance * disease.vacination_Factor * disease.hygiene_Factor)), (double)(demographic.total_Contact * lockdown_Factor)); //make not double pls
        decimal infection_Chance = 1 - not_Infection_Chance;
        stochastic_Rate = infection_Chance;
        
        //reproduction_Number = demographic.std_Infection_Chance * demographic.total_Contact * lockdown_Factor * (nr_Susceptible/population) * demographic.expected_Recovery_Time;


        // Calculating all the shifts in population
        // decimal added_Dead = nr_Infected * demographic.death_Chance + demographic.recovery_Chance * nr_Infected * disease.death_Chance;
        
        //decimal added_Susceptible = 0;// population * demographic.birth_Chance;
        decimal added_Infected = infection_Chance * nr_Susceptible;
        decimal added_Recovered = demographic.recovery_Chance * nr_Infected;
        deaths_Per_Day = policies.Deaths(nr_Infected, added_Recovered, population, disease.death_Chance);
        added_Recovered -= deaths_Per_Day;
        decimal added_Dead = deaths_Per_Day;
        updateStats(added_Dead, added_Infected, added_Recovered);



    }

    void updateStats( decimal added_Dead,  decimal added_Infected,  decimal added_Recovered)
    {
        // Updating all the variables of the population
        nr_Susceptible -= added_Infected;
        nr_Infected += added_Infected;
        nr_Infected -= added_Recovered;
        nr_Infected -= added_Dead;
        nr_Recovered += added_Recovered;
        nr_Dead += added_Dead;
    }

    void printStats(int n = 0)
    {
        Console.WriteLine($"day {n}");
        Console.WriteLine($"S: {nr_Susceptible}");
        Console.WriteLine($"I: {nr_Infected}");
        Console.WriteLine($"R: {nr_Recovered}");
        Console.WriteLine($"D: {nr_Dead}");
        Console.WriteLine($"\nRho: {carrier_Density}");
        Console.WriteLine($"R: {reproduction_Number}");
        Console.WriteLine($"lamda: {reproduction_Number}");

        Console.WriteLine("-------------------------------------\n");
    }
}



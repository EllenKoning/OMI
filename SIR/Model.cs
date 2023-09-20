using System;
using System.IO;

class Model
{
    Filer filer;

    ///start values population
    decimal population;
    decimal nr_Susceptible = 100;
    decimal nr_Infected = 1;
    decimal nr_Recovered = 0;
    decimal nr_Dead = 0;

    // spontaneous chances
    const decimal recovery_Chance = 0.1m;
    const decimal death_Chance = 0.001m;
    const decimal birth_Chance = 0.00001m;
    const decimal std_InfectionChance = 0.5m;

    // protections
    const decimal vacination_Protection = 0.8m;
    const decimal vacination_Rate = 0.9m;

    const decimal hygiene_Protection = 0.3m;
    const decimal hygiene_Rate = 0.3m;

    // contact per day
    const int far_Contact = 35; //same room. half the infection rate?
    const int close_Contact = 10; //sharing personal space

    // variables by def
    // no touchie
    const decimal total_Contact = (far_Contact / 2 + close_Contact);
    const decimal vacination_Factor = 1 - vacination_Protection * vacination_Rate;
    const decimal hygiene_Factor = 1 - hygiene_Protection * hygiene_Rate;

    decimal[] SIRD
    {
        get { return new decimal[4] { nr_Susceptible, nr_Infected, nr_Recovered, nr_Dead }; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filer"></param>
    public Model(Filer filer)
    {
        this.filer = filer;
        
    }

    /// <summary>
    /// Updates the model for n loops. Can save .csv files to storage.
    /// </summary>
    /// <param name="n">number of days</param>
    /// <param name="save">save raw data to csv file</param>
    public void run(int n, bool save = false)
    {
        if (save)
            filer.clearData();
        for (int i = 0; i < n; i++)
        {
            if (save)
                filer.saveData(SIRD);
            updateStats();
            printStats(i);
        }
        Console.WriteLine(nr_Susceptible + nr_Infected + nr_Recovered + nr_Dead);
        Console.ReadLine();
    }

    void updateStats()
    {
        population = nr_Susceptible + nr_Infected + nr_Recovered;
        decimal disease_Carrier_Chance = nr_Infected / population; 
        decimal infection_Chance = 1 - (decimal)Math.Pow((double)(1 - disease_Carrier_Chance * std_InfectionChance * vacination_Factor * hygiene_Factor), (double)total_Contact); //make not double pls


        // Calculating all the shifts in population
        decimal added_Dead = nr_Infected * death_Chance;
        decimal added_Susceptible = population * birth_Chance;
        decimal added_Infected = infection_Chance * nr_Susceptible;
        decimal added_Recovered = nr_Infected * recovery_Chance;
        

        // Updating all the variables of the population
        nr_Susceptible -= added_Infected;
        nr_Susceptible += added_Susceptible;
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
        Console.WriteLine("-------------------------------------\n");
    }
}



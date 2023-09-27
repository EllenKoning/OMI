using System;
using System.Globalization;
using System.IO;
//using CsvHelper;

class Model
{
    Filer filer;
    //Plotter plotter;
    Demographic demographic;
    Disease disease;


    ///start values population
    decimal population;
    decimal nr_Susceptible = 9999999;
    decimal nr_Infected = 1;
    decimal nr_Recovered = 0;
    decimal nr_Dead = 0;


    decimal[] SIRD
    {
        get { return new decimal[4] { nr_Susceptible, nr_Infected, nr_Recovered, nr_Dead }; }
    }

    public Model(Filer _filer, Demographic _demographic, Disease _disease)
    {
        filer = _filer;
        demographic = _demographic;
        disease = _disease;
        //plotter = new Plotter();
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
                filer.saveData(SIRD, i);
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
        decimal infection_Chance = 1 - (decimal)Math.Pow((double)(1 - disease_Carrier_Chance * demographic.std_Infection_Chance * disease.vacination_Factor * disease.hygiene_Factor), (double)demographic.total_Contact); //make not double pls


        // Calculating all the shifts in population
        decimal added_Dead = nr_Infected * demographic.death_Chance;
        //decimal added_Susceptible = population * person.birth_Chance;
        decimal added_Infected = infection_Chance * nr_Susceptible;
        decimal added_Recovered = nr_Infected * demographic.recovery_Chance;

        Console.WriteLine($"infection chance: {infection_Chance}");
        // Updating all the variables of the population
        nr_Susceptible -= added_Infected;
        //nr_Susceptible += added_Susceptible;
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



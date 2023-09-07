using System;
class Model
{
    decimal nr_Susceptible = 99999;
    //decimal[] infectionStages = new decimal[5] { 1,0,0,0,0 };
    decimal nr_Infected = 1;
    int stage = 0;
    decimal nr_Recovered = 0;
    long population;

    //decimal nr_Infected {
    //    get {
    //        decimal sum = 0;
    //        for (int i = 0; i < infectionStages.Length; i++)
    //        {
    //            sum += infectionStages[i];
    //        }
    //        return sum;
    //    }
    //}


    decimal std_InfectionRate = 0.1m;
    int farContact = 35; //same room. half the infection rate?
    int closeContact = 10; //sharing personal space
    decimal hygieneFactor = 1 / 2; //multiply by this factor

    public Model()
    {
        population = (long) (nr_Susceptible + nr_Infected + nr_Recovered);
        for (int i = 0; i < 100; i++)
        {
            updateStats();
            printStats(i);
        }
        Console.WriteLine(nr_Susceptible + nr_Infected + nr_Recovered);
        Console.ReadLine();
    }

    void updateStats()
    {
  
        decimal totalContact = (farContact / 2 + closeContact);
        decimal chanceOfCarrier = nr_Infected / population;
        decimal chanceOfInfection = 1 - (decimal)Math.Pow((double)( 1 - chanceOfCarrier * std_InfectionRate), (double)totalContact); //make not double pls
        decimal added_Infected =  chanceOfInfection * nr_Susceptible;
        decimal added_Recovered = nr_Infected * 0.1m;


        nr_Susceptible -= added_Infected;
        nr_Infected += added_Infected;
        nr_Infected -= added_Recovered;
        nr_Recovered += added_Recovered;
        
        
        
        //nr_Susceptible = (nr_Susceptible < 0) ? 0 : nr_Susceptible;

        //infectionStages[stage] = added_Infected;
        //stage = (stage + 1) % 5;
        //decimal added_Recovered = infectionStages[stage];
        //int added_Recovered;
        //int updated_Susceptible;
        //int updated 

    }

    void printStats(int n = 0)
    {
        Console.WriteLine($"day {n}");
        Console.WriteLine($"S: {nr_Susceptible}");
        Console.WriteLine($"I: {nr_Infected}");
        Console.WriteLine($"R: {nr_Recovered}");
        Console.WriteLine("-------------------------------------\n");

       
    }



    //birthrate
    //deat rate :)

    // graaf van mensen met super spreader (bijv jongeren)
    //contacten vs slechte weerstand (zijn ouderen beter of slechter beschermt want nieand houd van ouderen)

    //enum State
    //{
    //    Susceptible,
    //    Infected,
    //    Recovered
    //}
}

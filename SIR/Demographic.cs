using System;


public class Demographic
{
    public readonly int age;

    // spontaneous chances
    
    public readonly decimal death_Chance;// = 0.1m;
    public readonly decimal birth_Chance;// = 0.00001m;
    public readonly decimal std_Infection_Chance;// = 0.02m;

    public readonly decimal vacination_Rate;

    // contact per day
    public readonly int far_Contact;// = 35; //same room. half the infection rate?
    public readonly int close_Contact;// = 10; //sharing personal space
    public readonly decimal recovery_Chance;// = 0.1m;




    // variables by def
    // no touchie
    public readonly decimal total_Contact;

    public Demographic(decimal avg_recovery_days = 5
                      , decimal death_Rate_Per_Year_Per_Thousand = 9.81m //https://www.macrotrends.net/countries/NLD/netherlands/death-rate
                      , decimal birth_Rate_Per_Year_Per_Thousand = 10.82m //https://www.macrotrends.net/countries/NLD/netherlands/birth-rate
                      , decimal _std_Infection_Chance = 0.1m
                      ,int _far_Contact = 50
                      ,int _close_Contact = 10
                      )
    {
        int DAYS_OF_THE_YEAR = 365;
        recovery_Chance = 1/avg_recovery_days;
        death_Chance = death_Rate_Per_Year_Per_Thousand / (DAYS_OF_THE_YEAR * 1000);
        birth_Chance = birth_Rate_Per_Year_Per_Thousand / (DAYS_OF_THE_YEAR * 1000);
        std_Infection_Chance = _std_Infection_Chance;
        far_Contact = _far_Contact;
        close_Contact = _close_Contact;


        total_Contact = (far_Contact / (decimal) 3) + close_Contact;
    }
}

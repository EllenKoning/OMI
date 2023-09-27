using System;


public class Disease
{
    // no touchie
    // protections
    public readonly decimal vacination_Protection = 0.95m;
    public readonly decimal vacination_Rate = 0.9m;

    public readonly decimal hygiene_Protection = 0.3m;
    public readonly decimal hygiene_Rate = 0.3m;


    // not touchie
    public readonly decimal vacination_Factor;
    public readonly decimal hygiene_Factor;
    public Disease(decimal _vacination_Protection = 0.95m, decimal _vacination_Rate = 0.9m, decimal _hygiene_Protection = 0.3m, decimal _hygiene_Rate = 0.3m)
    {
        vacination_Protection = _vacination_Protection;
        vacination_Rate = _vacination_Rate;
        hygiene_Protection = _hygiene_Protection;
        hygiene_Rate = _hygiene_Rate;

        vacination_Factor = 1 - vacination_Protection * vacination_Rate;
        hygiene_Factor = 1 - hygiene_Protection * hygiene_Rate;
    }
}


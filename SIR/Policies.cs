using System;

public class Policies
{
	private readonly decimal social_Isolation_Factor;
	private int policydaysleft = 0;
	private readonly int policydays;
	private readonly decimal startingpoint, asymptotic_Carrier_Factor;
	private readonly bool social_Isolation, lockdown;
	private readonly decimal IC_Beds_Per_Hundredthousand;
	private readonly decimal hospitalisation_Risk = 0.016m;


    public Policies(bool _lockdown = true, bool _social_Isolation = true, decimal _social_Isolation_Factor = 0.25m, int _policyDays = 30, decimal _startingpoint = 0.1m, decimal _IC_Beds_Per_Hundredthousand = 6.4m )
	{
		social_Isolation_Factor = _social_Isolation_Factor;
		policydays = _policyDays;
		startingpoint = _startingpoint;
		social_Isolation = _social_Isolation;
		lockdown = _lockdown;
		IC_Beds_Per_Hundredthousand = _IC_Beds_Per_Hundredthousand;

    }

	public decimal Deaths(decimal infected, decimal recovered, decimal population,  decimal death_Rate)
	{
		decimal beds = IC_Beds_Per_Hundredthousand * (population/100000);
		decimal IC = infected * hospitalisation_Risk;

        if ( IC <= beds)
			return recovered * death_Rate;

		decimal outIC = IC - beds;
		decimal ratio = decimal.Divide(outIC, infected);


		return ratio * death_Rate * 10 * recovered + recovered * death_Rate;
	}


	/// <summary>
	/// Can only be called once per day!!!!
	/// </summary>
	/// <param name="density">rho</param>
	/// <returns></returns>
	public decimal Lockdown_Factor(decimal density)
	{
		if (!lockdown)
			return 1;

        if (density > startingpoint)
		{
			policydaysleft = policydays;
            return social_Isolation_Factor;
        }

        if (policydaysleft > 0)
		{
			policydaysleft--;
            return social_Isolation_Factor;
        }

		return 1;
	}

	public decimal Isolation_Factor(decimal density)
	{
        if (!social_Isolation)
            return 1;

        if (density > startingpoint)
        {
            policydaysleft = policydays;
            return social_Isolation_Factor;
        }

        if (policydaysleft > 0)
        {
            policydaysleft--;
            return social_Isolation_Factor;
        }

		return 1;
    }
}


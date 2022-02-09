using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PriceUnitsFormatter
{
    public static string SetUpPriceUnits(string unitsFormat)
    {
        if (unitsFormat == "rub_roll")
        {
            return (" \u20BD\\рулон");
        }
        else if (unitsFormat == "rub_m2")
		{
            return (" \u20BD\\м\u00B2");
		}
        else if (unitsFormat == "rub_apiece")
		{
            return (" \u20BD\\шт");
		}
        else if (unitsFormat == "rub_liter")
		{
            return (" \u20BD\\литр");
		}
        else
        {
            return (" \u20BD");
        }
    }

    public static string SetUpOneUnit(string unitsFormat)
	{
        if (unitsFormat == "rub_roll")
        {
            return (" рул.");
        }
        else if (unitsFormat == "rub_m2")
        {
            return (" м\u00B2");
        }
        else if (unitsFormat == "rub_apiece")
        {
            return (" шт.");
        }
        else if (unitsFormat == "rub_liter")
        {
            return (" л.");
        }
        else
        {
            return (" шт.");
        }
    }
}

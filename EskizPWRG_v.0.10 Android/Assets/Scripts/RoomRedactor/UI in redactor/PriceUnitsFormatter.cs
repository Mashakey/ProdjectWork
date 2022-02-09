using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PriceUnitsFormatter
{
    public static string SetUpPriceUnits(string unitsFormat)
    {
        if (unitsFormat == "rub_roll")
        {
            return (" \u20BD\\�����");
        }
        else if (unitsFormat == "rub_m2")
		{
            return (" \u20BD\\�\u00B2");
		}
        else if (unitsFormat == "rub_apiece")
		{
            return (" \u20BD\\��");
		}
        else if (unitsFormat == "rub_liter")
		{
            return (" \u20BD\\����");
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
            return (" ���.");
        }
        else if (unitsFormat == "rub_m2")
        {
            return (" �\u00B2");
        }
        else if (unitsFormat == "rub_apiece")
        {
            return (" ��.");
        }
        else if (unitsFormat == "rub_liter")
        {
            return (" �.");
        }
        else
        {
            return (" ��.");
        }
    }
}

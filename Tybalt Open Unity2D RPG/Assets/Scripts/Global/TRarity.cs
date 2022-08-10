using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TRarity
{
    /// <summary>
    /// Creates a hierachy of percentages and generates a roll
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="ranks"></param>
    /// <returns></returns>
    public static int RarityHierachyPercent(float percent, int ranks)
    {
        if (ranks == 0)
        {
            return 0;
        }

        //Make Array Cuz Its Simple
        float[] temp = new float[ranks];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = percent;
        }

        //Roll Iteration
        for (int i = 0; i < temp.Length; i++)
        {
            if (PercentRoll(temp[i]) == false)
            {
                return i;
            }
            else if (i == temp.Length - 1)
            {
                return i;
            }
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }

    /// <summary>
    /// Creates a hierachy of percentages and generates a roll
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="ranks"></param>
    /// <returns></returns>
    public static int ChaoticRarityPercent(float percent)
    {
        int i = 0;

        while (i != -1)
        {
            if (PercentRoll(percent) == false)
            {
                return i;
            }
            i++;
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }
    /// <summary>
    /// Rarity Hierarchy Simple, input odds with a max rank.
    /// </summary>
    /// <param name="odds"></param>
    /// <param name="ranks"></param>
    /// <returns></returns>
    public static int RarityHierarchy(int odds, int ranks)
    {
        int[] temp = new int[ranks];
        
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = odds;
        }

        return RarityHierarchy(temp);
    }
    /// <summary>
    /// Rarity Hierachy simple, input odds with max rank and a percentage modifier.
    /// </summary>
    /// <param name="odds"></param>
    /// <param name="ranks"></param>
    /// <param name="percentMod"></param>
    /// <returns></returns>
    public static int RarityHierarchy(int odds, int ranks, float percentMod)
    {
        //Make Array Cuz Its Simple
        int[] temp = new int[ranks];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = odds;
        }

        //Roll Iteration
        for (int i = 0; i < temp.Length; i++)
        {
            float totalPercent = percentMod + (1f / temp[i]);
            //Debug.Log(totalPercent);

            if (PercentRoll(totalPercent) == false)
            {
                return i;
            }
            else if (i == temp.Length - 1)
            {
                return i;
            }
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }
    /// <summary>
    /// Rarity Hierachy input with odds array.
    /// </summary>
    /// <param name="oddsArray"></param>
    /// <returns></returns>
    public static int RarityHierarchy(int[] oddsArray)
    {
        for (int i = 0; i < oddsArray.Length; i++)
        {
            if(RandomInteger(0, oddsArray[i]) != 0)
            {
                return i;
            }
            else if (i == oddsArray.Length -1)
            {
                return i;
            }
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }
    /// <summary>
    /// Rarity Hierarchy input odds array and percentage modifier.
    /// </summary>
    /// <param name="oddsArray"></param>
    /// <param name="percentMod"></param>
    /// <returns></returns>
    public static int RarityHierarchy(int[] oddsArray, float percentMod)
    {
        //Use Array And Iterate
        for (int i = 0; i < oddsArray.Length; i++)
        {
            float totalPercent = percentMod + (1f / oddsArray[i]);
            //Debug.Log(totalPercent);

            if (PercentRoll(totalPercent) == false)
            {
                return i;
            }
            else if (i == oddsArray.Length - 1)
            {
                return i;
            }
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }
    /// <summary>
    /// Rarity Hierachy input odds, max ranks, percentage modifiers and a minimum modifier.
    /// </summary>
    /// <param name="odds"></param>
    /// <param name="ranks"></param>
    /// <param name="percent"></param>
    /// <param name="minMod"></param>
    /// <returns></returns>
    public static int RarityHierarchy(int odds, int ranks, float percent, int minMod)
    {
        if (minMod >= ranks)
        {
            Debug.LogError("Error min can't be greater than ranks");
        }

        //Simple array set all to odds
        int[] temp = new int[ranks];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = odds;
        }

        //Roll Iteration
        for (int i = minMod; i < ranks; i++)
        {
            float totalPercent = percent + (1f / temp[i]);

            if (PercentRoll(totalPercent) == false)
            {
                return i;
            }
            else if (i == temp.Length - 1)
            {
                return i;
            }
        }

        Debug.Log("Rarity Busted");
        return 31337;
    }
    /// <summary>
    /// Generates a random integer with both parameters being exclusive.
    /// </summary>
    /// <param name="minInclusive"></param>
    /// <param name="maxInclusive"></param>
    /// <returns></returns>
    public static int RandomInteger(int minInclusive, int maxInclusive)
    {       
        int roll;

        //roll = UnityEngine.Random.Range(minInclusive, maxInclusive + 1);

        //System.Random rng = new System.Random();

        roll = RandomStaticSystem.rng.Next(minInclusive, maxInclusive + 1);

        return roll;
    }  

    //Input percentage to roll
    public static bool PercentRoll(float p)
    {
        float roll = (float)RandomStaticSystem.rng.NextDouble();

        return roll < p ? true : false;
    }

    //Static number generator guarrantees that there will
    //will be no repitition.
    private class RandomStaticSystem
    {
        public static System.Random rng;

        static RandomStaticSystem()
        {
            rng = new System.Random();
        }
    }
}

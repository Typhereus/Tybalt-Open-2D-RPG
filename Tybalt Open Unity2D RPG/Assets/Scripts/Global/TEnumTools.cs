using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEnumTools
{
    public static T Random<T>() // add exclude zero
    {
        Array A = Enum.GetValues(typeof(T));

        T V;

        V = (T)A.GetValue(RandomInteger(0, A.Length - 1));

        return V;
    }
    public static T Random<T>(bool excludeZero) // add exclude zero
    {
        Array A = Enum.GetValues(typeof(T));

        T V;

        if(excludeZero == true)
        {
            V = (T)A.GetValue(RandomInteger(1, A.Length - 1));
        }
        else
        {
            V = (T)A.GetValue(RandomInteger(0, A.Length - 1));
        }

        return V;
    }
    public static T[] RandomArray<T>(bool excludeZero, int amount)
    {
        T[] typeArray = new T[amount];

        for (int i = 0; i < typeArray.Length; i++)
        {
            Array A = Enum.GetValues(typeof(T));

            if (excludeZero == true)
            {
                typeArray[i] = (T)A.GetValue(RandomInteger(1, A.Length - 1));
            }
            else
            {
                typeArray[i] = (T)A.GetValue(RandomInteger(0, A.Length - 1));
            }
        }
        return typeArray;
    }

    public static int GetMax<T>()
    {
        return Enum.GetValues(typeof(T)).Length;
    }

    public static int RandomInteger(int minInclusive, int maxInclusive)
    {
        int roll;

        //roll = UnityEngine.Random.Range(minInclusive, maxInclusive + 1);

        //System.Random rng = new System.Random();

        roll = RandomStaticSystem.rng.Next(minInclusive, maxInclusive + 1);

        return roll;
    }

    public static T GetWithString<T>(string e)
    {
        return (T)Enum.Parse(typeof(T), e, true);
    }

    private class RandomStaticSystem
    {
        public static System.Random rng;

        static RandomStaticSystem()
        {
            rng = new System.Random();
        }
    }
}

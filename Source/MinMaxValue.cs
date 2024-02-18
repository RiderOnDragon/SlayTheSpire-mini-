using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MinMaxValue
{
    [System.Serializable]
    public struct Int
    {
        public int Min;
        public int Max;

        public int GetRandomValue()
        {
            int value = Random.Range(Min, Max + 1);
            return value;
        }
    }

    [System.Serializable]
    public struct Float
    {
        public float Min;
        public float Max;

        public float GetRandomValue()
        {
            float value = Random.Range(Min, Max);
            return value;
        }
    }

}
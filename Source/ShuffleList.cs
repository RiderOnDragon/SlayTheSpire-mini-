using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShuffleList
{
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            T temp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = temp;
        }
    }
}

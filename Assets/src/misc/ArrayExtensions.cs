using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions {
    public static int[] GetRandomlyShuffledIntArray(int numberCount)
    {
        int[] result = new int[numberCount];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = i;
        }
        for (int i = 0; i < result.Length; i++)
        {
            int temporal = result[i];
            int randomIndex = UnityEngine.Random.Range(i, result.Length);
            result[i] = result[randomIndex];
            result[randomIndex] = temporal;
        }
        return result;
    }
}

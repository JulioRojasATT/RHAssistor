using System;
using UnityEngine;

public static class GenMath {
    
    public delegate void VariableModifyFunction(NaviVariable variable);

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

    public static int GenericCompare(object o1, object o2) {
        Type o1Type = o1.GetType();
        if (o1.GetType() == o2.GetType()) {
            if (o1Type == typeof(int)) {
                return ((int) o1).CompareTo((int) o2);
            }
            if (o1Type == typeof(bool)) {
                if(o1.Equals(o2)) {
                    return 0;
                }

                return -1;
            }
            if (o1Type == typeof(string)) {
                return ((string) o1).CompareTo((string)o2);
            }
        }
        return -1;
    }
    
    public static object ObjPrimitiveAddition(object o1, object o2) {
        Type o1Type = o1.GetType();
        if (o1.GetType() == o2.GetType()) {
            if (o1Type == typeof(int)) {
                return (int) o1 + (int) o2;
            }
            if (o1Type == typeof(float)) {
                return (float) o1 + (float) o2;
            }
            if (o1Type == typeof(string)) {
                return (string) o1 + (string) o2;
            }
        }
        throw new Exception("Error, objects aren't the same type");
    }
    
    public static object ObjPrimitiveSubtraction(object o1, object o2) {
        Type o1Type = o1.GetType();
        if (o1.GetType() == o2.GetType()) {
            if (o1Type == typeof(int)) {
                return (int) o1 - (int) o2;
            }
            if (o1Type == typeof(float)) {
                return (float) o1 - (float) o2;
            }
        }
        throw new Exception("Error, objects aren't the same type");
    }
}

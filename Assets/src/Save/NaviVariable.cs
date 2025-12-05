using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviVariable{

    public Type type;

    public object value;

    public NaviVariable(Type type, object value) {
        this.type = type;
        SetValue(value);
    }

    public bool SetValue(object newValue) {
        if (newValue.GetType()==type) {
            value = newValue;
            return true;
        }
        else {
            Debug.LogError("Error! Type of the value isn't the same as the type of the variable");
            return false;
        }
    }

    public T TryGetValue<T>(out T referenceValue)
    {
        if(value.GetType()==typeof(T)) {
            referenceValue = (T)value;
            return referenceValue;
        }
        referenceValue = (T)value;
        return default(T);
    }
    
    public bool AddValue(object addedValue) {
        object newValue = GenMath.ObjPrimitiveAddition(value, addedValue);
        return SetValue(newValue);
    }
    
    public bool SustractValue(object sustractedValue) {
        object newValue = GenMath.ObjPrimitiveSubtraction(value, sustractedValue);
        return SetValue(newValue);
    }
}
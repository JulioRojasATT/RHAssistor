using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEngine;

public static class TextProcessing {
    
    private static Type[] _allowedTypes = {typeof(int), typeof(bool), typeof(string)};
    
    /// <summary>
    /// Eliminates all apparitions of the pattern expression on s
    /// </summary>
    /// <param name="s"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static string Clean(string s, string pattern) {
        return Regex.Replace(s, pattern, string.Empty, RegexOptions.Multiline);
    }
    
    public static object GetStringTValue(Type t, string mystring) {
        TypeConverter foo = TypeDescriptor.GetConverter(t);
        return foo.ConvertFromInvariantString(mystring);
    }

    /// <summary>
    /// Determines the type of the string contained in s
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Type DetermineTypeOfString(string s) {
        TypeConverter tc;
        StringConverter sc = new StringConverter();
        foreach (Type allowedType in _allowedTypes) {
            tc = TypeDescriptor.GetConverter(allowedType);
            try {
                tc.ConvertFromInvariantString(s);
                return allowedType;
            }
            catch (Exception e) {
            }
        }
        Debug.LogError("ERROR! String can't be casted to any primitive type");
        return null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension 
{
    public static string ReplaceAt(this string str, int index, int length, string replace)
    {
        return str.Remove(index, Math.Min(length, str.Length - index))
            .Insert(index, replace);
    }
}

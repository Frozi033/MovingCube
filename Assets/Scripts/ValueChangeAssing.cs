using System;
using UnityEngine;

public class ValueChangeAssing : MonoBehaviour
{
    public static Action<DataType, float> ValueChangedHandler;

    protected void OnValueChange(DataType dataType, string value)
    {
        ValueChangedHandler?.Invoke(dataType, GetFloatValue(value));
    }
    
    private float GetFloatValue(string value)
    {
        return float.Parse(value);
    }
}
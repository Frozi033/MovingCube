using UnityEngine;

public class ValueReader : ValueChangeAssing
{
    [SerializeField] private DataType _currentDataType;
    
    public void SetValue(string value)
   {
       OnValueChange(_currentDataType, value);
   }
}

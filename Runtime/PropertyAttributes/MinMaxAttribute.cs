using UnityEngine;

namespace PropertyAttributes
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public float MinLimit;
        public float MaxLimit;
        public bool ShowEditRange;
        public bool ShowDebugValues;
        
        public MinMaxAttribute(float minLimit, float maxLimit)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }
    }
}
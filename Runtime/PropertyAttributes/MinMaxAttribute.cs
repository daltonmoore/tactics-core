using UnityEngine;

namespace PropertyAttributes
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public float MinLimit = 0;
        public float MaxLimit = 1;
        public bool ShowEditRange;
        public bool ShowDebugValues;
        
        public MinMaxAttribute(float minLimit, float maxLimit)
        {
            MinLimit = minLimit;
            MaxLimit = maxLimit;
        }
    }
}
using System;
using UnityEngine;

namespace PropertyAttributes
{
    public abstract class HidingAttribute : PropertyAttribute {}
    
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class HideIfAttribute : HidingAttribute
    {
        public readonly string variable;
        public readonly bool state;
        
        public HideIfAttribute(string variable, bool state)
        {
            this.variable = variable;
            this.state = state;
        }
    }
}
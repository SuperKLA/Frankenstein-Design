using System;

[AttributeUsage(AttributeTargets.Class)]
public class ResourceSelectableAttribute : Attribute
{
    public Type _Type;
    public string _Label;
    
    public ResourceSelectableAttribute(Type t, string label = "")
    {
        this._Type = t;
        this._Label = label;
    }
}
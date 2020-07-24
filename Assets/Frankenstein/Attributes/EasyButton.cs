using System;

public enum ButtonMode
{
    AlwaysEnabled,
    EnabledInPlayMode,
    DisabledInPlayMode
}

[Flags]
public enum ButtonSpacing 
{
    None   = 0,
    Before = 1,
    After  = 2
}
    
/// <summary>
/// Attribute to create a button in the inspector for calling the method it is attached to.
/// The method must have no arguments.
/// </summary>
/// <example>
/// [Button]
/// public void MyMethod()
/// {
///     Debug.Log("Clicked!");
/// }
/// </example>
[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public sealed class EasyButtonAttribute : Attribute
{
    private string        name    = null;
    private ButtonMode    mode    = ButtonMode.AlwaysEnabled;
    private ButtonSpacing spacing = ButtonSpacing.None;

    public string        Name    { get { return name; } }
    public ButtonMode    Mode    { get { return mode; } }
    public ButtonSpacing Spacing { get { return spacing; } }

    public EasyButtonAttribute()
    {
    }

    public EasyButtonAttribute(string name)
    {
        this.name = name;
    }

    public EasyButtonAttribute(ButtonMode mode)
    {
        this.mode = mode;
    }
        
    public EasyButtonAttribute(ButtonSpacing spacing) 
    {
        this.spacing = spacing;
    }
        
    public EasyButtonAttribute(string name, ButtonMode mode)
    {
        this.name = name;
        this.mode = mode;
    }

    public EasyButtonAttribute(string name, ButtonSpacing spacing) 
    {
        this.name    = name;
        this.spacing = spacing;
    }

    public EasyButtonAttribute(string name, ButtonMode mode, ButtonSpacing spacing) 
    {
        this.name    = name;
        this.mode    = mode;
        this.spacing = spacing;
    }
}
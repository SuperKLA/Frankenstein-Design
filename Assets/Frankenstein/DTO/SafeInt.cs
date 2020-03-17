using UnityEngine;

public struct SafeInt
{
    private int offset;
    private int value;

    public SafeInt(int value = 0)
    {
        this.offset     = Random.Range(-1000, +1000);
        this.value = value + this.offset;
    }

    public int GetValue()
    {
        return this.value - this.offset;
    }

    public void Dispose()
    {
        this.offset = 0;
        this.value  = 0;
    }

    public override string ToString()
    {
        return this.GetValue().ToString();
    }

    public static SafeInt operator +(SafeInt f1, SafeInt f2)
    {
        return new SafeInt(f1.GetValue() + f2.GetValue());
    }

    // ...the same for the other operators
}
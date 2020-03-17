namespace Frankenstein.Utils
{
    public class RefContainer<T1>
    {
        public T1 Val1 { get; set; }
    }
    
    public class RefContainer<T1, T2>
    {
        public T1 Val1 { get; set; }
        public T2 Val2 { get; set; }
    }
    
    public class RefContainer<T1, T2, T3>
    {
        public T1 Val1 { get; set; }
        public T2 Val2 { get; set; }
        public T3 Val3 { get; set; }
    }
    
    public class RefContainer<T1, T2, T3, T4>
    {
        public T1 Val1 { get; set; }
        public T2 Val2 { get; set; }
        public T3 Val3 { get; set; }
        public T4 Val4 { get; set; }
    }
}
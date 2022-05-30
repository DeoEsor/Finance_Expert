namespace ExpertFramework.Core;

public class Condition<T>
    where T : IComparable<T>
{
    public bool IsGreater(T a, T b) => a.CompareTo(b) > 0;
    public bool IsLess(T a, T b) => a.CompareTo(b) < 0;
    public bool IsGreaterOrEqual(T a, T b) => a.CompareTo(b) >= 0;
    public bool IsLessOrEqual(T a, T b) => a.CompareTo(b) <= 0;
    public bool IsEqual(T a, T b) => a.CompareTo(b) == 0;
}
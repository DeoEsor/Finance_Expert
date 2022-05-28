using System.Diagnostics.CodeAnalysis;

namespace ExpertFramework.Core;

public class Statement<T>
{
    public string Variable { get; init; }
    public T Value { get; init; }
    public Predicate<T> Condition { get; protected set; }

    public Statement(string variable, Predicate<T> condition, T value)
    {
        Variable = variable;
        Value = value;
        Condition = condition;
    }
    
    public IntersectionType MatchStatement(Statement<T> rhs) 
        => Variable != rhs.Variable ? IntersectionType.Unknown : Intersect(rhs);

    protected virtual IntersectionType Intersect(Statement<T> rhs) 
        => throw new NotImplementedException();

    public override string ToString() 
        => Variable + " " + Condition + " " + Value;

    public Q Compare<Q>(Statement<T> statement, [NotNull] Func<T, T, Q> rule)
        => rule(Value, statement.Value);
}
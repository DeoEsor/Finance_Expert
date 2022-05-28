using System.Text;
// ReSharper disable MemberCanBePrivate.Global

namespace ExpertFramework.Core;

public class WorkingMemory<T>
{
    public List<Statement<T>> Facts { get; } = new List<Statement<T>>();

    public void AddFact(Statement<T> fact) 
        => Facts.Add(fact);

    public bool IsNotFact(Statement<T> c) 
        => Facts.Any(fact => fact.MatchStatement(c) == IntersectionType.MutuallyExclude);

    public void ClearFacts() => Facts.Clear();

    public bool IsFact(Statement<T> c) 
        => Facts.Any(fact => fact.MatchStatement(c) == IntersectionType.Include);

    public override string ToString()
    {
        var message = new StringBuilder();

        var firstStatement = true;
        foreach(var cc in Facts)
            if (firstStatement)
            {
                message.Append(cc);
                firstStatement = false;
            }
            else
                message.Append("\n"+cc);

        return message.ToString();
    }
}
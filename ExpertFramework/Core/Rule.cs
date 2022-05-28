namespace ExpertFramework.Core;

public class Rule<T>
{
    public Statement<T> Consequent;
    public List<Statement<T>> Antecedents = new List<Statement<T>>();
    public string Name;
    public int Index;

    public Rule(string name) => Name = name;
    
    public bool IsFired { get; protected set; }

    public void IsFirstAntecedent() => Index = 0;

    public bool HasNextAntecedents() 
        => Index < Antecedents.Count;

    public bool IsTriggered(WorkingMemory<T> wm) 
        => Antecedents.All(antecedent => wm.IsFact(antecedent));
    public void SetConsequent(Statement<T> consequent) 
        => Consequent = consequent;

    public void AddAntecedent(Statement<T> antecedent) 
        => Antecedents.Add(antecedent);

    public Statement<T> NextAntecedent()
    {
        var c = Antecedents[Index];
        Index++;
        return c;
    }

    public void Fire(WorkingMemory<T> wm)
    {
        if (!wm.IsFact(Consequent))
            wm.AddFact(Consequent);

        IsFired = true;
    }
}
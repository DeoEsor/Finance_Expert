namespace ExpertFramework.Core;

public class RuleInferenceEngine<T>
{
    public List<Rule<T>> Rules = new List<Rule<T>>();
    public WorkingMemory<T> WorkingMemory = new WorkingMemory<T>();

    public void AddRule(Rule<T> rule) 
        => Rules.Add(rule);

    public void ClearRules() 
        => Rules.Clear();
    
    /// <summary>
    /// property indicating the known facts in the current working memory
    /// </summary>
    public WorkingMemory<T> Facts => WorkingMemory;

    /// <summary>
    /// Add another know fact into the working memory
    /// </summary>
    /// <param name="c"></param>
    public void AddFact(Statement<T> c) 
        => WorkingMemory.AddFact(c);

    /// <summary>
    /// Method that return the set of rules whose antecedents match with the working memory
    /// </summary>
    /// <returns></returns>
    protected List<Rule<T>> Match() 
        => Rules.Where(rule => rule.IsTriggered(WorkingMemory)).ToList();
    
    
    public void ClearFacts() 
        => WorkingMemory.ClearFacts();

    public void ForwardChain()
    {
        List<Rule<T>> cs = null!;
        do
        {
            cs = Match();
            if (cs.Count > 0 && !FireRule(cs))
                    break;
            
        } while (cs.Count > 0);
    }

    
    public Statement<T> BackwardChain(string goalVariable, List<Statement<T>> unprovedConditions)
    {
        Statement<T> conclusion = default!;
        var goalStack = Rules
            .Where(rule => rule.Consequent.Variable == goalVariable)
            .ToList();

        foreach(var rule in Rules)
        {
            rule.IsFirstAntecedent();
            var goalReached = true;
            while (rule.HasNextAntecedents())
            {
                var antecedent = rule.NextAntecedent();
                if (!WorkingMemory.IsFact(antecedent))
                {
                    if (WorkingMemory.IsNotFact(antecedent)) //conflict with memory
                    {
                        goalReached = false;
                        break;
                    }

                    if (IsFact(antecedent, unprovedConditions)) //deduce to be a fact
                    {
                        WorkingMemory.AddFact(antecedent);
                    }
                    else //deduce to not be a fact
                    {
                        goalReached = false;
                        break;
                    }
                }
            }

            if (goalReached)
            {
                conclusion = rule.Consequent;
                break;
            }
        }

        return conclusion;
    }

    protected bool IsFact(Statement<T> goal, List<Statement<T>> unprovedConditions)
    {
        var goalStack = Rules
            .Where(rule => rule.Consequent.MatchStatement(goal) == IntersectionType.Include)
            .ToList();

        if (goalStack.Count == 0)
        {
            unprovedConditions.Add(goal);
        }
        else
        {
            foreach(var rule in goalStack)
            {
                rule.IsFirstAntecedent();
                var goalReached = true;
                while (rule.HasNextAntecedents())
                {
                    var antecedent = rule.NextAntecedent();
                    if (WorkingMemory.IsFact(antecedent)) continue;
                    if (WorkingMemory.IsNotFact(antecedent))
                    {
                        goalReached = false;
                        break;
                    }

                    if (IsFact(antecedent, unprovedConditions))
                        WorkingMemory.AddFact(antecedent);
                    else
                    {
                        goalReached = false;
                        break;
                    }
                }

                if (goalReached)
                    return true;
            }
        }

        return false;
    }

    protected bool FireRule(List<Rule<T>> conflictingRules)
    {
        var hasRule2Fire = false;
        foreach (var rule in conflictingRules.Where(rule => !rule.IsFired))
        {
            hasRule2Fire = true;
            rule.Fire(WorkingMemory);
        }

        return hasRule2Fire;
    }
}
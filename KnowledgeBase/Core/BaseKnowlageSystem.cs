using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Core;

public abstract class BaseKnowledgeSystem<T> : IKnowledgeSystem<T>
{
    public IKnowledgeHolder<T> KnowledgeDbContext;

    protected BaseKnowledgeSystem(IKnowledgeHolder<T> knowledgeDbContext)
    {
        KnowledgeDbContext = knowledgeDbContext;
    }

    public abstract BaseKnowledgeSystem<T> Update();
    
    public abstract BaseKnowledgeSystem<T> AddKnowledge(T knowledge);
    
    public abstract BaseKnowledgeSystem<T> AddKnowledge(IEnumerable<T> knowledge);

    public abstract IEnumerable<T> GetKnowledge();
}
namespace KnowledgeBase;

public interface IKnowledgeHolder<T>
{
    public IEnumerable<T> GetKnowledge();
}
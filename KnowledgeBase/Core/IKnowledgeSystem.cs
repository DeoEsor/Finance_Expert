namespace KnowledgeBase.Core;

public interface IKnowledgeSystem<T>
{
    BaseKnowledgeSystem<T> Update();
    BaseKnowledgeSystem<T> AddKnowledge(T knowledge);
    BaseKnowledgeSystem<T> AddKnowledge(IEnumerable<T> knowledge);
    IEnumerable<T> GetKnowledge();
}
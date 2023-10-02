using System.Collections.Generic;

public interface ILocationContent
{
    bool Unlocked { get; }
    IEnumerable<ICollectionContent> Content { get; }

    void Unlock();
}

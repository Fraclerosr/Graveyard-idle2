using System.Collections.Generic;
using UnityEngine;

public interface ICollectionContent
{
    IEnumerable<Sprite> Icons { get; }
    string Name { get; }
    string Id { get; }
    UnlockRule UnlockRule { get; }
    int DollarsReward { get; }
}

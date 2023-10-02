using UnityEngine;

[CreateAssetMenu(fileName = "RewardedUnlockRule", menuName = "Unlock Rules/RewardedUnlockRule", order = 51)]
public class RewardedUnlockRule : UnlockRule
{
    public override bool ForRewarded => true;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AlwaysUnlockRule", menuName = "Unlock Rules/AlwaysUnlockRule", order = 51)]
public class AlwaysUnlockRule : UnlockRule
{
    public override int CurrentProgress => 0;
    public override int TargetProgress => 0;
}

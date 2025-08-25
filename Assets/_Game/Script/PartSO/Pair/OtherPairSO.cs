using UnityEngine;

[CreateAssetMenu(fileName = "OtherPairSO", menuName = "ScriptableObjects/Pair/OtherPairSO", order = 5)]
public class OtherPairSO : PairPartSO<PartPairConfig>
{
    public PartPairConfig Get()
    {
        return GetRandom();
    }
}
using UnityEngine;

[CreateAssetMenu(fileName = "HandSO", menuName = "ScriptableObjects/Pair/HandSO", order = 3)]
public class HandSO : PairPartSO<PartPairConfig>
{
    public PartPairConfig Get()
    {
        return GetRandom();
    }
}
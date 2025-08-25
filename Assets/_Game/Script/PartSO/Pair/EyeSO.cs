using UnityEngine;

[CreateAssetMenu(fileName = "EyeSO", menuName = "ScriptableObjects/Pair/EyeSO", order = 1)]
public class EyeSO : PairPartSO<PartPairConfig>
{
    public PartPairConfig Get()
    {
        return GetRandom();
    }
}

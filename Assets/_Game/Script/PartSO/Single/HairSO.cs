using UnityEngine;

[CreateAssetMenu(fileName = "HairSO", menuName = "ScriptableObjects/Single/HairSO", order = 6)]
public class HairSO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}

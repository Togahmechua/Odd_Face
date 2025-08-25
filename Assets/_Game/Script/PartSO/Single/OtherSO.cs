using UnityEngine;

[CreateAssetMenu(fileName = "OtherSO", menuName = "ScriptableObjects/Single/OtherSO", order = 3)]
public class OtherSO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}
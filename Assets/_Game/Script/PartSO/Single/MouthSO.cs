using UnityEngine;

[CreateAssetMenu(fileName = "MouthSO", menuName = "ScriptableObjects/Single/MouthSO", order = 5)]
public class MouthSO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}

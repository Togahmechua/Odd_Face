using UnityEngine;

[CreateAssetMenu(fileName = "NoseSO", menuName = "ScriptableObjects/Single/NoseSO", order = 2)]
public class NoseSO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}

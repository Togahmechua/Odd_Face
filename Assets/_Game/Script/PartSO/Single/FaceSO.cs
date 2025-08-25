using UnityEngine;

[CreateAssetMenu(fileName = "FaceSO", menuName = "ScriptableObjects/Single/FaceSO", order = 1)]
public class FaceSO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "BodySO", menuName = "ScriptableObjects/Single/BodySO", order = 4)]
public class BodySO : PartSO<PartConfig>
{
    public PartConfig Get()
    {
        return GetRandom();
    }
}

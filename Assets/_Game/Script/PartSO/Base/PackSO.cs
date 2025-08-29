using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackSO", menuName = "ScriptableObjects/Pack/PackSO", order = 1)]
public class PackSO : ScriptableObject
{
    [SerializeField] private List<PackConfig> packs = new List<PackConfig>();

    public List<PackConfig> GetAllPacks() => packs;

    public PackConfig GetRandomPack()
    {
        if (packs == null || packs.Count == 0) return null;
        return packs[Random.Range(0, packs.Count)];
    }
}

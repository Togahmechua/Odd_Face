using System.Collections.Generic;
using UnityEngine;

public class PairPartSO<T> : ScriptableObject where T : PartPairConfig
{
    [SerializeField] protected List<T> pairs = new List<T>();

    public List<T> GetAll() => pairs;

    public virtual T GetRandom()
    {
        if (pairs == null || pairs.Count == 0) return null;
        int rand = Random.Range(0, pairs.Count);
        return pairs[rand];
    }
}

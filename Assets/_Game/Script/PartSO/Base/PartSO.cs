using System.Collections.Generic;
using UnityEngine;

public abstract class PartSO<T> : ScriptableObject where T : PartConfig
{
    [SerializeField] protected List<T> parts = new List<T>();
    public List<T> GetAll() => parts;

    //public abstract T GetRandom();

    public virtual T GetRandom()
    {
        if (parts == null || parts.Count == 0) return null;
        int rand = Random.Range(0, parts.Count);
        return parts[rand];
    }
}

using System;
using UnityEngine;

[Serializable]
public class PartConfig
{
    public int id;

    [Header("===Sorting===")]
    public int orderInLayer;

    [Header("===Details===")]
    public string name;
    public Sprite sprite;
}

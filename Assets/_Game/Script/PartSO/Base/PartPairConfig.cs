using System;
using UnityEngine;

[Serializable]
public class PartPairConfig
{
    public int id;

    [Header("===Sorting===")]
    public int orderInLayer;

    [Header("===Left - Right Parts===")]
    public PartSide left;
    public PartSide right;
}

[Serializable]
public class PartSide
{
    public string displayName;   // "Tay Trái" hoặc "Tay Phải"
    public Sprite sprite;
}

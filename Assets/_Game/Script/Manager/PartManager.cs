using UnityEngine;

public class PartManager : Singleton<PartManager>
{
    [Header("Single Parts")]
    [SerializeField] private BodySO bodySO;
    [SerializeField] private FaceSO faceSO;
    [SerializeField] private HairSO hairSO;
    [SerializeField] private MouthSO mouthSO;
    [SerializeField] private NoseSO noseSO;
    [SerializeField] private OtherSO otherSO;

    [Header("Pair Parts")]
    [SerializeField] private EyeSO eyeSO;
    [SerializeField] private EyeBrowSO eyeBrowSO;
    [SerializeField] private HandSO handSO;
    [SerializeField] private LegSO legSO;
    [SerializeField] private OtherPairSO otherPairSO;

    #region GET
    // Single
    public PartConfig GetRandomBody() => bodySO.GetRandom();
    public PartConfig GetRandomFace() => faceSO.GetRandom();
    public PartConfig GetRandomHair() => hairSO.GetRandom();
    public PartConfig GetRandomMouth() => mouthSO.GetRandom();
    public PartConfig GetRandomNose() => noseSO.GetRandom();
    public PartConfig GetRandomOther() => otherSO.GetRandom();

    // Pair
    public PartPairConfig GetRandomEyes() => eyeSO.GetRandom();
    public PartPairConfig GetRandomEyeBrows() => eyeBrowSO.GetRandom();
    public PartPairConfig GetRandomHands() => handSO.GetRandom();
    public PartPairConfig GetRandomLegs() => legSO.GetRandom();
    public PartPairConfig GetRandomOtherPair() => otherPairSO.GetRandom();
    #endregion

    #region Random Build
    public void RandomPart(Transform trans)
    {
        // Spawn bắt buộc
        SpawnSingle(GetRandomBody(), trans);
        SpawnSingle(GetRandomFace(), trans);
        SpawnSingle(GetRandomHair(), trans);
        SpawnSingle(GetRandomMouth(), trans);
        SpawnSingle(GetRandomNose(), trans);

        SpawnPair(GetRandomEyes(), trans);
        SpawnPair(GetRandomEyeBrows(), trans);
        SpawnPair(GetRandomHands(), trans);
        SpawnPair(GetRandomLegs(), trans);

        // Spawn optional
        int randOther = Random.Range(0, 3); // 0 = none, 1 = single, 2 = pair
        if (randOther == 1) SpawnSingle(GetRandomOther(), trans);
        else if (randOther == 2) SpawnPair(GetRandomOtherPair(), trans);
    }
    #endregion

    #region Helpers
    private void SpawnSingle(PartConfig config, Transform trans)
    {
        if (config == null || config.sprite == null) return;

        GameObject go = new GameObject(config.name);
        go.transform.SetParent(trans, false);

        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = config.sprite;
        sr.sortingOrder = config.orderInLayer;

        go.SetActive(false); // theo yêu cầu ban đầu
    }

    private void SpawnPair(PartPairConfig config, Transform trans)
    {
        if (config == null) return;

        if (config.left != null && config.left.sprite != null)
        {
            GameObject left = new GameObject(config.left.displayName);
            left.transform.SetParent(trans, false);

            var sr = left.AddComponent<SpriteRenderer>();
            sr.sprite = config.left.sprite;
            sr.sortingOrder = config.orderInLayer;

            left.SetActive(false);
        }

        if (config.right != null && config.right.sprite != null)
        {
            GameObject right = new GameObject(config.right.displayName);
            right.transform.SetParent(trans, false);

            var sr = right.AddComponent<SpriteRenderer>();
            sr.sprite = config.right.sprite;
            sr.sortingOrder = config.orderInLayer;

            right.SetActive(false);
        }
    }
    #endregion
}

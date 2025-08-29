using UnityEditor.Sprites;
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

    [Header("Pack")]
    [SerializeField] private PackSO packSO;

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

    public void RandomByPack(Transform trans)
    {
        PackConfig pack = packSO.GetRandomPack();
        if (pack == null) return;

        SpawnSingle(pack.body, trans);
        SpawnSingle(pack.face, trans);
        SpawnSingle(pack.hair, trans);
        SpawnSingle(pack.mouth, trans);
        SpawnSingle(pack.nose, trans);

        SpawnPair(pack.eyes, trans);
        SpawnPair(pack.eyeBrows, trans);
        SpawnPair(pack.hands, trans);
        SpawnPair(pack.legs, trans);

        int randOther = Random.Range(0, 3); // 0 = none, 1 = single, 2 = pair
        if (randOther == 1) SpawnSingle(pack.other, trans);
        else if (randOther == 2) SpawnPair(pack.otherPair, trans);
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

        var col = go.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        // Set box size
        Vector2 currentSize = col.size;
        float newX = currentSize.x < 1.5f ? 1.5f : currentSize.x;
        float newY = currentSize.y < 1.5f ? 1.5f : currentSize.y;
        col.size = new Vector2(newX, newY);

        PartObject poNew = go.AddComponent<PartObject>();

        LevelManager.Ins.curLevel.AddToList(poNew);

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

            var col = left.AddComponent<BoxCollider2D>();
            col.isTrigger = true;

            // Set box size
            Vector2 currentSize = col.size;
            float newX = currentSize.x < 1.5f ? 1.5f : currentSize.x;
            float newY = currentSize.y < 1.5f ? 1.5f : currentSize.y;
            col.size = new Vector2(newX, newY);

            PartObject poNewL = left.AddComponent<PartObject>();

            LevelManager.Ins.curLevel.AddToList(poNewL);

            left.SetActive(false);
        }

        if (config.right != null && config.right.sprite != null)
        {
            GameObject right = new GameObject(config.right.displayName);
            right.transform.SetParent(trans, false);

            var sr = right.AddComponent<SpriteRenderer>();
            sr.sprite = config.right.sprite;
            sr.sortingOrder = config.orderInLayer;

            var col = right.AddComponent<BoxCollider2D>();
            col.isTrigger = true;

            // Set box size
            Vector2 currentSize = col.size;
            float newX = currentSize.x < 1.5f ? 1.5f : currentSize.x;
            float newY = currentSize.y < 1.5f ? 1.5f : currentSize.y;
            col.size = new Vector2(newX, newY);

            PartObject poNewR = right.AddComponent<PartObject>();

            LevelManager.Ins.curLevel.AddToList(poNewR);

            right.SetActive(false);
        }
    }

    #endregion
}

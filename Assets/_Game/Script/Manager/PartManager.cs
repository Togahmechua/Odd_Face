using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PartManager : Singleton<PartManager>
{
    [Header("Single Parts")]
    private BodySO bodySO;
    private FaceSO faceSO;
    private HairSO hairSO;
    private MouthSO mouthSO;
    private NoseSO noseSO;
    private OtherSO otherSO;

    [Header("Pair Parts")]
    private EyeSO eyeSO;
    private EyeBrowSO eyeBrowSO;
    private HandSO handSO;
    private LegSO legSO;
    private OtherPairSO otherPairSO;

    [Header("Pack")]
    private PackSO packSO;

    public bool IsLoaded { get; private set; } = false;

    private Dictionary<Type, Action<ScriptableObject>> typeMap;

    private void Awake()
    {
        typeMap = new Dictionary<Type, Action<ScriptableObject>>()
        {
            { typeof(BodySO), so => bodySO = so as BodySO },
            { typeof(FaceSO), so => faceSO = so as FaceSO },
            { typeof(HairSO), so => hairSO = so as HairSO },
            { typeof(MouthSO), so => mouthSO = so as MouthSO },
            { typeof(NoseSO), so => noseSO = so as NoseSO },
            { typeof(OtherSO), so => otherSO = so as OtherSO },

            { typeof(EyeSO), so => eyeSO = so as EyeSO },
            { typeof(EyeBrowSO), so => eyeBrowSO = so as EyeBrowSO },
            { typeof(HandSO), so => handSO = so as HandSO },
            { typeof(LegSO), so => legSO = so as LegSO },
            { typeof(OtherPairSO), so => otherPairSO = so as OtherPairSO },

            { typeof(PackSO), so => packSO = so as PackSO },
        };
    }

    private async void Start()
    {
        await LoadAllSOFromRemoteAsync();
    }

    #region LOAD REMOTE ASSETS
    private async Task LoadAllSOFromRemoteAsync()
    {
        Debug.Log("🔄 Bắt đầu load catalog remote...");

        // 🚨 XÓA CACHE TRIỆT ĐỂ TRƯỚC KHI LOAD
        if (Caching.ClearCache())
        {
            Debug.Log("✅ Đã xóa cache cũ");
        }

        Addressables.ClearResourceLocators();

        // URL catalog remote trên server Netlify MỚI
        string timestamp = DateTime.Now.Ticks.ToString();
        string remoteCatalogUrl = $"https://togadata.netlify.app/StandaloneWindows64/catalog_1.0.json?t={timestamp}";

        Debug.Log($"📦 Loading catalog từ: {remoteCatalogUrl}");

        try
        {
            // Load catalog remote với caching disabled
            var catalogHandle = Addressables.LoadContentCatalogAsync(remoteCatalogUrl, false);
            await catalogHandle.Task;

            if (catalogHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("✅ Catalog remote loaded thành công");

                // 🎯 DEBUG: Kiểm tra URLs thực tế
                DebugLocations();

                // Load assets từ label "AllPartsSO" từ remote
                await LoadAssetsFromRemote();
            }
            else
            {
                Debug.LogError($"❌ Lỗi load catalog: {catalogHandle.OperationException}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"❌ Lỗi load catalog: {ex.Message}");
        }
    }

    private async Task LoadAssetsFromRemote()
    {
        var handle = Addressables.LoadAssetsAsync<ScriptableObject>("AllPartsSO", null);
        var allSO = await handle.Task;

        foreach (var so in allSO)
        {
            if (typeMap.TryGetValue(so.GetType(), out var setter))
            {
                setter(so);
                Debug.Log($"✅ Loaded {so.GetType().Name}: {so.name} from remote!");
            }
            else
            {
                Debug.LogWarning($"⚠ Không có typeMap cho {so.GetType().Name} ({so.name})");
            }
        }

        IsLoaded = true;
        Debug.Log("🎉 Tất cả ScriptableObjects đã load xong từ server!");
    }

    private void DebugLocations()
    {
        try
        {
            var locators = Addressables.ResourceLocators;
            //Debug.Log($"🔍 Found {locators.Count} resource locators");

            foreach (var locator in locators)
            {
                foreach (var key in locator.Keys)
                {
                    if (locator.Locate(key, typeof(object), out var locations))
                    {
                        foreach (var location in locations)
                        {
                            Debug.Log($"📦 {key} -> {location.InternalId}");
                            // Kiểm tra nếu có URL cũ
                            if (location.InternalId.Contains("togahmechua"))
                            {
                                Debug.LogError($"🚨 PHÁT HIỆN URL CŨ: {location.InternalId}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"⚠ Không thể debug locations: {ex.Message}");
        }
    }
    #endregion

    #region GET RANDOM
    public PartConfig GetRandomBody() => bodySO?.GetRandom();
    public PartConfig GetRandomFace() => faceSO?.GetRandom();
    public PartConfig GetRandomHair() => hairSO?.GetRandom();
    public PartConfig GetRandomMouth() => mouthSO?.GetRandom();
    public PartConfig GetRandomNose() => noseSO?.GetRandom();
    public PartConfig GetRandomOther() => otherSO?.GetRandom();

    public PartPairConfig GetRandomEyes() => eyeSO?.GetRandom();
    public PartPairConfig GetRandomEyeBrows() => eyeBrowSO?.GetRandom();
    public PartPairConfig GetRandomHands() => handSO?.GetRandom();
    public PartPairConfig GetRandomLegs() => legSO?.GetRandom();
    public PartPairConfig GetRandomOtherPair() => otherPairSO?.GetRandom();
    #endregion

    #region RANDOM BUILD
    public void RandomPart(Transform parent)
    {
        SpawnSingle(GetRandomBody(), parent);
        SpawnSingle(GetRandomFace(), parent);
        SpawnSingle(GetRandomHair(), parent);
        SpawnSingle(GetRandomMouth(), parent);
        SpawnSingle(GetRandomNose(), parent);

        SpawnPair(GetRandomEyes(), parent);
        SpawnPair(GetRandomEyeBrows(), parent);
        SpawnPair(GetRandomHands(), parent);
        SpawnPair(GetRandomLegs(), parent);

        SpawnOptional(GetRandomOther(), GetRandomOtherPair(), parent);
    }

    public void RandomByPack(Transform parent)
    {
        PackConfig pack = packSO?.GetRandomPack();
        if (pack == null) return;

        SpawnSingle(pack.body, parent);
        SpawnSingle(pack.face, parent);
        SpawnSingle(pack.hair, parent);
        SpawnSingle(pack.mouth, parent);

        SpawnPair(pack.eyes, parent);
        SpawnOptional(pack.other, pack.otherPair, parent);
    }

    private void SpawnOptional(PartConfig single, PartPairConfig pair, Transform parent)
    {
        int choice = UnityEngine.Random.Range(0, 3);
        if (choice == 1) SpawnSingle(single, parent);
        else if (choice == 2) SpawnPair(pair, parent);
    }
    #endregion

    #region SPAWN HELPERS
    private GameObject SpawnPart(string name, Sprite sprite, int order, Transform parent)
    {
        if (sprite == null) return null;

        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);

        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = order;

        var col = go.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = Vector2.Max(col.size, Vector2.one * 1.5f);

        var partObj = go.AddComponent<PartObject>();
        LevelManager.Ins.curLevel.AddToList(partObj);

        go.SetActive(false);
        return go;
    }

    private void SpawnSingle(PartConfig config, Transform parent)
    {
        if (config == null) return;
        SpawnPart(config.name, config.sprite, config.orderInLayer, parent);
    }

    private void SpawnPair(PartPairConfig config, Transform parent)
    {
        if (config == null) return;

        if (config.left != null)
            SpawnPart(config.left.displayName, config.left.sprite, config.orderInLayer, parent);

        if (config.right != null)
            SpawnPart(config.right.displayName, config.right.sprite, config.orderInLayer, parent);
    }
    #endregion
}

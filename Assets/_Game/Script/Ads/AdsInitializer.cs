using UnityEngine;
using Unity.Services.Core;
//using Unity.Services.Mediation;
using System.Threading.Tasks;

public class AdsInitializer : MonoBehaviour
{
    [SerializeField] string _gameIdAndroid = "YOUR_GAME_ID_HERE";
    [SerializeField] string _gameIdIOS = "YOUR_GAME_ID_HERE";

    async void Awake()
    {
        await InitializeAds();
    }

    private async Task InitializeAds()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("✅ Unity Mediation initialized successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"❌ Initialization failed: {e.Message}");
        }
    }
}

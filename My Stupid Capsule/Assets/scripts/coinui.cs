using UnityEngine;
using TMPro;

public class MenuCoinDisplay : MonoBehaviour
{
    public TMP_Text coinText;  // Menüdeki Coin TextMeshPro UI

    private const string COIN_KEY = "CoinCount";
    private int totalCoins = 0;

    void Start()
    {
        LoadCoins();
        UpdateCoinUI();
    }

    void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt(COIN_KEY, 0);
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coin: " + totalCoins;
        else
            Debug.LogWarning("CoinText referansý atanmamýþ!");
    }

    // Ýstersen dýþarýdan coin deðiþtiðinde çaðýrabilirsin
    public void RefreshCoins()
    {
        LoadCoins();
        UpdateCoinUI();
    }
}

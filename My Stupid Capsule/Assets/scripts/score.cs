using UnityEngine;
using TMPro;

public class Scorecoin : MonoBehaviour
{
    public AudioSource altinsesi;
    public TextMeshProUGUI scoreText;

    private int sessionScore = 0; // Oyun i�i anl�k coin say�s�
    private int totalCoins = 0;   // Kaydedilmi� toplam coin say�s�

    private const string COIN_KEY = "CoinCount";

    void Start()
    {
        altinsesi = GetComponent<AudioSource>();

        totalCoins = PlayerPrefs.GetInt(COIN_KEY, 0);

        sessionScore = 0;
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        sessionScore += amount;
        UpdateScoreText();

        if (altinsesi != null)
            altinsesi.Play();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Coin: " + sessionScore;
    }

    // Oyun sahnesi de�i�meden �nce �a��r, toplam coinlere session coinleri ekleyip kaydeder
    public void SaveTotalCoins()
    {
        totalCoins += sessionScore;
        PlayerPrefs.SetInt(COIN_KEY, totalCoins);
        PlayerPrefs.Save();

        sessionScore = 0;
        UpdateScoreText();

        Debug.Log("Coin kaydedildi. Toplam coin: " + totalCoins);
    }

    public void ResetSessionScore()
    {
        sessionScore = 0;
        UpdateScoreText();
    }
}

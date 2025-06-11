using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Oyuniçi : MonoBehaviour
{
    public Scorecoin scorecoin; // Inspector'dan bağla

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine(LoadSavedLevel());
        }
    }

    IEnumerator LoadSavedLevel()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != savedLevel)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(savedLevel);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }

    public void OyunuSifirla()
    {
        PlayerPrefs.DeleteKey("SavedLevel");

        // Coinleri sıfırla
        PlayerPrefs.DeleteKey("CoinCount");
        PlayerPrefs.Save();

        // Oyun içi session coinleri sıfırla
        if (scorecoin != null)
        {
            scorecoin.ResetSessionScore();
        }

        ResetTime();
        StartCoroutine(LoadSceneAsync(1));
    }

    public void OyunuYenidenBaslat()
    {
        ResetTime();
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void Menu()
    {
        SaveCoinsBeforeSceneChange();
        ResetTime();
        StartCoroutine(LoadSceneAsync(0));
    }

    public void NextLevel()
    {
        SaveCoinsBeforeSceneChange();

        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("SavedLevel", nextLevel);
            PlayerPrefs.Save();
            ResetTime();
            StartCoroutine(LoadSceneAsync(nextLevel));
        }
        else
        {
            Debug.Log("Tüm bölümler tamamlandı!");
            PlayerPrefs.DeleteKey("SavedLevel");
            StartCoroutine(LoadSceneAsync(0));
        }
    }

    void SaveCoinsBeforeSceneChange()
    {
        if (scorecoin != null)
        {
            scorecoin.SaveTotalCoins();  // Oyun içi coinler toplam coinlere eklenip kaydediliyor
        }
        else
        {
            Debug.LogWarning("Scorecoin referansı atanmadı!");
        }
    }

    void ResetTime()
    {
        Time.timeScale = 1f;
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
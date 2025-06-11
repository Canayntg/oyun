using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Oyuniçi : MonoBehaviour
{
    void Start()
    {
        // Eğer ana menüde değilsek, kaydedilen seviyeye gitmeye çalış
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            StartCoroutine(LoadSavedLevel());
        }
    }

    // Kaydedilen leveli yükler
    IEnumerator LoadSavedLevel()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1); // varsayılan seviye 1
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

    // Oyunu sıfırlar ve ilk seviyeyi yükler
    public void OyunuSifirla()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        ResetTime();
        StartCoroutine(LoadSceneAsync(1)); // İlk seviye
    }

    // Oyunu yeniden başlatır
    public void OyunuYenidenBaslat()
    {
        ResetTime();
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    // Ana menüye döner
    public void Menu()
    {
        ResetTime();
        StartCoroutine(LoadSceneAsync(0)); // Ana menü sahnesi
    }

    // Bir sonraki seviyeye geçer ve kaydeder
    public void NextLevel()
    {
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
            // İstersen burada son ekranı veya menüyü çağırabilirsin
            StartCoroutine(LoadSceneAsync(0)); // Ana menüye dön
        }
    }

    // Zamanı sıfırlar
    void ResetTime()
    {
        Time.timeScale = 1f;
    }

    // Async sahne yükleyici
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject gameplayUI;
    public TextMeshProUGUI levelText;

    public Scorecoin scorecoin;  // Inspector'dan Scorecoin scriptini buraya baðlayýn

    public static bool isPaused = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        gameplayUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            int savedLevelIndex = PlayerPrefs.GetInt("SavedLevel", 1);
            UpdateLevelText(savedLevelIndex);
            return;
        }

        UpdateLevelText(currentSceneIndex);
    }

    void UpdateLevelText(int level)
    {
        if (levelText != null)
            levelText.text = "Level: " + level.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("duvar") && !isPaused)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bitiþ") && !isPaused)
        {
            Win();
            Destroy(other.gameObject);
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Win()
    {
        // Oyun kazanýldýðýnda coinleri kaydet
        if (scorecoin != null)
        {
            scorecoin.SaveTotalCoins();
        }
        else
        {
            Debug.LogWarning("Scorecoin referansý atanmadý!");
        }

        winPanel.SetActive(true);
        gameplayUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("SavedLevel", nextLevel);
            StartCoroutine(LoadSceneAsync(nextLevel));
        }
        else
        {
            Debug.Log("Tüm seviyeler tamamlandý!");
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync("mainmenu"));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

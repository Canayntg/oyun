using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ayarlarPaneli;
    public GameObject menuPanel;

    void Start()
    {
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void AyarlarPaneliAc()
    {
        ayarlarPaneli.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void MenuPaneliAc()
    {
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void YeniOyun()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        PlayerPrefs.SetInt("SavedLevel", 1); // level1 sahne indeksi (1 yap)
        PlayerPrefs.Save();
        SceneManager.LoadScene(1); // level1 sahnesi index 1
    }

    public void DevamEt()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        SceneManager.LoadScene(savedLevel);
    }

    public void OyunuSifirla()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        PlayerPrefs.DeleteKey("CoinCount");
        PlayerPrefs.Save();
        Debug.Log("Kayýt ve coinler silindi.");
    }

    public void Cikis()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýldý.");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ayarlarPaneli;
    public GameObject menuPanel;

    void Start()
    {
        // Ba�lang��ta sadece ana men� paneli a��k
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    // Ayarlar panelini a�
    public void AyarlarPaneliAc()
    {
        ayarlarPaneli.SetActive(true);
        menuPanel.SetActive(false);
    }

    // Ana men� paneline geri d�n
    public void MenuPaneliAc()
    {
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    // Yeni oyun ba�lat: kay�t s�f�rlan�r, ilk seviyeye ge�ilir
    public void YeniOyun()
    {
        PlayerPrefs.DeleteKey("SavedLevel"); // Kay�t tamamen silinir
        PlayerPrefs.SetInt("SavedLevel", 2); // 2: level1�in build index�i
        PlayerPrefs.Save();
        SceneManager.LoadScene("level1"); // Sahne ad� �level1� olmal�
    }

    // Kald��� yerden devam et
    public void DevamEt()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 2); // Kay�t yoksa 2. sahneye (level1) gider
        SceneManager.LoadScene(savedLevel);
    }

    // Oyunu s�f�rlar (kay�t silinir ama sahne de�i�mez)
    public void OyunuSifirla()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        Debug.Log("Kay�t silindi.");
    }

    // Oyunu kapat
    public void Cikis()
    {
        Application.Quit();
        Debug.Log("Oyun kapat�ld�.");
    }
}

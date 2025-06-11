using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ayarlarPaneli;
    public GameObject menuPanel;

    void Start()
    {
        // Baþlangýçta sadece ana menü paneli açýk
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    // Ayarlar panelini aç
    public void AyarlarPaneliAc()
    {
        ayarlarPaneli.SetActive(true);
        menuPanel.SetActive(false);
    }

    // Ana menü paneline geri dön
    public void MenuPaneliAc()
    {
        ayarlarPaneli.SetActive(false);
        menuPanel.SetActive(true);
    }

    // Yeni oyun baþlat: kayýt sýfýrlanýr, ilk seviyeye geçilir
    public void YeniOyun()
    {
        PlayerPrefs.DeleteKey("SavedLevel"); // Kayýt tamamen silinir
        PlayerPrefs.SetInt("SavedLevel", 2); // 2: level1’in build index’i
        PlayerPrefs.Save();
        SceneManager.LoadScene("level1"); // Sahne adý “level1” olmalý
    }

    // Kaldýðý yerden devam et
    public void DevamEt()
    {
        int savedLevel = PlayerPrefs.GetInt("SavedLevel", 2); // Kayýt yoksa 2. sahneye (level1) gider
        SceneManager.LoadScene(savedLevel);
    }

    // Oyunu sýfýrlar (kayýt silinir ama sahne deðiþmez)
    public void OyunuSifirla()
    {
        PlayerPrefs.DeleteKey("SavedLevel");
        Debug.Log("Kayýt silindi.");
    }

    // Oyunu kapat
    public void Cikis()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýldý.");
    }
}

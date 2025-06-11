using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudSys : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI volumeAmount;
    [SerializeField] private Slider slider;

    private void Start() { LoadAudio(); }

    public void SetAudio(float value)
    {
        AudioListener.volume = value;
        volumeAmount.text = ((int)(value * 100)).ToString();
        SaveAudio();
    }

    private void SaveAudio()
    {
        PlayerPrefs.SetFloat("audioVolume", AudioListener.volume);
    }

    private void LoadAudio()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("audioVolume");
            AudioListener.volume = savedVolume;
            slider.value = savedVolume;
            volumeAmount.text = ((int)(savedVolume * 100)).ToString();
        }
        else
        {
            float defaultVolume = 0.5f;
            PlayerPrefs.SetFloat("audioVolume", defaultVolume);
            AudioListener.volume = defaultVolume;
            slider.value = defaultVolume;
            volumeAmount.text = ((int)(defaultVolume * 100)).ToString();
        }
    }
}
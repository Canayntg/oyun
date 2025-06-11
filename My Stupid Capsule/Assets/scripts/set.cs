using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class UsernameAndCoinManager : MonoBehaviour
{
    [Header("Kullanýcý Adý Sistemi")]
    public GameObject usernameTextObject;
    public GameObject usernameInputObject;
    public TMP_Text usernameText;
    public TMP_InputField usernameInput;

    private const string USERNAME_KEY = "Username";

    void Start()
    {
        LoadUsername();

        usernameInputObject.SetActive(false);
        AddClickEvent(usernameTextObject, ShowInputField);
        usernameInput.onEndEdit.AddListener(delegate { HideInputField(); });
    }

    void LoadUsername()
    {
        usernameText.text = PlayerPrefs.GetString(USERNAME_KEY, "Player");
    }

    void ShowInputField()
    {
        usernameTextObject.SetActive(false);
        usernameInputObject.SetActive(true);
        usernameInput.text = usernameText.text;
        usernameInput.ActivateInputField();
    }

    void HideInputField()
    {
        if (!string.IsNullOrWhiteSpace(usernameInput.text))
        {
            usernameText.text = usernameInput.text;
            PlayerPrefs.SetString(USERNAME_KEY, usernameInput.text);
            PlayerPrefs.Save();
        }

        usernameInputObject.SetActive(false);
        usernameTextObject.SetActive(true);
    }

    void AddClickEvent(GameObject target, UnityEngine.Events.UnityAction action)
    {
        EventTrigger trigger = target.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = target.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { action(); });

        trigger.triggers.Add(entry);
    }
}

using UnityEngine;
using TMPro;

public class Scorecoin : MonoBehaviour
{
    public AudioSource altinsesi;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        altinsesi = GetComponent<AudioSource>();
        UptadeScoretext();
    }

    public void AddScore(int Amount)
    {
        score += Amount;
        UptadeScoretext();
        altinsesi.Play(); 
    }

    private void UptadeScoretext()
    {
        scoreText.text = "Coin: " + score;
    
    }
}

using TMPro;
using UnityEngine;

// Met à jour tous les textes de l'interface à chaque image.
public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private WaveManager waveManager;
    
    [Header("Barre du haut")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text timerText;
    
    [Header("Divers")]
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text speedText;
    
    [Header("Fin de partie")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text endText;
    
    private float messageTimer = 0f;
    
    private void Start()
    {
        endPanel.SetActive(false);
        messageText.text = "";
    }
    
    private void Update()
    {
        moneyText.text = "Argent : " + gameManager.GetMoney() + " $";
        livesText.text = "Vies : " + gameManager.GetLives();
        waveText.text = "Vague : " + waveManager.GetWaveNumber() + " / " + waveManager.GetWaveCount();
        
        if (waveManager.IsWaiting())
        {
            // Mathf.Ceil arrondit au-dessus : 4.2 -> 5
            timerText.text = "Prochaine vague : " + Mathf.Ceil(waveManager.GetCountdown()) + " s";
        }
        else
        {
            timerText.text = "Ennemis : " + waveManager.GetEnemiesAlive();
        }
        
        speedText.text = "Vitesse x" + Time.timeScale;
        
        // Le message disparaît au bout de quelques secondes
        // (unscaledDeltaTime : pas influencé par la vitesse du jeu)
        if (messageTimer > 0f)
        {
            messageTimer -= Time.unscaledDeltaTime;
            if (messageTimer <= 0f)
            {
                messageText.text = "";
            }
        }
    }
    
    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageTimer = 2f;
    }
    
    public void ShowEndScreen(bool victory)
    {
        endPanel.SetActive(true);
        
        if (victory)
        {
            endText.text = "VICTOIRE !";
        }
        else
        {
            endText.text = "GAME OVER";
        }
    }
}

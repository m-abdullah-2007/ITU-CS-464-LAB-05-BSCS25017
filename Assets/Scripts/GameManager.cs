using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] int targetScore = 10;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject winPanel;

    [Header("Bonus")]
    [SerializeField] Spawner spawner;
    [SerializeField] TMP_Text bonusText;      // starts hidden
    [SerializeField] int bonusTarget = 50;

    int score;
    int bonus;
    int redThreshold;
    bool redSpawned;

    void Awake() => Instance = this;

    void Start()
    {
        // Task 1: pick the score at which the single red coin appears (1 to 9, see note below)
        redThreshold = Random.Range(1, targetScore);
        Debug.Log($"Red coin will spawn when score reaches {redThreshold}");
        UpdateUI();
        UpdateBonusUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();

        if (!redSpawned && score >= redThreshold)
        {
            redSpawned = true;            // guarantees only ONE red coin
            spawner.SpawnRedCoin();
        }

        if (score >= targetScore) Win();
    }

    // Task 2
    public void CollectRedCoin(Vector3 playerPosition)
    {
        spawner.SpawnBonusCoins(playerPosition, bonusTarget);
    }

    // Task 3
    public void AddBonus()
    {
        bonus++;
        bonusText.gameObject.SetActive(true);   // reveals the hidden text on the first blue coin
        UpdateBonusUI();
    }

    void UpdateUI() => scoreText.text = $"Score: {score} / {targetScore}";
    void UpdateBonusUI() => bonusText.text = $"BonusCoins: {bonus}/{bonusTarget}";

    void Win() { winPanel.SetActive(true); Time.timeScale = 0f; }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
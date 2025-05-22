using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;

    public int score { get; private set; } = 0;

    public int currentLevel;

    public List<Sprite> GoalSprite;
    public Image GoalImage;

    [SerializeField] private int levelNo;

    [SerializeField] private List<int> WinList = new List<int>();
    [SerializeField] private List<int> gameTimeList = new List<int>();

    public GameObject GameWinObject;
    public GameObject GameLossObject;


    public float gameTimer;

    public Text GameTimerText;

    public bool isWintrue = false;

    public GameObject OptionPoup;
    public GameObject RulesPopup;

    private void Awake() {
        if (PlayerPrefs.HasKey("currentLevel")) {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
            Debug.Log("currentLevel == " + currentLevel);
        }
        else {
            currentLevel = PlayerPrefs.GetInt("UnlockedLevel");

        }

        levelNo = currentLevel;
        SetGoal();

        gameTimer = gameTimeList[currentLevel];

        if (Instance != null) {
            DestroyImmediate(gameObject);
        }
        else {
            Instance = this;
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        NewGame();
    }

    public void NewGame() {
        // reset score
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();

        // hide game over screen
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    private void Update() {
        if (!isWintrue) {
            gameTimer -= Time.deltaTime;
            GameTimerText.text = ((int)gameTimer).ToString();

            if (gameTimer <= 0) {
                GameLossObject.SetActive(true);
                isWintrue = true;
            }
        }
    }

    public void GameOver() {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f) {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points) {
        SetScore(score + points);
    }

    private void SetScore(int score) {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore() {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    private int LoadHiscore() {
        return PlayerPrefs.GetInt("hiscore", 0);
    }


    public void SetGoal() {
        GoalImage.sprite = GoalSprite[currentLevel];
    }
    public int GetCurrentLevel() {
        return currentLevel;
    }

    public void CheckWin(int currentNumber) {
        if (WinList[levelNo] == currentNumber) {
            GameWinObject.SetActive(true);
            isWintrue = true;

            if (currentLevel != 9) {
                currentLevel += 1;
                PlayerPrefs.SetInt("currentLevel", currentLevel);
                PlayerPrefs.SetInt("UnlockedLevel", currentLevel);
            }
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene("2048");
    }


    public void OpenLevel() {
        SceneManager.LoadScene("Levels");
    }

    public void OpenOption() {
        OptionPoup.SetActive(true);
    }
    public void CloseOption() {
        OptionPoup.SetActive(false);
    }

    public void OpenRule() {
        RulesPopup.SetActive(true);
    }

    public void CloseRule() {
        RulesPopup.SetActive(false);
    }

    public void CloseGame() {
        SceneManager.LoadScene("Menu");
    }

}

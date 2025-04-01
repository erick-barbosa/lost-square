using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler: MonoBehaviour {
    public static GameHandler instance;
    [SerializeField] private GameObject gameoverMenu;
    [SerializeField] private TextMeshProUGUI startOrContinueText;
    private bool hasSave;
    private bool isOnMenu;
    [SerializeField] private bool testing;

    public bool IsOnMenu { get => isOnMenu; set => isOnMenu = value; }

    private void Awake() {
        IsOnMenu = SceneManager.GetActiveScene().name == "MainMenu";

        if (testing) {
            PlayerPrefs.DeleteAll();
        }

        // Singleton Pattern
        if (instance == null) {
            instance = this;
            SetupObject();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void OnChangeToMenu() {
        if (hasSave) {
            if (startOrContinueText) {
                startOrContinueText.text = "Continue";
            }
        }
        else {
            if (startOrContinueText) {
                startOrContinueText.text = "Play";
            }
        }

        gameoverMenu.SetActive(false);
        IsOnMenu = true;
    }

    private void SetupObject() {
        GetComponent<Canvas>().worldCamera = Camera.main;

        hasSave = PlayerPrefs.HasKey("save");

        OnChangeToMenu();

        gameoverMenu.SetActive(false);
    }

    // Reinicia a fase atual
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Troca para a próxima fase
    public void NextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else {
            FinishGame();
        }
    }

    // Troca para a próxima fase
    private void ContinueGame() {
        int currentSceneIndex = PlayerPrefs.GetInt("save");
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
    
    // Troca para a próxima fase
    private void StartGame() {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("save", 1);
    }

    // Troca para a próxima fase
    public void SaveGame() {
        PlayerPrefs.SetInt("save", SceneManager.GetActiveScene().buildIndex);
    }

    // Finaliza o jogo
    public void FinishGame() {
        Debug.Log("Parabéns! Você terminou o jogo!");
        gameoverMenu.SetActive(true);
    }
    
    // Finaliza o jogo
    public void StartOrContinueGame() {
        /*if (hasSave) {
            ContinueGame();
        }
        else {*/
            StartGame();
        //}

        IsOnMenu = false;
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
        OnChangeToMenu();
    }
}

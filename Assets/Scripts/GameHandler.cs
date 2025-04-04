using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler: MonoBehaviour {
    public static GameHandler instance;
    [SerializeField] private GameObject gameoverMenu;

    private bool hasSave;
    private bool isOnMenu;
    [SerializeField] private bool testing;

    public event Action OnSetup;

    public bool IsOnMenu { get => isOnMenu; set => isOnMenu = value; }
    public bool HasSave { get => hasSave; set => hasSave = value; }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnMenuSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnMenuSceneLoaded;
    }

    private void Awake() {
        IsOnMenu = SceneManager.GetActiveScene().name == "MainMenu";

        if (testing) {
            PlayerPrefs.DeleteAll();
        }


        // Singleton Pattern
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    

    private void SetupObject() {
        GetComponent<Canvas>().worldCamera = Camera.main;

        HasSave = PlayerPrefs.HasKey("save");
        

        gameoverMenu.SetActive(false);
        IsOnMenu = true;

        OnSetup?.Invoke();
    }

    // Reinicia a fase atual
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Troca para a próxima fase
    public void NextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (HasNextLevel(nextSceneIndex)) {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else {
            FinishGame();
        }
    }

    private bool HasNextLevel(int index) {
        if (index >= SceneManager.sceneCountInBuildSettings) {
            return false;
        }

        string nextScenePath = SceneUtility.GetScenePathByBuildIndex(index);
        if (string.IsNullOrEmpty(nextScenePath)) {
            return false;
        }

        string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);
        if (nextSceneName == "LevelMenu") {
            return false;
        }

        return true;
    }

    // Troca para a próxima fase
    public void LoadLevel(int index) {
        if (index < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(index);
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
        gameoverMenu.SetActive(true);
    }
    
    // Finaliza o jogo
    public void StartOrContinueGame() {
        if (HasSave) {
            ContinueGame();
        }
        else {
            StartGame();
        }

        IsOnMenu = false;
    }

    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnMenuSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "MainMenu") {
            SetupObject();
        } else {
            SaveGame();
        }
    }

    public void OpenLevelMenu() {
        SceneManager.LoadScene("LevelMenu");
    }
}

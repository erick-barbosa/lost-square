using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler: MonoBehaviour {
    public static GameHandler instance;

    private void Awake() {
        // Singleton Pattern
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Reinicia a fase atual
    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Troca para a pr�xima fase
    public void NextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else {
            Debug.Log("Parab�ns! Voc� terminou todas as fases!");
        }
    }

    // Finaliza o jogo
    public void FinishGame() {
        Debug.Log("Jogo finalizado!");
        // Carregar uma tela de fim de jogo, mostrar UI, etc.
    }
}

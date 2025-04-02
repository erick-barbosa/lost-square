using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI txtStartOrContinue;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private Button btnStartOrContinue;
    [SerializeField] private Button btnSelectLevel;

    private void Start() {
        btnStartOrContinue.onClick.AddListener(GameHandler.instance.StartOrContinueGame);
        btnSelectLevel.onClick.AddListener(GameHandler.instance.OpenLevelMenu);
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnChangeToMenu;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnChangeToMenu;
    }

    private void OnChangeToMenu(Scene scene, LoadSceneMode mode) {
        if (GameHandler.instance.HasSave) {
            if (txtStartOrContinue) {
                txtStartOrContinue.text = "Continue";
                txtLevel.text = "lvl " + PlayerPrefs.GetInt("save");
            }
        }
        else {
            if (txtStartOrContinue) {
                txtStartOrContinue.text = "Play";
                txtLevel.text = "";
            }
        }
    }
}
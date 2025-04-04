using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour {
    [SerializeField] private Button btnBackToMenu;

    private void Start() {
        btnBackToMenu.onClick.AddListener(GameHandler.instance.BackToMenu);
    }

    void Update() {
        
    }
}

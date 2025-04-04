using UnityEngine;

public class LevelItem : MonoBehaviour {
    private void OnMouseDown() {
        GameHandler.instance.LoadLevel(GetLevelIndexFromName());
    }

    private int GetLevelIndexFromName() {
        string levelName = gameObject.name;
        int levelIndex = int.Parse(levelName.Substring(levelName.Length - 1));
        return levelIndex;
    }
}

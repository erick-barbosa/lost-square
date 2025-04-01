using UnityEngine;

public class GameOverScreen : MonoBehaviour {
    public GameObject fireworksPrefab; // Prefab dos fogos

    void Start() {
        InvokeRepeating(nameof(SpawnFireworks), 0, 1.5f); // Dispara fogos periodicamente
    }

    void SpawnFireworks() {
        if (GameHandler.instance.IsOnMenu) {
            CancelInvoke(nameof(SpawnFireworks)); // Cancela os fogos
            return;
        }

        Vector3 randomPos = new (Random.Range(-5, 5), Random.Range(-5, 5), 0);
        Instantiate(fireworksPrefab, randomPos, Quaternion.identity);
    }
}
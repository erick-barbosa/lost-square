using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour {
    private ParticleSystem ps;

    void Start() {
        ps = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (ps && !ps.IsAlive()) { // Verifica se a partícula terminou
            Destroy(gameObject);
        }
    }
}

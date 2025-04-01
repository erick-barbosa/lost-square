using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private InputActionReference moveAction; // Refer�ncia para a a��o de movimento
    [SerializeField] private float speed = 25f; // Velocidade de movimento
    [SerializeField] private Transform startPos; // Posi��o em que o jogador inicia
    [SerializeField] private LayerMask obstacleLayer; // Camada para detectar colis�es

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        transform.position = startPos.position;
    }

    private void FixedUpdate() {
        movementInput = moveAction.action.ReadValue<Vector2>();

        // Move apenas se houver espa�o dispon�vel
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movementInput);
    }
}

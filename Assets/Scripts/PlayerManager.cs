using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private InputActionReference moveAction; // Referência para a ação de movimento
    [SerializeField] private float speed = 5f; // Velocidade de movimento
    [SerializeField] private Transform startPos; // Posição em que o jogador inicia
    [SerializeField] private float distance = 0.04f; // Tamanho da área de detecção
    [SerializeField] private LayerMask obstacleLayer; // Camada para detectar colisões

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        transform.position = startPos.position;
    }

    private void FixedUpdate() {
        movementInput = moveAction.action.ReadValue<Vector2>();

        // Ajusta o movimento baseado na colisão
        Vector2 adjustedMovement = AdjustMovement(movementInput);

        // Move apenas se houver espaço disponível
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * adjustedMovement);
    }

    private Vector2 AdjustMovement(Vector2 direction) {
        if (direction == Vector2.zero)
            return Vector2.zero; // Sem movimento, sem necessidade de checar.

        Vector2 adjustedMovement = direction;

        return adjustedMovement;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
    [SerializeField] private InputActionReference moveAction; // Refer�ncia para a a��o de movimento
    [SerializeField] private float speed = 5f; // Velocidade de movimento
    private Vector2 boxSize;// Tamanho da �rea de detec��o
    [SerializeField] private Transform startPos;// Posi��o em que o jogador inicia
    [SerializeField] private float distance = 0.04f; // Tamanho da �rea de detec��o
    [SerializeField] private LayerMask obstacleLayer; // Camada para detectar colis�es

    private Vector2 movementInput;

    private void Awake() {
        var boxCollider = GetComponent<BoxCollider2D>();

        transform.position = startPos.position;
        boxSize = transform.localScale;
    }

    private void Update() {
        movementInput = moveAction.action.ReadValue<Vector2>();

        // Ajusta o movimento baseado na colis�o
        Vector2 adjustedMovement = AdjustMovement(movementInput);

        // Move apenas se houver espa�o dispon�vel
        transform.position += speed * Time.deltaTime * (Vector3)adjustedMovement;
    }
    private Vector2 AdjustMovement(Vector2 direction) {
        if (direction == Vector2.zero)
            return Vector2.zero; // Sem movimento, sem necessidade de checar.

        Vector2 adjustedMovement = direction;

        // Testa colis�o no eixo X
        if (direction.x != 0) {
            RaycastHit2D hitX = 
                Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(direction.x, 0), distance, obstacleLayer);
            if (hitX.collider != null) {
                adjustedMovement.x = 0; // Bloqueia o movimento no eixo X
            }
        }

        // Testa colis�o no eixo Y
        if (direction.y != 0) {
            RaycastHit2D hitY = 
                Physics2D.BoxCast(transform.position, boxSize, 0, new Vector2(0, direction.y), distance, obstacleLayer);
            if (hitY.collider != null) {
                adjustedMovement.y = 0; // Bloqueia o movimento no eixo Y
            }
        }

        return adjustedMovement;
    }
}
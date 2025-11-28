using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    private Movement movement;
    private PlayerController input;
    private Vector2 moveInput;
    private Vector2 lastDirection;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        input = new PlayerController();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Move.performed += OnMove;
    }

    private void OnDisable()
    {
        input.Gameplay.Move.performed -= OnMove;
        input.Disable();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (movement.Direction != lastDirection)
        {
            float angle = Mathf.Atan2(this.movement.Direction.y, this.movement.Direction.x);
            this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

            lastDirection = movement.Direction;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (moveInput.y > 0)
        {
            movement.SetDirection(Vector2.up);
        }
        else if (moveInput.y < 0)
        {
            movement.SetDirection(Vector2.down);
        }
        else if (moveInput.x < 0)
        {
            movement.SetDirection(Vector2.left);
        }
        else if (moveInput.x > 0)
        {
            movement.SetDirection(Vector2.right);
        }
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}

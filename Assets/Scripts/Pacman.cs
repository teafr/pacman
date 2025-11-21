using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour, @PlayerController.IGameplayActions
{
    private Movement movement;
    private PlayerController input;
    private Vector2 moveInput;
    private Vector2 lastDirection;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        input = new PlayerController();
        input.Gameplay.SetCallbacks(this);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (movement.direction != lastDirection)
        {
            float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
            this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

            lastDirection = movement.direction;
        }
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
}

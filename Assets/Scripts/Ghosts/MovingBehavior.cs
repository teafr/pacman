using UnityEngine;

public abstract class MovingBehavior : GhostBehavior
{
    protected void HandleNodeCollision(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && IsReadyToChangeDirection())
        {
            SetNewDirection(node);
        }
    }

    protected bool IsReadyToChangeDirection()
    {
        return enabled && !Ghost.Frightened.enabled;
    }

    protected abstract void SetNewDirection(Node node);

    protected bool IsDirectionOpposite(Vector2 direction)
    {
        return direction == -Ghost.Movement.Direction;
    }
}
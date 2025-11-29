using UnityEngine;

public class GhostChase : MovingBehavior
{
    private void OnDisable()
    {
        Ghost.Scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleNodeCollision(collision);
    }

    protected override void SetNewDirection(Node node)
    {
        Ghost.Movement.SetDirection(FindOptimalDirection(node));
    }

    private Vector2 FindOptimalDirection(Node node)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        foreach (Vector2 availableDirection in node.AvailableDirections)
        {
            if (IsDirectionOpposite(availableDirection)) continue;

            float distance = CalculateDistance(GetNewPosition(availableDirection));

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        return direction;
    }

    private float CalculateDistance(Vector3 position)
    {
        return (Ghost.target.position - position).sqrMagnitude;
    }

    private Vector3 GetNewPosition(Vector2 direction)
    {
        return transform.position + new Vector3(direction.x, direction.y, 0.0f);
    }
}

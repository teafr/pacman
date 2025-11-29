using UnityEngine;

public class GhostScatter : MovingBehavior
{
    private void OnDisable()
    {
        Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleNodeCollision(collision);
    }

    protected override void SetNewDirection(Node node)
    {
        int index = Random.Range(0, node.AvailableDirections.Count);

        if (IsDirectionOpposite(node.AvailableDirections[index]) && node.AvailableDirections.Count > 1)
        {
            index = NormalizeIndex(index, node.AvailableDirections.Count);
        }

        Ghost.Movement.SetDirection(node.AvailableDirections[index]);
    }

    private static int NormalizeIndex(int currentIndex, int count)
    {
        currentIndex++;
        if (currentIndex >= count)
        {
            currentIndex = 0;
        }
        return currentIndex;
    }
}

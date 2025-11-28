using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obsticleLayer;
    public List<Vector2> AvailableDirections { get; private set; }

    private void Start()
    {
        AvailableDirections = new List<Vector2>();
        CheckAllDirections();
    }

    private void CheckAllDirections()
    {
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, obsticleLayer);
        if (hit.collider == null)
        {
            AvailableDirections.Add(direction);
        }
    }
}

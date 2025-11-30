using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public SpriteRenderer SpriteRenderer { get; private set; }
    public Movement Movement { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        SetDirectionSprite(Movement.Direction);
    }

    private void SetDirectionSprite(Vector2 direction)
    {
        if (direction == Vector2.up) SpriteRenderer.sprite = up;
        else if (direction == Vector2.down) SpriteRenderer.sprite = down;
        else if (direction == Vector2.left) SpriteRenderer.sprite = left;
        else if (direction == Vector2.right) SpriteRenderer.sprite = right;
    }

}

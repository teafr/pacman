using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    private const string PacmanLayerName = "Pacman";

    private const float FlashDurationThreshold = 2.0f;
    private const float InitialSpeedMultiplier = 0.5f;
    private const float NormalSpeedMultiplier = 1.0f;

    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool IsEaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        KeepOnlyBlue();
        Invoke(nameof(Flash), duration / FlashDurationThreshold);
    }

    public override void Disable()
    {
        base.Disable();
        ReturnInitialState();
    }

    public void Flash()
    {
        if (enabled && !IsEaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void OnEnable()
    {
        ChangeSpeedMultipier(InitialSpeedMultiplier);
    }

    private void OnDisable()
    {
        ChangeSpeedMultipier(NormalSpeedMultiplier);
    }

    private void ChangeSpeedMultipier(float multiplier)
    {
        Ghost.Movement.speedMultiplier = multiplier;
        IsEaten = false;
    }

    private void Eaten()
    {
        IsEaten = true;
        Ghost.transform.position = GetHomePosition();
        Ghost.Home.Enable(duration);
        KeepOnlyEyes();
    }

    private Vector3 GetHomePosition()
    {
        Vector3 position = Ghost.Home.inside.position;
        position.z = Ghost.transform.position.z;
        return position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PacmanLayerName) && enabled)
        {
            Eaten();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            Ghost.Movement.SetDirection(FindFurthestDirection(node));
        }
    }

    private Vector2 FindFurthestDirection(Node node)
    {
        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        foreach (Vector2 availableDirection in node.AvailableDirections)
        {
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (Ghost.target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance)
            {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        return direction;
    }

    private void KeepOnlyEyes()
    {
        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void KeepOnlyBlue()
    {
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
    }

    private void ReturnInitialState()
    {
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
}

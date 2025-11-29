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

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration / FlashDurationThreshold);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
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
        Vector3 position = Ghost.Home.inside.position;
        position.z = Ghost.transform.position.z;
        Ghost.transform.position = position;
        Ghost.Home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PacmanLayerName) && enabled)
        {
            Eaten();
        }
    }
}

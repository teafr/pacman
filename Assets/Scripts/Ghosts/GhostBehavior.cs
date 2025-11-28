using UnityEngine;

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        Ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        SetEnabledAndCancelInvoke(true);
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        SetEnabledAndCancelInvoke(false);
    }

    private void SetEnabledAndCancelInvoke(bool isEnabled)
    {
        this.enabled = isEnabled;
        CancelInvoke();
    }
}

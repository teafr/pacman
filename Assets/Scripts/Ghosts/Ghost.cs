using UnityEngine;

public class Ghost : MonoBehaviour
{
    private const string PacmanLayerName = "Pacman";

    public int points = 200;

    public GhostBehavior initialBehavior;
    public Transform target;

    public Movement Movement { get; private set; }
    public GhostHome Home { get; private set; }
    public GhostScatter Scatter { get; private set; }
    public GhostChase Chase { get; private set; }
    public GhostFrightened Frightened { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Home = GetComponent<GhostHome>();
        Scatter = GetComponent<GhostScatter>();
        Chase = GetComponent<GhostChase>();
        Frightened = GetComponent<GhostFrightened>();
    }

    public void ChangeState(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    public void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        Movement.ResetState();

        Scatter.Disable();
        Chase.Disable();
        Frightened.Disable();

        if (initialBehavior != null)
        {
            if (this.Home != this.initialBehavior)
            {
                Home.Disable();
            }

            initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(PacmanLayerName))
        {
            HandlePacmanCollision();
        }
    }

    private void HandlePacmanCollision()
    {
        var gameManager = FindFirstObjectByType<GameManager>();
        if (Frightened.enabled)
        {
            gameManager.GhostEaten(this);
        }
        else
        {
            gameManager.PacmanEaten();
        }
    }
}

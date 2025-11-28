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

    public void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();
        ResetBehaviorStates();
    }

    private void ResetBehaviorStates()
    {
        DisableNonInitialBehaviors();

        if (initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }

    private void DisableNonInitialBehaviors()
    {
        Scatter.Disable();
        Chase.Disable();
        Frightened.Disable();

        if (Home != initialBehavior)
        {
            Home.Disable();
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

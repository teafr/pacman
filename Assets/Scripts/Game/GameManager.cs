using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private const int InitialLives = 3;
    private const int InitialGhostMultiplier = 1;
    private const float ResetStateDelay = 3.0f;

    private PlayerController input;

    [Header("Game References")]
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    [Header("UI References")]
    public ScoreDisplay scoreDisplay;   // ссылка на UI для счёта
    public GameOverUI gameOverUI;       // ссылка на UI для Game Over

    public int GhostMultiplier { get; private set; } = InitialGhostMultiplier;
    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Awake()
    {
        input = new PlayerController();
        input.Gameplay.Restart.Disable();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        NewGame();
        input.Gameplay.Restart.performed += OnRestart;
    }

    private void OnDestroy()
    {
        input.Gameplay.Restart.performed -= OnRestart;
    }

    private void OnRestart(InputAction.CallbackContext ctx)
    {
        if (this.Lives <= 0)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        input.Gameplay.Restart.Disable();
        SetScore(0);
        SetLives(InitialLives);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        foreach (var ghost in ghosts)
        {
            ghost.ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        ChangeState(false);
        input.Gameplay.Restart.Enable();

        if (gameOverUI != null)
        {
            gameOverUI.Show(); // показать экран Game Over
        }
    }


    private void ChangeState(bool isActive)
    {
        foreach (var ghost in ghosts)
        {
            ghost.gameObject.SetActive(isActive);
        }

        this.pacman.gameObject.SetActive(isActive);
    }

    private void SetScore(int score)
    {
        this.Score = score;

        if (scoreDisplay != null)
        {
            scoreDisplay.SetScore(score); // обновляем UI
        }
    }

    private void SetLives(int lives)
    {
        this.Lives = lives;

        if (livesDisplay != null)
        {
            livesDisplay.SetLives(lives);
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.GhostMultiplier;
        SetScore(this.Score + points);
        this.GhostMultiplier++;
    }
    public LivesDisplay livesDisplay;

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);

        SetLives(this.Lives - 1);

        if (this.Lives > 0)
        {
            Invoke(nameof(ResetState), ResetStateDelay);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.Score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), ResetStateDelay);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        foreach (var ghost in ghosts)
        {
            ghost.Frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.GhostMultiplier = InitialGhostMultiplier;
    }

    public void ExitGame()
    {
        Debug.Log("Выход из игры...");

        Application.Quit(); // работает в билде

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // работает в редакторе
#endif
    }
}


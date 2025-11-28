using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int InitialLives = 3;
    private const int InitialGhostMultiplier = 1;
    private const float ResetStateDelay = 3.0f;

    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int GhostMultiplier { get; private set; } = InitialGhostMultiplier;
    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
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
            ghost.gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
    }

    private void GameOver() 
    {
        ChangeState(false);
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
    }

    private void SetLives(int lives)
    {
        this.Lives = lives;
    }

    private void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.GhostMultiplier;
        SetScore(this.Score + points);
        this.GhostMultiplier++;
    }

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
        // TODO: changing ghost state

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
}

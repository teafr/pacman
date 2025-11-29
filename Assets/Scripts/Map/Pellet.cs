using UnityEngine;

public class Pellet : MonoBehaviour
{
    private const string PacmanLayerName = "Pacman";

    public int points = 10;
    protected GameManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<GameManager>();
    }

    protected virtual void Eat()
    {
        manager.PelletEaten(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(PacmanLayerName))
        {
            Eat();
        }    
    }
}

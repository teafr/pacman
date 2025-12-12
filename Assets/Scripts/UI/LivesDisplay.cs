using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    public Image[] lifeDots;

    public void SetLives(int lives)
    {
        for (int i = 0; i < lifeDots.Length; i++)
        {
            lifeDots[i].enabled = i < lives;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Header("UI Digits")]
    public Image[] digits;         
    public Sprite[] digitSprites;  

    public void SetScore(int score)
    {
        string scoreStr = score.ToString();
        int digitIndex = digits.Length - 1;

        for (int i = scoreStr.Length - 1; i >= 0 && digitIndex >= 0; i--, digitIndex--)
        {
            int digit = scoreStr[i] - '0';

            if (digit >= 0 && digit < digitSprites.Length)
            {
                digits[digitIndex].sprite = digitSprites[digit];
                digits[digitIndex].enabled = true;
            }
            else
            {
                Debug.LogWarning("Неверная цифра: " + digit);
            }
        }

  
        for (; digitIndex >= 0; digitIndex--)
        {
            digits[digitIndex].enabled = false;
        }
    }
}

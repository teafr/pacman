using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Header("UI Digits")]
    public Image[] digits;         // Слоты для цифр (например, 6 Image объектов)
    public Sprite[] digitSprites;  // Спрайты цифр от 0 до 9

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

        // Отключаем лишние ведущие нули
        for (; digitIndex >= 0; digitIndex--)
        {
            digits[digitIndex].enabled = false;
        }
    }
}

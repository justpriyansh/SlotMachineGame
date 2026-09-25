using TMPro;
using UnityEngine;

public class SlotUI : MonoBehaviour
{
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private TMP_Text resultText;

    public void UpdateBalance(int balance)
    {
        balanceText.text = "Balance: " + balance;
    }

    public void ShowWin(int amount)
    {
        winText.text = "+" + amount;
        resultText.text = "WIN!";
    }

    public void ShowLose()
    {
        winText.text = "0";
        resultText.text = "TRY AGAIN";
    }
}
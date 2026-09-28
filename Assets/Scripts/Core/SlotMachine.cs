using System.Collections;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [Header("Reels")]
    [SerializeField] private Reel[] reels;

    [Header("Symbols")]
    [SerializeField] private SlotSymbol[] symbols;

    [Header("Game Settings")]
    [SerializeField] private int startingBalance = 1000;
    [SerializeField] private int spinCost = 10;

    [Header("Systems")]
    [SerializeField] private SlotRNG slotRNG;
    [SerializeField] private PayoutSystem payoutSystem;

    [Header("UI")]
    [SerializeField] private SlotUI slotUI;

    private int balance;
    private bool isSpinning;

    private void Start()
    {
        balance = startingBalance;

        foreach (Reel reel in reels)
        {
            reel.Setup(symbols);
        }

        // Show starting balance
        slotUI.UpdateBalance(balance);
    }

    public void Spin()
    {
        if (isSpinning)
        {
            return;
        }

        if (balance < spinCost)
        {
            return;
        }

        // Pay for the spin
        balance -= spinCost;

        // Update balance UI
        slotUI.UpdateBalance(balance);

        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;

        // Generate random result for each reel
        int firstResult =
            slotRNG.GetRandomSymbolIndex(symbols.Length);

        int secondResult =
            slotRNG.GetRandomSymbolIndex(symbols.Length);

        int thirdResult =
            slotRNG.GetRandomSymbolIndex(symbols.Length);

        // Start first reel
        StartCoroutine(
            reels[0].Spin(firstResult)
        );

        yield return new WaitForSeconds(0.2f);

        // Start second reel
        StartCoroutine(
            reels[1].Spin(secondResult)
        );

        yield return new WaitForSeconds(0.2f);

        // Start third reel and wait for it to finish
        yield return StartCoroutine(
            reels[2].Spin(thirdResult)
        );

        // Check result
        CheckResult();

        isSpinning = false;
    }

    private void CheckResult()
    {
        SlotSymbol first =
            reels[0].GetCurrentSymbol();

        SlotSymbol second =
            reels[1].GetCurrentSymbol();

        SlotSymbol third =
            reels[2].GetCurrentSymbol();

        int payout =
            payoutSystem.CalculatePayout(
                first,
                second,
                third
            );

        if (payout > 0)
        {
            // Add winnings
            balance += payout;

            // Update UI
            slotUI.UpdateBalance(balance);
            slotUI.ShowWin(payout);

        }
        else
        {
            // Show losing result
            slotUI.ShowLose();

        }
    }

    public int GetBalance()
    {
        return balance;
    }
}
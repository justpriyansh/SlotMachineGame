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

    private int balance;
    private bool isSpinning;

    private void Start()
    {
        balance = startingBalance;

        foreach (Reel reel in reels)
        {
            reel.Setup(symbols);
        }
    }

    public void Spin()
    {
        if (isSpinning)
            return;

        if (balance < spinCost)
            return;

        balance -= spinCost;

        StartCoroutine(SpinReels());
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;

        int firstResult = slotRNG.GetRandomSymbolIndex(symbols.Length);
        int secondResult = slotRNG.GetRandomSymbolIndex(symbols.Length);
        int thirdResult = slotRNG.GetRandomSymbolIndex(symbols.Length);

        StartCoroutine(reels[0].Spin(firstResult));

        yield return new WaitForSeconds(0.2f);

        StartCoroutine(reels[1].Spin(secondResult));

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(reels[2].Spin(thirdResult));

        CheckResult();

        isSpinning = false;
    }

    private void CheckResult()
    {
        SlotSymbol first = reels[0].GetCurrentSymbol();
        SlotSymbol second = reels[1].GetCurrentSymbol();
        SlotSymbol third = reels[2].GetCurrentSymbol();

        int payout = payoutSystem.CalculatePayout(
            first,
            second,
            third
        );

        if (payout > 0)
        {
            balance += payout;

            Debug.Log("WIN! Payout: " + payout);
        }
        else
        {
            Debug.Log("No Win");
        }
    }

    public int GetBalance()
    {
        return balance;
    }
}
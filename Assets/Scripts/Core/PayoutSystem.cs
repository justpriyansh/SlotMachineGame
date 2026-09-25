using UnityEngine;

public class PayoutSystem : MonoBehaviour
{
    public int CalculatePayout(SlotSymbol first , SlotSymbol second , SlotSymbol third)
    {
        if (first.symbolName == second.symbolName && second.symbolName == third.symbolName)
        {
            return first.payout;
        }

        return 0;
    }
}
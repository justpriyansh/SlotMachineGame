using UnityEngine;

public class SlotRNG : MonoBehaviour
{
    public int GetRandomSymbolIndex(int symbolCount)
    {
        return Random.Range(0, symbolCount);
    }
}
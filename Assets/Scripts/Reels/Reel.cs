using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [SerializeField] private Image symbolImage;
    [SerializeField] private float spinDuration = 1.5f;
    [SerializeField] private float symbolChangeSpeed = 0.06f;

    private SlotSymbol[] symbols;
    private int currentIndex;

    public void Setup(SlotSymbol[] availableSymbols)
    {
        symbols = availableSymbols;

        currentIndex = Random.Range(0, symbols.Length);

        UpdateSymbol();
    }

    public IEnumerator Spin(int targetIndex)
    {
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;

            currentIndex = Random.Range(0, symbols.Length);

            UpdateSymbol();

            yield return new WaitForSeconds(symbolChangeSpeed);
        }

        currentIndex = targetIndex;

        UpdateSymbol();
    }

    private void UpdateSymbol()
    {
        if (symbols == null || symbols.Length == 0)
            return;

        symbolImage.sprite = symbols[currentIndex].symbolSprite;
    }

    public SlotSymbol GetCurrentSymbol()
    {
        return symbols[currentIndex];
    }
}
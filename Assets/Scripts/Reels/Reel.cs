using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform symbolContainer;
    [SerializeField] private Image[] symbolImages;

    [Header("Reel Settings")]
    [SerializeField] private float symbolHeight = 100f;
    [SerializeField] private float spinSpeed = 1200f;
    [SerializeField] private float spinDuration = 1.5f;
    [SerializeField] private float stopDuration = 0.5f;

    private SlotSymbol[] symbols;
    private int currentIndex;
    private bool isSpinning;

    public void Setup(SlotSymbol[] availableSymbols)
    {
        symbols = availableSymbols;

        currentIndex = Random.Range(0, symbols.Length);

        SetupSymbols();
        SetCurrentSymbol();
    }

    private void SetupSymbols()
    {
        for (int i = 0; i < symbolImages.Length; i++)
        {
            int symbolIndex = i % symbols.Length;

            symbolImages[i].sprite = symbols[symbolIndex].symbolSprite;

            RectTransform rect = symbolImages[i].rectTransform;

            rect.anchoredPosition = new Vector2(
                0f,
                -i * symbolHeight
            );
        }
    }

    public IEnumerator Spin(int targetIndex)
    {
        if (isSpinning)
            yield break;

        isSpinning = true;

        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;

            MoveSymbols(spinSpeed);

            yield return null;
        }

        yield return StartCoroutine(
            StopOnTarget(targetIndex)
        );

        isSpinning = false;
    }

    private void MoveSymbols(float speed)
    {
        symbolContainer.anchoredPosition +=
            Vector2.down * speed * Time.deltaTime;

        float resetDistance =
            symbolImages.Length * symbolHeight;

        if (symbolContainer.anchoredPosition.y <= -resetDistance)
        {
            symbolContainer.anchoredPosition +=
                Vector2.up * resetDistance;
        }
    }

    private IEnumerator StopOnTarget(int targetIndex)
    {
        int safetyCounter = 0;

        while (currentIndex != targetIndex && safetyCounter < 100)
        {
            safetyCounter++;

            currentIndex++;

            if (currentIndex >= symbols.Length)
                currentIndex = 0;

            yield return MoveToNextSymbol();
        }

        yield return MoveToExactPosition();
    }

    private IEnumerator MoveToNextSymbol()
    {
        Vector2 startPosition = symbolContainer.anchoredPosition;

        Vector2 targetPosition =
            startPosition + Vector2.up * symbolHeight;

        float elapsed = 0f;

        while (elapsed < stopDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / stopDuration;

            t = Mathf.SmoothStep(0f, 1f, t);

            symbolContainer.anchoredPosition =
                Vector2.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            yield return null;
        }

        symbolContainer.anchoredPosition = targetPosition;
    }

    private IEnumerator MoveToExactPosition()
    {
        float targetY =
            -currentIndex * symbolHeight;

        Vector2 startPosition =
            symbolContainer.anchoredPosition;

        Vector2 targetPosition =
            new Vector2(
                startPosition.x,
                targetY
            );

        float elapsed = 0f;

        while (elapsed < stopDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / stopDuration;

            t = Mathf.SmoothStep(0f, 1f, t);

            symbolContainer.anchoredPosition =
                Vector2.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            yield return null;
        }

        symbolContainer.anchoredPosition =
            targetPosition;
    }

    private void SetCurrentSymbol()
    {
        symbolContainer.anchoredPosition =
            new Vector2(
                0f,
                -currentIndex * symbolHeight
            );
    }

    public SlotSymbol GetCurrentSymbol()
    {
        return symbols[currentIndex];
    }
}
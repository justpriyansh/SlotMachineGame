using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform symbolContainer;
    [SerializeField] private Image[] symbolImages;

    [Header("Settings")]
    [SerializeField] private float symbolHeight = 100f;
    [SerializeField] private float spinSpeed = 1000f;
    [SerializeField] private float spinDuration = 1.5f;

    private SlotSymbol[] symbols;
    private int currentIndex;

    public void Setup(SlotSymbol[] availableSymbols)
    {
        symbols = availableSymbols;

        SetupSymbols();

        currentIndex = Random.Range(0, symbols.Length);

        SetPosition(currentIndex);
    }

    private void SetupSymbols()
    {
        for (int i = 0; i < symbolImages.Length; i++)
        {
            int index = i % symbols.Length;

            symbolImages[i].sprite = symbols[index].symbolSprite;

            RectTransform symbolTransform = symbolImages[i].rectTransform;

            symbolTransform.anchorMin = new Vector2(0.5f, 0.5f);
            symbolTransform.anchorMax = new Vector2(0.5f, 0.5f);
            symbolTransform.pivot = new Vector2(0.5f, 0.5f);

            symbolTransform.anchoredPosition =
                new Vector2(0f, -i * symbolHeight);
        }

        symbolContainer.anchorMin = new Vector2(0.5f, 0.5f);
        symbolContainer.anchorMax = new Vector2(0.5f, 0.5f);
        symbolContainer.pivot = new Vector2(0.5f, 0.5f);

        symbolContainer.anchoredPosition = Vector2.zero;
    }

    public IEnumerator Spin(int targetIndex)
    {
        float elapsed = 0f;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;

            symbolContainer.anchoredPosition +=
                Vector2.down * spinSpeed * Time.deltaTime;

            LoopContainer();

            yield return null;
        }

        yield return StartCoroutine(MoveToTarget(targetIndex));

        currentIndex = targetIndex;

        SetPosition(currentIndex);
    }

    private void LoopContainer()
    {
        float totalHeight = symbolImages.Length * symbolHeight;

        if (symbolContainer.anchoredPosition.y <= -totalHeight)
        {
            symbolContainer.anchoredPosition +=
                Vector2.up * (totalHeight+ 300f);
        }
    }

    private IEnumerator MoveToTarget(int targetIndex)
    {
        float targetY = -targetIndex * symbolHeight;

        Vector2 startPosition = symbolContainer.anchoredPosition;

        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            float y = Mathf.Lerp(
                startPosition.y,
                targetY,
                t
            );

            symbolContainer.anchoredPosition =
                new Vector2(0f, y);

            yield return null;
        }

        symbolContainer.anchoredPosition =
            new Vector2(0f, targetY);
    }

    private void SetPosition(int index)
    {
        symbolContainer.anchoredPosition =
            new Vector2(
                0f,
                -index * symbolHeight + 300f
            );
    }

    public SlotSymbol GetCurrentSymbol()
    {
        return symbols[currentIndex];
    }
}
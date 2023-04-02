using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InfoTextController : MonoBehaviour
{
    private TextMeshProUGUI infoText;
    public float displayDuration = 1f;
    public float fadeDuration = 0.2f;

    void Start()
    {
        infoText = GetComponent<TextMeshProUGUI>();
        infoText.text = "";
    }

    public void DisplayInfoAfterHit(string sentence, string word, bool isHarmful)
    {
        ClearText();
        string highlightedWord = $"<color={(isHarmful ? "red" : "green")}>{word}</color>";
        string highlightedSentence = sentence.Replace(word, highlightedWord);
        StartCoroutine(FadeInAndDisplayText(highlightedSentence));
    }
    
    private void ClearText()
    {
        StopAllCoroutines();
        infoText.text = "";
        // StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeInAndDisplayText(string text)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, alpha);
            yield return null;
        }

        infoText.text = text;
        StartCoroutine(HideInfoTextAfterDelay());
    }

    private IEnumerator HideInfoTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            infoText.color = new Color(infoText.color.r, infoText.color.g, infoText.color.b, alpha);
            yield return null;
        }

        infoText.text = "";
    }
}
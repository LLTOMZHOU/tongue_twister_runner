using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUIController : MonoBehaviour
{
    public GameObject heartPrefab;
    public int initialLives = 3;
    public float heartSpacing = 10f;

    private List<Image> hearts;

    void Start()
    {
        hearts = new List<Image>();

        for (int i = 0; i < initialLives; i++)
        {
            GameObject heartInstance = Instantiate(heartPrefab, transform);
            RectTransform heartRectTransform = heartInstance.GetComponent<RectTransform>();
            heartRectTransform.anchoredPosition = new Vector2(i * (heartRectTransform.rect.width + heartSpacing), 0);
            hearts.Add(heartInstance.GetComponent<Image>());
        }
    }

    public void UpdateHearts(int lives)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].enabled = (i < lives);
        }
    }
}
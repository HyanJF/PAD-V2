using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeInController : MonoBehaviour
{
    [Header("Referencias")]
    public Image blackPanel;

    [Header("Duración del Fade")]
    public float fadeInDuration = 1.5f;

    private void Start()
    {
        if (blackPanel != null)
        {
            Color c = blackPanel.color;
            c.a = 1f;
            blackPanel.color = c;
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeInDuration);

            if (blackPanel != null)
            {
                Color c = blackPanel.color;
                c.a = alpha;
                blackPanel.color = c;
            }

            yield return null;
        }

        if (blackPanel != null)
        {
            Color c = blackPanel.color;
            c.a = 0f;
            blackPanel.color = c;
        }
    }
}

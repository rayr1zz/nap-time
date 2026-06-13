using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WhiteFadeOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeOutFromWhite());
    }

    IEnumerator FadeOutFromWhite()
    {
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        fadeImage.gameObject.SetActive(false);
    }
}
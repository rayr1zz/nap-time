using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutOnStart : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float time = 0f;

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        fadeImage.gameObject.SetActive(false);
    }
}
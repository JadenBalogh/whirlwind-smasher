using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasMenu : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.5f;

    private CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Reveal()
    {
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(Fade(1f));
    }

    public void Hide()
    {
        canvasGroup.blocksRaycasts = false;
        StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float target)
    {
        float fadeTimer = 0;
        while (fadeTimer < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1 - target, target, fadeTimer / fadeTime);

            fadeTimer += Time.deltaTime;
            yield return null;
        }
    }
}

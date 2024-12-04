using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PauseTween : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private AnimationCurve anim;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;

    public void StartAnimateIn()
    {
        StartCoroutine(AnimateIn());
    }

    private IEnumerator AnimateIn()
    {
        float timer = 0f;
        while(timer < anim.keys[anim.length - 1].time)
        {
            timer += Time.unscaledDeltaTime;
            obj.GetComponent<RectTransform>().anchoredPosition = startPos + (endPos - startPos) * anim.Evaluate(timer);
            yield return null;
        }
    }

    public void StartAnimateOut()
    {
        StartCoroutine(AnimateOut());
    }

    private IEnumerator AnimateOut()
    {
        float timer = anim.keys[anim.length - 1].time;
        while(timer > 0)
        {
            timer -= Time.unscaledDeltaTime;
            obj.GetComponent<RectTransform>().anchoredPosition = startPos + (endPos - startPos) * anim.Evaluate(timer);
            yield return null;
        }
    }
}
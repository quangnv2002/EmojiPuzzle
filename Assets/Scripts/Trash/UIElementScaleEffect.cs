using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIElementScaleEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Enable");
        gameObject.transform.DOScale(transform.localScale * 1.5f, 3f).OnComplete(() =>
           {
               transform.localScale = Vector3.one;
               
           });
    }
    private void OnDisable() {
        DOTween.Kill(gameObject);
          transform.localScale = Vector3.one;
    }
}

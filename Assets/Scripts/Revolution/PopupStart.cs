using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupStart : PopupBase
{
    public static PopUpName popup_start = PopUpName.Start;
    [SerializeField] Button start_btn;
    //[SerializeField] GameObject _text;


    public override PopUpName getPopUpName()
    {
        return popup_start;
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }

    private void Start()
    {
        AssignEventOnClickStart();
        start_btn.gameObject.transform.DOScale(start_btn.gameObject.transform.localScale * 1.2f, 1)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    //    _text.gameObject.transform.DOScale(_text.gameObject.transform.localScale * 1.2f, 1.5f)
    //.SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    public void AssignEventOnClickStart()
    {
        start_btn.onClick.AddListener(() =>
        {
            DestroyPopup();
            revoGameController.Instance.LoadCurrentLevel();
        });
    }


}

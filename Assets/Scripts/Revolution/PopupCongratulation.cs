using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupCongratulation : PopupBase
{
    public static PopUpName popup_congratulation = PopUpName.Congratulation;
    [SerializeField] Button next_btn;
    [SerializeField] Button award_btn;
    //[SerializeField] GameObject _text;


    public override PopUpName getPopUpName()
    {
        return popup_congratulation;
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(0.2f);
    }

    private void Start()
    {
        AssignEventOnClickNext();
        AssignEventOnClickAward();
        award_btn.gameObject.transform.DOScale(award_btn.gameObject.transform.localScale * 1.2f, 1.5f)
        .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void AssignEventOnClickNext()
    {
        next_btn.onClick.AddListener(() =>
        {
            DestroyPopup();
            Destroy(revoGameController.Instance.getLevel());
            Debug.Log("Da xoa");

            revoGameController.max_level = 1;
            Debug.Log(revoGameController.max_level);
            PlayerPrefs.SetInt(ConstantString.level, revoGameController.max_level);
            AllPopupHandle.Instance.InitPopup(PopUpName.Start);
        });
    }
    private void AssignEventOnClickAward()
    {
        award_btn.onClick.AddListener(() =>
        {
            AdsManager.Instance.RequestRewardAd();
            if (AdsManager.Instance.GetRewardedAd().IsLoaded())
            {
                AdsManager.Instance.GetRewardedAd().Show();
            }
        });
    }
}

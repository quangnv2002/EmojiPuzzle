using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using DG.Tweening;

public class PopupLose : PopupBase
{
    [SerializeField] GameObject lose_sprite;
    [SerializeField] Button retry_btn;
    [SerializeField] Button award_btn;
    [SerializeField] Button home_btn;
    public static PopUpName popup_lose = PopUpName.Lose;


    public override PopUpName getPopUpName()
    {
        return popup_lose;
    }

    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    IEnumerator DelayTime(float _time)
    {
        yield return new WaitForSeconds(_time);
    }

    private void Start()
    {
        AssignEventOnClickRetry();
        AssignEventOnClickAward();
        lose_sprite.transform.DOScale(transform.localScale * 1.3f, 2f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            transform.localScale = Vector3.one;
            DelayTime(0.2f);
            award_btn.gameObject.transform.DOScale(award_btn.gameObject.transform.localScale * 1.2f, 1.3f)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        });
    }


    private void AssignEventOnClickRetry()
    {
        retry_btn.onClick.AddListener(() =>
        {
            DestroyPopup();
            revoGameController.Instance.LoadCurrentLevel();
            Debug.Log("click retry button");

            MobileAds.Initialize(initstatus => { });
            AdsManager.Instance.RequestInterstitial();
            if (AdsManager.Instance.GetInterstitialAd().IsLoaded())
            {
                AdsManager.Instance.GetInterstitialAd().Show();
            }
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

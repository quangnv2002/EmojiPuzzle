using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupWin : PopupBase
{
    [SerializeField] Button next_level_btn;
    [SerializeField] Button home_btn;
    [SerializeField] Button award_btn;

    [SerializeField] GameObject win_sprite;

    public static PopUpName popup_win = PopUpName.Win;


    public override PopUpName getPopUpName()
    {
        return popup_win;
    }

    public override void Show()
    {
        base.Show();
        //res_sprite[0].gameObject.SetActive(false);
        //res_sprite[1].gameObject.SetActive(true);
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
        AssignEventOnClickNextLevel();
        AssignEventOnClickAward();
        //WinAnimation();
        win_sprite.transform.DOScale(transform.localScale * 1.3f, 2f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            transform.localScale = Vector3.one;
            DelayTime();
            award_btn.gameObject.transform.DOScale(award_btn.gameObject.transform.localScale * 1.2f, 1.5f)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        });




    }
    IEnumerator WinAnimation()
    {
        win_sprite.transform.DOScale(transform.localScale * 1.5f, 2f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            transform.localScale = Vector3.one;
        });
        yield return new WaitForSeconds(0.2f);
    }

    private void AssignEventOnClickNextLevel()
    {
        next_level_btn.onClick.AddListener(() =>
        {
            DestroyPopup();
            Destroy(revoGameController.Instance.getLevel());
            revoGameController.Instance.LoadNextLevel();
            Debug.Log("click next button");
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

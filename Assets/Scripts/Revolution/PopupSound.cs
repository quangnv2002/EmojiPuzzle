using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupSound : PopupBase
{
    [SerializeField] RectTransform panel;

    [SerializeField] Button music_btn;
    [SerializeField] Button sfx_btn;

    [SerializeField] Slider music_slider;
    [SerializeField] Slider sfx_slider;

    [SerializeField] Sprite[] music_change_icon;
    [SerializeField] Sprite[] sfx_change_icon;

    [SerializeField] Button exit_btn;
    
    public static PopUpName popup_sound = PopUpName.Sound;

    public override PopUpName getPopUpName()
    {
        return popup_sound;
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
        sfx_slider.value = SoundManager.Instance.SFXVolume;
        music_slider.value = SoundManager.Instance.MusicVolume;


        music_btn.GetComponent<Image>().sprite = music_change_icon[SoundManager.Instance.CheckMusicMute()];
        sfx_btn.GetComponent<Image>().sprite = sfx_change_icon[SoundManager.Instance.CheckSFXMute()];

        AssignEventOnClickMusic();
        AssignEventOnClickSFX();


        AssignEventOnClickSFXSlider();
        AssignEventOnClickMusicSlider();

        AssignEventOnClickExit();
        PanelFadeIn();
    }


    private void PanelFadeIn()
    {
        panel.DOAnchorPos(new Vector2(0f, 0f), 1f, false).SetEase(Ease.OutElastic);
    }

    private void AssignEventOnClickMusic()
    {
        music_btn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ToggleMusic();
            bool isMusicOn = SoundManager.Instance.CheckMusicMute() == 0;
            music_slider.value = isMusicOn ? 1 : 0;
            music_btn.GetComponent<Image>().sprite = music_change_icon[SoundManager.Instance.CheckMusicMute()];
        });
    }
    private void AssignEventOnClickSFX()
    {
        sfx_btn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ToggleSFX();
            bool isSoundOn = SoundManager.Instance.CheckSFXMute() == 0;
            sfx_slider.value = isSoundOn ? 1 : 0;
            sfx_btn.GetComponent<Image>().sprite = sfx_change_icon[SoundManager.Instance.CheckSFXMute()];
        });
    }


    private void AssignEventOnClickMusicSlider()
    {
        music_slider.onValueChanged.AddListener(delegate
        {
            SoundManager.Instance.ChangeMusicVolume(music_slider.value);
        });
    }
    private void AssignEventOnClickSFXSlider()
    {
        sfx_slider.onValueChanged.AddListener(delegate
        {
            SoundManager.Instance.ChangeSFXVolume(sfx_slider.value);

        });
    }

    private void AssignEventOnClickExit()
    {
        exit_btn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX("ClickButton");
            DestroyPopup();
        }
        );
    }
}

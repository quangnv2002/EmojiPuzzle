using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class AdsManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public InterstitialAd GetInterstitialAd()
    {
        return interstitial;
    }

    public RewardedAd GetRewardedAd()
    {
        return rewardedAd;
    }


    public static AdsManager Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    [Header("ID TEST")]
    public string bannerIdTest = "ca-app-pub-3940256099942544/6300978111";
    public string interstitialIdTest = "ca-app-pub-3940256099942544/8691691433";
    public string rewardIdTest = "ca-app-pub-3940256099942544/5224354917";

    [Header("ID ANDROID")]
    [SerializeField] string bannerIdAndroid = "";
    [SerializeField] string interstitialIdAndroid = "";
    [SerializeField] string rewardIdAndroid = "";

    private UnityAction earnedRewardCallback;

    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();

        RequestInterstitial();
        RequestRewardAd();
    }

    public void RequestRewardAd()
    {
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = rewardIdAndroid;
#elif UNITY_IPHONE
            adUnitId = rewardIdIOS;
#else
            adUnitId = "unexpected_platform";
#endif

#if EnviromentTest
            this.rewardedAd = new RewardedAd(rewardIdTest);
#else
        this.rewardedAd = new RewardedAd(adUnitId);
#endif

        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        this.rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;
        this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;

        // Create an empty ad request.

        AdRequest request = new AdRequest.Builder()
        .Build();
        this.rewardedAd.LoadAd(request);
    }

    private void RewardedAd_OnAdClosed(object sender, EventArgs e)
    {
        RequestRewardAd();
        RequestInterstitial();
    }

    private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        //Debug.LogError(" ========== ADS FAIL TO SHOW " + e.Message);
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdErrorEventArgs e)
    {
        //Debug.LogError(" ========== ADS FAIL TO LOAD " + e.Message);
    }


    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {

    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        earnedRewardCallback?.Invoke();
    }


    public void ShowReward(UnityAction callback)
    {
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            if (earnedRewardCallback != null) earnedRewardCallback = null;
            earnedRewardCallback = callback;
        }
    }

    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = bannerIdAndroid;
#elif UNITY_IPHONE
            string adUnitId = bannerIdIOS;
#else
            string adUnitId = "unexpected_platform";
#endif
        AdSize _adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
#if EnviromentTest
            bannerView = new BannerView(bannerIdTest, adaptiveSize, AdPosition.Bottom);
#else
        bannerView = new BannerView(adUnitId, _adaptiveSize, AdPosition.Bottom);
#endif

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += OnBannerLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += OnBannerFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += OnBannerOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += OnBannerClosed;

        AdRequest request = new AdRequest.Builder()
        .Build();

        bannerView.LoadAd(request);
        //FirebaseManager.Instance.LogShow_Banner_Ads();
        //FirebaseManager.Instance.LogShow_Banner_And_Interstitial_Ads();
    }

    private void OnBannerLoaded(object sender, EventArgs args)
    {
        //Debug.unityLogger.Log("OnBannerLoaded event received");
    }

    private void OnBannerFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //Debug.unityLogger.Log("OnBannerFailedToLoad event received with message: " + args.LoadAdError.GetMessage());
    }

    private void OnBannerOpened(object sender, EventArgs args)
    {
        //Debug.unityLogger.Log("OnBannerOpened event received");
    }

    private void OnBannerClosed(object sender, EventArgs args)
    {
        //Debug.unityLogger.Log("OnBannerClosed event received");
    }

    public void DestroyBanner()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }



    public void RequestInterstitial()
    {

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
}


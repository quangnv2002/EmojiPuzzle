using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Linq;

public class revoGameController : MonoBehaviour
{
    public static revoGameController Instance { get; set; }

    private int score = 0;
    public static int max_level;
    [SerializeField] float scale_item_rate = .5f;
    [SerializeField] float scale_item_duration = .5f;

    [SerializeField] ItemData item_data;
    [SerializeField] LineRenderer line;

    private ItemClick item_1;
    private ItemClick item_2;
    int valid_turn = 0;
    private Vector2 linePoint_1;
    private Vector2 linePoint_2;

    [SerializeField] Dictionary<LineRenderer, bool> line_dic = new Dictionary<LineRenderer, bool>();
    private bool is_finish;
    [Header("LEVEL")]
    [SerializeField] GameObject[] level_prefab;
    private GameObject level;

    public GameObject getLevel()
    {
        return level;
    }
    [SerializeField] Button settings_btn;
    [SerializeField] Button ads_btn;
    [SerializeField] ParticleSystem[] particle_system;


    private Vector3 origin_scale;
    private Color line_color;

    private List<GameObject> connected_dic = new List<GameObject>();
    private int guide_des;

    private void Awake()
    {
        Instance = this;
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(3);
    }

    public void LoadCurrentLevel()
    {
        if (level != null) Destroy(level.gameObject);
        foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
        {
            Destroy(item.Key.gameObject);
        }
        line_dic.Clear();
        connected_dic.Clear();
        score = 0;
        valid_turn = 0;
        SoundManager.Instance.PlayMusic("Background");
        level = Instantiate(level_prefab[max_level - 1]);
        is_finish = false;
        item_1 = null;
        item_2 = null;
        foreach (ObjectController obj in level.GetComponent<LevelController>().GetArrayLeft())
        {
            for (int i = 0; i < item_data.GetPairList().Length; i++)
            {
                if (obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic1() || obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic2())
                {
                    obj.SetItemID(i);
                }
            }
            obj.GetComponent<PairLine>().SetObject1(obj.gameObject);
            obj.GetComponent<PairLine>().SetObject2(obj.gameObject);
        }
        foreach (ObjectController obj in level.GetComponent<LevelController>().GetArrayRight())
        {
            for (int i = 0; i < item_data.GetPairList().Length; i++)
            {
                if (obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic1() || obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic2())
                {
                    obj.SetItemID(i);
                }
            }
            obj.GetComponent<PairLine>().SetObject1(obj.gameObject);
            obj.GetComponent<PairLine>().SetObject2(obj.gameObject);
        }
        foreach (ObjectController obj in level.GetComponent<LevelController>().GetArrayLeft())
        {
            connected_dic.Add(obj.gameObject);
        }
        Debug.Log(max_level);
    }
    public void LoadNextLevel()
    {
        Destroy(level.gameObject);
        foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
        {
            Destroy(item.Key.gameObject);
        }
        line_dic.Clear();
        connected_dic.Clear();
        score = 0;
        valid_turn = 0;
        SoundManager.Instance.PlayMusic("Background");
        LoadCurrentLevel();
    }

    private void Start()
    {
        guide_des = 0;
        AllPopupHandle.Instance.InitPopup(PopUpName.Start);
        max_level = PlayerPrefs.GetInt(ConstantString.level, 1);
        Debug.Log("Level cao nhat la : " + max_level);
        AssignEventOnClickSettings();
        AssignEventOnClickAds();
        origin_scale = new Vector3(1f, 1f, 1f);
        line_color = new Color32(0, 128, 0, 255);

    }

    // UI Controller
    private void AssignEventOnClickSettings()
    {
        settings_btn.onClick.AddListener(() =>
        {
            AllPopupHandle.Instance.InitPopup(PopUpName.Sound);
            SoundManager.Instance.PlaySFX("ClickButton");
        });
    }

    private void AssignEventOnClickAds()
    {
        ads_btn.onClick.AddListener(() =>
        {
            AdsManager.Instance.RequestRewardAd();
            GuideObjectController.Instance.DeActiveGuide(); //HỦY HƯỚNG DẪN
            UnityAction earnedReward = () =>
            {
                Debug.Log("Bat dieu huong");
                guide_des = 0;
                foreach (GameObject obj in connected_dic)
                {
                    if (obj.gameObject.GetComponent<PairLine>().GetPairLine_Line() != null)
                    {
                        guide_des++;
                    }
                    else
                    {
                        break;
                    }
                }
                Debug.Log(guide_des);
                GuideObjectController.Instance.ActiveGuide(guide_des);

            };
            AdsManager.Instance.ShowReward(earnedReward);
        });
    }

    public void OnClickPoint(ItemSide side, int itemId, int GetItemID, Vector3 GetPos, GameObject GetGameObject)
    {
        //tat huong dan
        GuideObjectController.Instance.DeActiveGuide();
        int total = level.GetComponent<LevelController>().GetArrayLeft().Length;
        if (score < 0) score = 0;
        if (valid_turn < 0) valid_turn = 0;
        if (is_finish == false)
        {
            if (item_1 == null)
            {
                item_1 = new ItemClick();
                SoundManager.Instance.PlaySFX("ClickButton");
                item_1.itemId = itemId;
                item_1.itemSide = side;
                item_1.gameObject = GetGameObject;

                //linePoint_1 = GetPos ;
                linePoint_1 = new Vector3(GetPos.x - 0.5f, GetPos.y - 0.25f, GetPos.z);
                //Debug.Log(linePoint_1);
                //Debug.Log(item_1.gameObject.name);
                if (item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line() != null)
                {
                    valid_turn--;
                    if (item_1.gameObject.GetComponent<PairLine>().GetScored() == true)
                    {
                        score--;
                    }
                    line_dic.Remove(item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line());

                    Destroy(item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line().gameObject);
                    // item 1 co duong thi open 1 & delete 2 cu
                    item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    item_1.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    item_1.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.DOScale(origin_scale, scale_item_duration).OnComplete(() => item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.localScale = Vector3.one);
                    item_1.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.DOScale(origin_scale, scale_item_duration).OnComplete(() => item_1.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.localScale = Vector3.one); ;
                    item_1.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).OnComplete(() => item_1.gameObject.transform.localScale = Vector3.one); ;


                    //13/1/23
                    item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject
                        .GetComponent<PairLine>().SetObject1(item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject);
                    item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject
                        .GetComponent<PairLine>().SetObject2(item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject);
                    item_1.gameObject.GetComponent<PairLine>().SetObject1(item_1.gameObject);
                    item_1.gameObject.GetComponent<PairLine>().SetObject2(item_1.gameObject);

                    //12/1/23
                    //for (int i = 0; i < connected_dic.Count; i++)
                    //{
                    //    var item = connected_dic.ElementAt(i);
                    //    if (item.Key.GetComponent<PairLine>().GetPairLine_Line() != null)
                    //    {
                    //        connected_dic[item.Key] = true;
                    //    }
                    //    else
                    //    {
                    //        connected_dic[item.Key] = false;
                    //    }
                    //}
                }
                else // item 1 object == null
                {
                    item_1.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    item_1.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine).OnComplete(() => item_1.gameObject.transform.localScale = Vector3.one);

                    //13/1/23
                    item_1.gameObject.GetComponent<PairLine>().SetObject1(item_1.gameObject);
                    item_1.gameObject.GetComponent<PairLine>().SetObject2(item_1.gameObject);
                }

            }
            else
            {
                item_2 = new ItemClick();
                SoundManager.Instance.PlaySFX("ClickButton");
                item_2.itemId = itemId;
                item_2.itemSide = side;
                item_2.gameObject = GetGameObject;
                //linePoint_2 = GetPos;
                linePoint_2 = new Vector3(GetPos.x - 0.5f, GetPos.y - 0.25f, GetPos.z);


                if (item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line() != null)
                {
                    valid_turn--;
                    if (item_2.gameObject.GetComponent<PairLine>().GetScored() == true)
                    {
                        score--;
                    }

                    line_dic.Remove(item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line());
                    Destroy(item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line().gameObject);
                    item_2.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    item_2.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                    //13/1/23
                    item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject
                        .GetComponent<PairLine>().SetObject1(item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject);
                    item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject
                        .GetComponent<PairLine>().SetObject2(item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject);
                    item_2.gameObject.GetComponent<PairLine>().SetObject1(item_2.gameObject);
                    item_2.gameObject.GetComponent<PairLine>().SetObject2(item_2.gameObject);


                    //13/1/23
                    //for (int i = 0; i < connected_dic.Count; i++)
                    //{
                    //    var item = connected_dic.ElementAt(i);
                    //    if (item.Key.gameObject.name == item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject.name)
                    //    {
                    //        connected_dic[item.Key] = true;
                    //    }
                    //    else
                    //    {
                    //        connected_dic[item.Key] = false;
                    //    }
                    //}

                }
                else 
                {
                    item_2.gameObject.GetComponent<PairLine>().SetObject1(item_2.gameObject);
                    item_2.gameObject.GetComponent<PairLine>().SetObject2(item_2.gameObject);
                }
                if (item_2.itemSide == item_1.itemSide && item_2.itemId == item_1.itemId)
                {
                    item_1.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    item_1.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine).OnComplete(() => item_1.gameObject.transform.localScale = Vector3.one);


                    item_1 = null;
                    item_2 = null;
                }
                else if (item_1.itemSide == item_2.itemSide)
                {
                    item_1.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    item_2.gameObject.transform.GetChild(0).gameObject.SetActive(false);

                    item_1.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine).OnComplete(() => item_1.gameObject.transform.localScale = Vector3.one);
                    item_2.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine).OnComplete(() => item_2.gameObject.transform.localScale = Vector3.one);


                    Debug.Log("Chon cung ben");
                    item_1 = null;
                    //item_1 = item_2;
                    item_2 = null;
                }
                else if (item_1.itemSide != item_2.itemSide)
                {
                    item_2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    item_2.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine)
                        .OnComplete(() =>
                        {
                            item_2.gameObject.transform.localScale = Vector3.one;
                            item_1 = null;
                            item_2 = null;
                        });

                    valid_turn++;
                    LineRenderer new_line = Instantiate(line);
                    CreateLine(new_line);
                    new_line.gameObject.SetActive(true);
                    new_line.SetPosition(0, linePoint_1);
                    new_line.SetPosition(1, linePoint_2);

                    //13/1/23
                    //item_1.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, true);
                    //item_2.gameObject.GetComponent<PairLine>().SetPairLine(item_2.gameObject, item_1.gameObject, new_line, true);
                    //item_2.gameObject.GetComponent<PairLine>().PrintPairLine();

                    //connected_dic[item_1.itemSide == ItemSide.Left ? item_1.gameObject : item_2.gameObject] = true;
                    //if(item_1.itemSide == ItemSide.Left)
                    //{
                    //    connected_dic[item_1.gameObject] = true;
                    //}
                    //else
                    //{
                    //    connected_dic[item_2.gameObject] = true;
                    //}

                    if (item_1.itemId == item_2.itemId)
                    {
                        score++;
                        item_1.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, true);
                        item_2.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, true);
                        //item_2.gameObject.GetComponent<PairLine>().PrintPairLine();
                        line_dic.Add(new_line, true);
                    }
                    else
                    {
                        line_dic.Add(new_line, false);
                        item_1.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, false);
                        item_2.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, false);
                        //item_2.gameObject.GetComponent<PairLine>().PrintPairLine();
                    }
                }
            }
        }

        if (score == total && valid_turn == total)
        {
            Debug.Log("Ban da thang");
            is_finish = true;
            foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
            {
                ChangeLine(item.Key, item.Value);
            }
            StartCoroutine(DelayTime());

            if (max_level < 10)
            {
                AllPopupHandle.Instance.InitPopup(PopUpName.Win);
                SoundManager.Instance.PlaySFX("Win");
                SoundManager.Instance.music_source.Stop();
                particle_system[0].Play();
                particle_system[1].Play();
                max_level++;
                PlayerPrefs.SetInt(ConstantString.level, max_level);

            }
            else if (max_level == 10)
            {
                AllPopupHandle.Instance.InitPopup(PopUpName.Congratulation);

                SoundManager.Instance.music_source.Stop();
                SoundManager.Instance.PlaySFX("Win");
                particle_system[0].Play();
                particle_system[1].Play();

            }
        }
        else if (score < total && valid_turn == total)
        {
            Debug.Log("Thua roi");
            is_finish = true;
            foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
            {
                ChangeLine(item.Key, item.Value);
            }
            StartCoroutine(DelayTime());
            AllPopupHandle.Instance.InitPopup(PopUpName.Lose);

            SoundManager.Instance.PlaySFX("Lose");
            SoundManager.Instance.music_source.Stop();
        }
    }

    private void CreateLine(LineRenderer line)
    {
        line.startColor = line_color;
        line.endColor = line_color;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
    }
    private void ChangeLine(LineRenderer line, bool res)
    {
        switch (res)
        {
            case true:
                line.startColor = Color.green;
                line.endColor = Color.green;
                break;
            case false:
                line.startColor = Color.red;
                line.endColor = Color.red;
                break;
            default:
        }
    }
}


[System.Serializable]
public class ItemClick
{
    public int itemId;
    public ItemSide itemSide;
    public GameObject gameObject;
}

public enum ItemSide
{
    Left,
    Right
}



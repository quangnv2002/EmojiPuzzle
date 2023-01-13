// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Rendering;
// using System;
// using UnityEngine.UI;
// using DG.Tweening;
// public class GameController : MonoBehaviour
// {
//     public static GameController Instance { get; set; }

//     private int score = 0;
//     private int max_level;
//     [SerializeField] float scale_item_rate = .5f;
//     [SerializeField] float scale_item_duration = .5f;

//     [SerializeField] ItemData item_data;
//     [SerializeField] LineRenderer line;

//     private ItemClick item_1;
//     private ItemClick item_2;
//     int valid_turn = 0;
//     private Vector2 linePoint_1;
//     private Vector2 linePoint_2;

//     [SerializeField] Dictionary<LineRenderer, bool> line_dic = new Dictionary<LineRenderer, bool>();
//     private bool is_finish;
//     [Header("Level")]
//     [SerializeField] GameObject[] level_prefab;

//     [SerializeField] int cur_level;
//     private GameObject level;

//     [Header("UI")]
//     [SerializeField] Canvas canvas;
//     [SerializeField] GameObject panel_menu;
//     [SerializeField] Button[] buttonLevels;
//     [SerializeField] GameObject panel_end_game;
//     //[SerializeField] Text res_text;
//     [SerializeField] Button next_level_btn;
//     [SerializeField] Button retry_btn;
//     [SerializeField] Button home_btn;
//     private Vector3 home_btn_pos;
//     [SerializeField] Button settings_btn;
//     [SerializeField] GameObject panel_sound;
//     [SerializeField] Button music_btn;
//     [SerializeField] Button sfx_btn;

//     [SerializeField] Slider music_slider;
//     [SerializeField] Slider sfx_slider;

//     [SerializeField] Sprite[] music_change_icon;
//     [SerializeField] Sprite[] sfx_change_icon;

//     [SerializeField] ParticleSystem[] particle_system;

//     [SerializeField] GameObject[] res_sprite;

//     private Vector3 origin_scale;
//     private void Awake()
//     {
//         Instance = this;
//     }
//     IEnumerator DelayTime()
//     {
//         yield return new WaitForSeconds(3);
//     }

//     private void OnEnable()
//     {
//         // AssignEventOnClickLevel();
//         // AssignEventOnClickNextLevel();
//         // AssignEventOnClickRetry();
//         // AssignEventOnClickHome();
//         // AssignEventOnClickSettings();
//         // AssignEventOnClickMusic();
//         // AssignEventOnClickSFX();
//         // AssignEventOnClickMusicSlider();
//     }
//     private void OnDisable()
//     {

//     }
//     private void Start()
//     {
//         max_level = PlayerPrefs.GetInt("-Level-", 1);
//         Debug.Log("Level cao nhat la : " + PlayerPrefs.GetInt("-Level-", 1));
//         canvas.gameObject.SetActive(true);
//         home_btn_pos = home_btn.gameObject.transform.position;
//         panel_end_game.gameObject.SetActive(false);
//         panel_menu.gameObject.SetActive(true);
//         panel_sound.gameObject.SetActive(false);
//         music_slider.value = 0.5f;
//         sfx_slider.value = 0.5f;
//         AssignEventOnClickLevel();
//         AssignEventOnClickNextLevel();
//         AssignEventOnClickRetry();
//         AssignEventOnClickHome();
//         AssignEventOnClickSettings();
//         AssignEventOnClickMusic();
//         AssignEventOnClickSFX();
//         origin_scale = new Vector3(1f, 1f, 1f);

//         foreach (GameObject obj in res_sprite)
//         {
//             obj.gameObject.SetActive(false);
//             obj.gameObject.SetActive(false);
//         }

//     }
//     private void Update()
//     {
//         AssignEventOnClickMusicSlider();
//         AssignEventOnClickSFXSlider();

//         for (int i = 0; i < buttonLevels.Length; i++)
//         {
//             if (i < max_level)
//             {
//                 buttonLevels[i].GetComponent<Button>().interactable = true;
//             }
//             else buttonLevels[i].GetComponent<Button>().interactable = false;

//         }
//     }

//     private void AssignEventOnClickLevel()
//     {
//         for (int i = 0; i < buttonLevels.Length; i++)
//         {
//             int levelIndex = i;
//             buttonLevels[i].onClick.AddListener(() =>
//             {
//                 OnSelectLevel(levelIndex + 1);
//             });
//         }
//     }

//     private void AssignEventOnClickRetry()
//     {
//         retry_btn.onClick.AddListener(() =>
//         {
//             Destroy(level);
//             OnSelectLevel(cur_level);
//             foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
//             {
//                 Destroy(item.Key);
//             }
//             line_dic.Clear();
//             score = 0;
//             valid_turn = 0;
//             SoundManager.Instance.PlayMusic("Background");
//         });

//     }

//     private void AssignEventOnClickNextLevel()
//     {
//         next_level_btn.onClick.AddListener(() =>
//         {
//             Destroy(level);
//             OnSelectLevel(cur_level + 1);
//             foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
//             {
//                 Destroy(item.Key);
//             }
//             line_dic.Clear();
//             score = 0;
//             valid_turn = 0;
//             SoundManager.Instance.PlayMusic("Background");
//         })
// ;
//     }

//     private void AssignEventOnClickHome()
//     {
//         home_btn.onClick.AddListener(() =>
//         {
//             panel_end_game.gameObject.SetActive(false);
//             panel_menu.gameObject.SetActive(true);
//             home_btn.gameObject.transform.position = home_btn_pos;

//             Destroy(level);
//             foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
//             {
//                 Destroy(item.Key);
//             }
//             line_dic.Clear();
//             score = 0;
//             valid_turn = 0;
//             SoundManager.Instance.PlayMusic("Background");
//         });
//     }
//     private void AssignEventOnClickSettings()
//     {
//         settings_btn.onClick.AddListener(() =>
//         {
//             bool is_active = panel_sound.gameObject.activeSelf;
//             panel_sound.gameObject.SetActive(!is_active);
//         });

//     }

//     private void AssignEventOnClickMusic()
//     {
//         music_btn.onClick.AddListener(() =>
//         {

//             SoundManager.Instance.ToggleMusic();
//             //Debug.Log(music_change_icon.ContainsKey(true));
//             music_btn.GetComponent<Image>().sprite = music_change_icon[SoundManager.Instance.CheckMusicMute()];
//         });
//     }
//     private void AssignEventOnClickSFX()
//     {
//         sfx_btn.onClick.AddListener(() =>
//         {
//             SoundManager.Instance.ToggleSFX();
//             sfx_btn.GetComponent<Image>().sprite = sfx_change_icon[SoundManager.Instance.CheckSFXMute()];
//         });
//     }

//     private void AssignEventOnClickMusicSlider()
//     {
//         SoundManager.Instance.MusicVolume(music_slider.value);
//     }
//     private void AssignEventOnClickSFXSlider()
//     {
//         SoundManager.Instance.SFXVolume(sfx_slider.value);
//     }
//     private void OnSelectLevel(int levelInput)
//     {
//         if (PlayerPrefs.GetInt("-Level-", 1) >= levelInput)
//         {
//             level = Instantiate(level_prefab[levelInput - 1]);
//             cur_level = levelInput;
//             is_finish = false;
//             panel_end_game.gameObject.SetActive(false);
//             panel_menu.gameObject.SetActive(false);
//             item_1 = null;
//             item_2 = null;
//             foreach (ObjectController obj in level.GetComponent<LevelController>().GetArrayLeft())
//             {
//                 for (int i = 0; i < item_data.GetPairList().Length; i++)
//                 {
//                     if (obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic1() || obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic2())
//                     {
//                         obj.SetItemID(i);
//                     }
//                 }
//             }
//             foreach (ObjectController obj in level.GetComponent<LevelController>().GetArrayRight())
//             {
//                 for (int i = 0; i < item_data.GetPairList().Length; i++)
//                 {
//                     if (obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic1() || obj.GetComponent<SpriteRenderer>().sprite == item_data.GetPairList()[i].GetPic2())
//                     {
//                         obj.SetItemID(i);
//                     }
//                 }
//             }
//             Debug.Log(levelInput + "/" + max_level);
//         }
//     }

//     public void OnClickPoint(ItemSide side, int itemId, int GetItemID, Vector3 GetPos, GameObject GetGameObject)
//     {
//         int total = level.GetComponent<LevelController>().GetArrayLeft().Length;
//         if (score < 0) score = 0;
//         if (valid_turn < 0) valid_turn = 0;
//         if (is_finish == false)
//         {
//             if (item_1 == null)
//             {
//                 item_1 = new ItemClick();
//                 SoundManager.Instance.PlaySFX("ClickButton");
//                 item_1.itemId = itemId;
//                 item_1.itemSide = side;
//                 item_1.gameObject = GetGameObject;
//                 Debug.Log(origin_scale);

//                 linePoint_1 = GetPos;
//                 Debug.Log(item_1.gameObject.name);
//                 if (item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line() != null)
//                 {
//                     valid_turn--;
//                     if (item_1.gameObject.GetComponent<PairLine>().GetScored() == true)
//                     {
//                         score--;
//                     }
//                     line_dic.Remove(item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line());

//                     Destroy(item_1.gameObject.GetComponent<PairLine>().GetPairLine_Line());
//                     // item 1 co duong thi open 1 & delete 2 cu
//                     item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.GetChild(0).gameObject.SetActive(false);
//                     item_1.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.GetChild(0).gameObject.SetActive(false);
//                     item_1.gameObject.transform.GetChild(0).gameObject.SetActive(true);

//                     item_1.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_1.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_1.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine);

//                 }
//                 else
//                 {
//                     item_1.gameObject.transform.GetChild(0).gameObject.SetActive(true);

//                     item_1.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine);
//                 }

//             }
//             else
//             {
//                 item_2 = new ItemClick();
//                 SoundManager.Instance.PlaySFX("ClickButton");
//                 item_2.itemId = itemId;
//                 item_2.itemSide = side;
//                 item_2.gameObject = GetGameObject;
//                 linePoint_2 = GetPos;


//                 if (item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line() != null)
//                 {
//                     valid_turn--;
//                     if (item_2.gameObject.GetComponent<PairLine>().GetScored() == true)
//                     {
//                         score--;
//                     }

//                     line_dic.Remove(item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line());
//                     Destroy(item_2.gameObject.GetComponent<PairLine>().GetPairLine_Line());
//                     item_2.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.GetChild(0).gameObject.SetActive(false);
//                     item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.GetChild(0).gameObject.SetActive(false);
//                     item_2.gameObject.transform.GetChild(0).gameObject.SetActive(true);


//                     item_2.gameObject.GetComponent<PairLine>().GetObject2().gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_2.gameObject.GetComponent<PairLine>().GetObject1().gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_2.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine);

//                 }

//                 if (item_2.itemSide == item_1.itemSide && item_2.itemId == item_1.itemId)
//                 {
//                     item_1.gameObject.transform.GetChild(0).gameObject.SetActive(false);

//                     item_1.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);


//                     item_1 = null;
//                     item_2 = null;
//                 }
//                 else if (item_1.itemSide == item_2.itemSide)
//                 {
//                     item_1.gameObject.transform.GetChild(0).gameObject.SetActive(false);
//                     item_2.gameObject.transform.GetChild(0).gameObject.SetActive(false);

//                     item_1.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_2.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);


//                     Debug.Log("Chon cung ben");
//                     item_1 = null;
//                     //item_1 = item_2;
//                     item_2 = null;
//                 }
//                 else if (item_1.itemSide != item_2.itemSide)
//                 {
//                     item_2.gameObject.transform.GetChild(0).gameObject.SetActive(true);
//                     item_2.gameObject.transform.DOScale(origin_scale * scale_item_rate, scale_item_duration).SetEase(Ease.InOutSine);

//                     valid_turn++;
//                     LineRenderer new_line = Instantiate(line);
//                     CreateLine(new_line);

//                     new_line.gameObject.SetActive(true);
//                     new_line.SetPosition(0, linePoint_1);
//                     new_line.SetPosition(1, linePoint_2);

//                     item_1.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);
//                     item_2.gameObject.transform.DOScale(origin_scale, scale_item_duration).SetEase(Ease.InOutSine);

//                     if (item_1.itemId == item_2.itemId)
//                     {
//                         score++;
//                         item_1.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, true);
//                         item_2.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, true);
//                         item_2.gameObject.GetComponent<PairLine>().PrintPairLine();
//                         line_dic.Add(new_line, true);
//                     }
//                     else
//                     {
//                         line_dic.Add(new_line, false);
//                         item_1.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, false);
//                         item_2.gameObject.GetComponent<PairLine>().SetPairLine(item_1.gameObject, item_2.gameObject, new_line, false);
//                         item_2.gameObject.GetComponent<PairLine>().PrintPairLine();
//                     }

//                     //Debug.Log(valid_turn + "_luot hop le --- " + score + " diem");
//                     item_1 = null;
//                     item_2 = null;
//                 }
//             }
//         }

//         if (score == total && valid_turn == total)
//         {
//             Debug.Log("Ban da thang");
//             is_finish = true;
//             foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
//             {
//                 ChangeLine(item.Key, item.Value);
//             }
//             StartCoroutine(DelayTime());

//             panel_end_game.gameObject.SetActive(true);
//             panel_menu.gameObject.SetActive(false);
//             if (cur_level < 10)
//             {
//                 //res_text.text = "WIN";
//                 retry_btn.gameObject.SetActive(false);
//                 next_level_btn.gameObject.SetActive(true);
//                 home_btn.gameObject.SetActive(true);
//                 SoundManager.Instance.PlaySFX("Win");
//                 SoundManager.Instance.music_source.Stop();
//                 particle_system[0].Play();
//                 particle_system[1].Play();

//                 res_sprite[0].gameObject.SetActive(false);
//                 res_sprite[1].gameObject.SetActive(true);


//             }
//             else if (cur_level == 10)
//             {
//                 //res_text.text = "CONGRATULATION";
//                 retry_btn.gameObject.SetActive(false);
//                 next_level_btn.gameObject.SetActive(false);
//                 home_btn.gameObject.transform.position = new Vector3(0, home_btn.gameObject.transform.position.y, home_btn.gameObject.transform.position.z);
//                 home_btn.gameObject.SetActive(true);
//                 SoundManager.Instance.music_source.Stop();
//                 SoundManager.Instance.PlaySFX("Win");
//             }

//             if (cur_level == max_level)
//             {
//                 PlayerPrefs.SetInt("-Level-", max_level + 1);
//                 max_level++;
//                 //cur_level++;
//             }
//         }
//         else if (score < total && valid_turn == total)
//         {
//             Debug.Log("Thua roi");
//             is_finish = true;
//             foreach (KeyValuePair<LineRenderer, bool> item in line_dic)
//             {
//                 ChangeLine(item.Key, item.Value);
//             }
//             StartCoroutine(DelayTime());
//             panel_end_game.gameObject.SetActive(true);
//             panel_menu.gameObject.SetActive(false);
//             retry_btn.gameObject.SetActive(true);
//             next_level_btn.gameObject.SetActive(false);
//             home_btn.gameObject.SetActive(true);
//             //res_text.text = "LOSE";
//             SoundManager.Instance.PlaySFX("Lose");
//             SoundManager.Instance.music_source.Stop();

//             res_sprite[1].gameObject.SetActive(false);
//             res_sprite[0].gameObject.SetActive(true);           
//         }
//     }

//     private void CreateLine(LineRenderer line)
//     {
//         line.startColor = Color.black;
//         line.endColor = Color.black;
//         //line.SetWidth(0.05f, 0.05f);
//         //line.startColor = Color.green;
//         line.startWidth = 0.05f;
//         line.endWidth = 0.05f;
//     }
//     private void ChangeLine(LineRenderer line, bool res)
//     {
//         switch (res)
//         {
//             case true:
//                 line.startColor = Color.green;
//                 line.endColor = Color.green;
//                 break;
//             case false:
//                 line.startColor = Color.red;
//                 line.endColor = Color.red;
//                 break;
//             default:
//         }
//     }
// }


// [System.Serializable]
// public class ItemClick
// {
//     public int itemId;
//     public ItemSide itemSide;
//     public GameObject gameObject;
// }

// public enum ItemSide
// {
//     Left,
//     Right
// }


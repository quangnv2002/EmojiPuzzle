using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideObjectController : MonoBehaviour
{
    public static GuideObjectController Instance { get; set; }
    [SerializeField] GameObject guide_icon;
    [SerializeField] GameObject hand_icon;
    [SerializeField] GameObject _hand;
    [SerializeField] GameObject level_prefab;
    [Header("2 POINTS")]
    [SerializeField] Vector2 _point1;
    [SerializeField] Vector2 _point2;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level_prefab = FindObjectOfType<LevelController>().gameObject;
        //_point1 = new Vector2();
        //_point2 = new Vector2();
        gameObject.SetActive(false);
    }

    public void ActiveGuide(int _des)
    {

        gameObject.SetActive(true);
        hand_icon.gameObject.SetActive(false);
        _hand = Instantiate(hand_icon,gameObject.transform);
        _hand.gameObject.SetActive(true);

        _point1 = level_prefab.GetComponent<LevelController>().GetArrayLeft()[_des].gameObject.transform.position;
        _point2 = Array.Find(level_prefab.GetComponent<LevelController>().GetArrayRight(), element =>
        element.GetComponent<ObjectController>().getItemID() == level_prefab.GetComponent<LevelController>().GetArrayLeft()[_des].getItemID()).transform.position;

        SpriteRenderer guideSprite = guide_icon.gameObject.GetComponent<SpriteRenderer>();
        guideSprite.drawMode = SpriteDrawMode.Tiled;

        guide_icon.transform.position = level_prefab.GetComponent<LevelController>().GetArrayLeft()[_des].gameObject.transform.position;
        guide_icon.gameObject.transform.rotation = Quaternion.Euler(0, 0, MathController.CalAngleBetween2Point(_point1, _point2));

        Vector2 _vec_1_2 = MathController.CalVectorBetween2Point(_point1, _point2);
        guideSprite.size = new Vector2(MathController.CalVectorLength(_vec_1_2) * 4.8f / 1.78f, 0.5f);

        _hand.transform.position = _point1; //+ new Vector2(0.25f, 0.25f);

        _hand.transform.DOMove(_point2, 3f).SetLoops(-1);
    }   
    
    public void DeActiveGuide()
    {
        Destroy(_hand.gameObject);
        gameObject.SetActive(false);
        //gameObject.transform.position = Vector2.zero;

        _point1 = Vector2.zero;
        _point2 = Vector2.zero;
       
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class GuideObjectController : MonoBehaviour
{
    public static GuideObjectController Instance { get; set; }
    [SerializeField] GameObject hand_icon;
    [SerializeField] GameObject _hand;
    [SerializeField] GameObject level_prefab;
    [Header("2 POINTS")]
    [SerializeField] Vector2 _point1;
    [SerializeField] Vector2 _point2;

    [SerializeField] LineRenderer guide_line;
    [SerializeField] List<LineRenderer> list_guideLine;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        level_prefab = FindObjectOfType<LevelController>().gameObject;
        gameObject.SetActive(false);
    }

    public void ActiveGuide(int _des)
    {

        gameObject.SetActive(true);
        hand_icon.gameObject.SetActive(false);
        _hand = Instantiate(hand_icon, gameObject.transform);
        _hand.gameObject.SetActive(true);

        _point1 = level_prefab.GetComponent<LevelController>().GetArrayLeft()[_des].gameObject.transform.position;
        _point2 = Array.Find(level_prefab.GetComponent<LevelController>().GetArrayRight(), element =>
        element.GetComponent<ObjectController>().getItemID() == 
        level_prefab.GetComponent<LevelController>().GetArrayLeft()[_des].getItemID()).transform.position;

        _hand.transform.position = _point1; //+ new Vector2(0.25f, 0.25f);
        _hand.transform.DOMove(_point2, 3f).SetLoops(-1);

        LineHandle();
    }

    private void LineHandle()
    {
        LineRenderer new_line = Instantiate(guide_line);
        new_line.gameObject.SetActive(true);
        new_line.startColor = Color.white;
        new_line.endColor = Color.white;
        new_line.SetPosition(0, _point1);
        new_line.SetPosition(1, _point2);
        list_guideLine.Add(new_line);
    }

    public void DeActiveGuide()
    {
        if(list_guideLine.Any())    //NẾU DIC CÓ PHẦN TỬ
        {
            Destroy(_hand.gameObject);
            Destroy(list_guideLine[0].gameObject);
            list_guideLine.Clear();
            gameObject.SetActive(false);
            _point1 = Vector2.zero;
            _point2 = Vector2.zero;
        }
    }
}
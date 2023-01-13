using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPopupHandle : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] List<GameObject> list_popup;
    public static AllPopupHandle Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AllPopupHandle>();
        }
        if (canvas == null)
        {
            canvas = FindObjectOfType<GameCanvas>().gameObject;
        }
    }

    public void InitPopup(PopUpName _name)
    {
        GameObject _popup = list_popup.Find(_popup => _popup != null && _popup.GetComponent<PopupBase>().getPopUpName() == _name);
        if (_popup.gameObject)
        {
            GameObject obj = Instantiate(_popup.gameObject, canvas.transform);
        }
        Debug.Log("Init");

    }
    
    private void Start()
    {
        Debug.Log(canvas.gameObject.name);

    }
}

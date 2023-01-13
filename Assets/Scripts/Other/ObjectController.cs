// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;


// public class ObjectController : MonoBehaviour
// {
//     [SerializeField] ItemSide itemSide;
//     [SerializeField] int itemId;
//     private EventTrigger eventTrigger;

//     Vector3 origin_scale;
//     Vector3 scale_to;

//     private void Start()
//     {
//         origin_scale = transform.localScale;
//         scale_to = origin_scale * 2;
//         eventTrigger = GetComponent<EventTrigger>();
//         PointerDown();
//     }

//     private void PointerDown()
//     {
//         EventTrigger.Entry entry = new EventTrigger.Entry();
//         entry.eventID = EventTriggerType.PointerDown;
//         entry.callback.AddListener((eventData) =>
//         {
//             GameController.Instance.OnClickPoint(itemSide, itemId, GetItemID(), GetPos(), GetGameObject());
            
//         });
//         eventTrigger.triggers.Add(entry);
//     }

//     private Sprite GetSprite()
//     {
//         return GetComponent<SpriteRenderer>().sprite;
//     }
//     public void SetItemID(int item_id)
//     {
//         this.itemId = item_id;
//     }
//     public int GetItemID()
//     {
//         return itemId;
//     }
//     public Vector3 GetPos()
//     {
//         return gameObject.transform.position;
//     }

//     public GameObject GetGameObject()
//     {
//         return gameObject;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectController : MonoBehaviour
{
    [SerializeField] ItemSide itemSide;
    [SerializeField] int itemId;
    private EventTrigger eventTrigger;

    Vector3 origin_scale;
    Vector3 scale_to;

    public int getItemID()
    {
        return itemId;
    }    

    private void Start()
    {
        origin_scale = transform.localScale;
        scale_to = origin_scale * 2;
        eventTrigger = GetComponent<EventTrigger>();
        PointerDown();
    }

    private void PointerDown()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) =>
        {
            revoGameController.Instance.OnClickPoint(itemSide, itemId, GetItemID(), GetPos(), GetGameObject());
            
        });
        eventTrigger.triggers.Add(entry);
    }

    private Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }
    public void SetItemID(int item_id)
    {
        this.itemId = item_id;
    }
    public int GetItemID()
    {
        return itemId;
    }
    public Vector3 GetPos()
    {
        return gameObject.transform.position;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

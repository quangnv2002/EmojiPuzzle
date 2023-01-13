using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class SafeAreaFilter : MonoBehaviour
{
    private void Awake()
    {
        var rec_transform = GetComponent<RectTransform>();
        var safe_area = Screen.safeArea;
        var anchorMin = safe_area.position;
        var anchorMax = anchorMin + safe_area.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;

        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rec_transform.anchorMin = anchorMin;
        rec_transform.anchorMax = anchorMax;
    }
}

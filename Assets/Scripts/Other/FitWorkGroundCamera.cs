using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitWorkGroundCamera : MonoBehaviour
{
    private Camera main_camera;
    private float current_scale;

    private float ty_le_man_hinh_tham_chieu;
    private void Start()
    {
        ty_le_man_hinh_tham_chieu = 9 / 18f;
        Debug.Log(ty_le_man_hinh_tham_chieu);
        main_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.position = new Vector3(main_camera.transform.position.x, main_camera.transform.position.x, 0);

        Vector3 bottom_left = main_camera.ViewportToWorldPoint(Vector3.zero) * 100;
        Vector3 top_right = main_camera.ViewportToWorldPoint(new Vector3(main_camera.rect.width, main_camera.rect.height)) * 100;

        Vector3 screen_size = top_right - bottom_left;
        float screen_ratio = screen_size.x / screen_size.y; // ngang/doc

        current_scale = transform.localScale.x;
        transform.localScale = new Vector3(0.8f * screen_ratio, 0.8f * screen_ratio, 0.8f * screen_ratio);
        //screen_ratio / ty_le_man_hinh_tham_chieu
        Debug.Log("Ty le man hinh hien tai : " + 1 / screen_ratio + " --- prefab scale truoc / sau : " + current_scale + "/" + transform.localScale.x);
    }
}

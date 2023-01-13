using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] ObjectController[] array_left;
    [SerializeField] ObjectController[] array_right;



    public ObjectController[] GetArrayLeft()
    {
        return array_left;
    }

    public ObjectController[] GetArrayRight()
    {
        return array_right;
    }

}

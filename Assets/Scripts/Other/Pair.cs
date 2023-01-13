using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pair
{
    [SerializeField] Sprite pic1;
    [SerializeField] Sprite pic2;

    public Sprite GetPic1()
    {
        return this.pic1;
    }
    public Sprite GetPic2()
    {
        return this.pic2;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class ItemData : ScriptableObject
{
    [SerializeField] Pair[] pair_list;
    public Pair[] GetPairList()
    {
        return this.pair_list;
    }
}

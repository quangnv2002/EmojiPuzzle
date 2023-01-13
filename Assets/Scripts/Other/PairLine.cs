using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairLine : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private LineRenderer line;

    private bool scored;

    public void SetPairLine(GameObject obj1, GameObject obj2, LineRenderer line, bool scored)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
        this.line = line;
        this.scored = scored;
    }
    public LineRenderer GetPairLine_Line()
    {
        return this.line;
    }
    public void PrintPairLine()
    {
        Debug.Log(obj1.name + "---" + obj2.name + "---" + line.name);
    }
    public bool GetScored()
    {
        return scored;
    }

    public GameObject GetObject2()
    {
        return obj2;
    }
    public GameObject GetObject1()
    {
        return obj1;
    }

    public void SetObject1(GameObject _obj)
    {
        this.obj1 = _obj;
    }
    public void SetObject2(GameObject _obj)
    {
        this.obj2 = _obj;
    }
}

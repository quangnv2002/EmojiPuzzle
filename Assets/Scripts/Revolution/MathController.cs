using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathController
{
    public static float ChangeRadToDegree(float _rad)
    {
        return (_rad * 180 / Mathf.PI);
    }

    public static float CalVectorLength(Vector2 _vec)
    {
        return Mathf.Sqrt(_vec.x * _vec.x + _vec.y * _vec.y);
    }
    //public static float CalAngleBetween2Vectors(Vector2 _vec1, Vector2 _vec2)
    //{
    //    //float _res = (_vec1.x * _vec2.x + _vec1.y * _vec2.y) / (CalVectorLength(_vec1) * CalVectorLength(_vec2));
    //    float _res = Vector2.Dot(_vec1, _vec2) / (CalVectorLength(_vec1) * CalVectorLength(_vec2));
    //    return _res;
    //}

    public static Vector2 CalVectorBetween2Point(Vector2 _root, Vector2 _tail)
    {
        return new Vector2(_tail.x - _root.x, _tail.y - _root.y);
    }

    //public static float CalAngleBetween3Point(Vector2 _root, Vector2 _point1, Vector2 _point2)
    //{
    //    Vector2 _vec1 = CalVectorBetween2Point(_root, _point1);
    //    Vector2 _vec2 = CalVectorBetween2Point(_root, _point2);
    //    return ChangeRadToDegree(Mathf.Acos(CalAngleBetween2Vectors(_vec1, _vec2)));
    //}

    public static float CalAngleBetween2Point(Vector2 _root, Vector2 _point2)
    {
        Vector2 _point1 = _root + new Vector2(1, 0);
        //Debug.Log(_point1);
        Vector2 _vec1 = CalVectorBetween2Point(_root, _point1);
        Vector2 _vec2 = CalVectorBetween2Point(_root, _point2);

        float _res = Vector2.SignedAngle(_vec1, _vec2);
        return _res;
        //float _res = Mathf.Acos(_rad);

        //float _rad = CalAngleBetween2Vectors(_vec1, _vec2);
        //if (_rad <= 0)
        //{
        //    return _res;
        //}
        //else return _res;
    }
}

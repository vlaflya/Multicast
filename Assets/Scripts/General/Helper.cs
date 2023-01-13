using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static Vector3 GetRandomPositionInCircle(Vector3 center, float radius, float maxDistance)
    {
        float angle = Mathf.Deg2Rad * Random.Range(0, 360);
        float distance = Random.Range(0, maxDistance) + radius;
        Vector3 pos = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        return pos;
    }
}

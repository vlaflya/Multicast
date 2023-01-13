using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    public float speed;
    public float radius;
    public float dps;
    public Action<Vector3> onPositionUpdated;
    public Action<float> onSpeedUpdated;
    public Action<float> onDpsUpdated;
    public Action<float> onRadiusUpdated;
    
    public void SetCallbacks(Action<Vector3> onPositionUpdated)
    {
        this.onPositionUpdated = onPositionUpdated;
    }
}

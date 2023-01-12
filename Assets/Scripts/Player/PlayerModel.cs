using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerModel : MonoBehaviour
{
    public float speed;
    public Action<Vector3> onPositionUpdated;
    public Action<float> onSpeedUpdated{get; private set;}
    public Action<float> onDpsUpdated{get; private set;}
    public Action<float> onRadiusUpdated{get; private set;}
    public void SetCallbacks(Action<Vector3> onPositionUpdated){
        this.onPositionUpdated = onPositionUpdated;
    }
}

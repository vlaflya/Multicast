using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsController : MonoBehaviour
{
    [SerializeField] private ParticleSystem circlePartilceSystem;
    public void ChangeRingRadius(float radius){
        var shape = circlePartilceSystem.shape;
        shape.radius = radius;
    }
}

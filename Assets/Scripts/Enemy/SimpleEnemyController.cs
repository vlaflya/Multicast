using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour, IHealthController
{
    public void Kill(){
        Debug.Log("Kill enemy");
        Destroy(gameObject);
    }
}

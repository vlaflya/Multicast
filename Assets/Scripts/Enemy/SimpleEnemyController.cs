using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour, IHealthController
{
    [SerializeField] private GameObject deathParticles;

    public void Kill()
    {
        ScoreController.score++;
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int score;
    private void Start() {
        score = 0;
    }
    private void OnDestroy() {
        score = 0;
    }
}

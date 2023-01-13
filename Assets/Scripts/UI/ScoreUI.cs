using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private int prevScore = 0;
    void Start()
    {
        var scoreSctream = Observable.EveryUpdate()
        .Where(_ => prevScore != ScoreController.score)
        .Subscribe(_ => {
            prevScore = ScoreController.score;
            text.text = "Enemies killed " + prevScore;
        });

    }
}

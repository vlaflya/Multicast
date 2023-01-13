using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text dpsText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text radiusText;
    private float prevDps;
    private float prevSpeed;
    private float prevRadius;

    private IDisposable findPlayerStream;
    void Start()
    {
        findPlayerStream = Observable.EveryUpdate()
        .Where(_ => GameObject.FindObjectOfType<PlayerModel>())
        .Subscribe(_ =>
        {
            SubscribeToPlayerModelStreams();
        });
    }

    private void SubscribeToPlayerModelStreams()
    {
        findPlayerStream.Dispose();
        var playerModel = GameObject.FindObjectOfType<PlayerModel>();
        SubscribeToDpsStream(playerModel);
        SubscribeToRadiusStream(playerModel);
        SubscribeToSpeedStream(playerModel);
    }

    private void SubscribeToDpsStream(PlayerModel playerModel)
    {
        prevDps = playerModel.dps;
        dpsText.text = "DPS " + prevDps;
        var dpsStream = Observable.EveryUpdate()
        .Where(_ => prevDps != playerModel.dps)
        .Subscribe(_ =>
        {
            prevDps = playerModel.dps;
            dpsText.text = "DPS " + prevDps;
        });
    }

    private void SubscribeToSpeedStream(PlayerModel playerModel)
    {
        prevSpeed = playerModel.speed;
        speedText.text = "Speed " + prevSpeed;
        var speedStream = Observable.EveryUpdate()
        .Where(_ => prevSpeed != playerModel.speed)
        .Subscribe(_ =>
        {
            prevSpeed = playerModel.speed;
            speedText.text = "Speed " + prevSpeed;
        });
    }

    private void SubscribeToRadiusStream(PlayerModel playerModel)
    {
        prevRadius = playerModel.radius;
        radiusText.text = "Radius " + prevRadius;
        var radiusStream = Observable.EveryUpdate()
        .Where(_ => prevRadius != playerModel.radius)
        .Subscribe(_ =>
        {
            prevRadius = playerModel.radius;
            radiusText.text = "Radius " + prevRadius;
        });
    }
}

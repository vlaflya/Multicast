using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;
using System;

public class PlayerCameraFollower : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private IDisposable findPlayerStream;
    void Start()
    {
        findPlayerStream = Observable.EveryUpdate()
        .Where(_ => GameObject.FindObjectOfType<PlayerModel>())
        .Subscribe(_ =>
        {
            AssignCameraToPlayer();
        });
    }

    public void AssignCameraToPlayer(){
        findPlayerStream.Dispose();
        Transform playerTransform = GameObject.FindObjectOfType<PlayerModel>().transform;
        cinemachineVirtualCamera.Follow = playerTransform;
        // cinemachineVirtualCamera.LookAt = playerTransform;
    }
}

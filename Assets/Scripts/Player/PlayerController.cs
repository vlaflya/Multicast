using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerMovementController movementController;
    private void Start()
    {
        inputController.Configure(onDirectionInput);
        movementController.Configure();
    }
    private void onDirectionInput(Vector3 direction){
        movementController.Move(direction, model.speed);
        model.onPositionUpdated?.Invoke(transform.position);
    }
}

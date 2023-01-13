using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerInputController inputController;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerVisualsController visualsController;

    public void OnStart()
    {
        inputController.Configure(onDirectionInput);
        movementController.Configure();
        visualsController.ChangeRingRadius(model.radius);
    }

    private void onDirectionInput(Vector3 direction)
    {
        movementController.Move(direction, model.speed);
        model.onPositionUpdated?.Invoke(transform.position);
    }

    public void ChangeSpeed(float value)
    {
        model.speed += value;
    }

    public void ChangeRadius(float value)
    {
        model.radius += value;
        model.onRadiusUpdated?.Invoke(model.radius);
        visualsController.ChangeRingRadius(model.radius);
    }

    public void ChangeDPS(float value)
    {
        model.dps += value;
        model.onDpsUpdated?.Invoke(model.dps);
    }
}

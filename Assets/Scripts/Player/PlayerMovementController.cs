using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private Vector3 moveAmount = Vector3.zero;
    private CharacterController characterController;

    public void Configure()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction, float speed)
    {
        moveAmount = direction * speed;
        characterController.Move(moveAmount * Time.deltaTime);
    }
}

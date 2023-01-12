using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private CharacterController characterController;
    public void Configure() {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 direction, float speed){
        characterController.Move(direction * speed * Time.deltaTime);
    }
}

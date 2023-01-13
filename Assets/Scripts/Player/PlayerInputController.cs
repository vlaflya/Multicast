using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputController : MonoBehaviour
{
    private Action<Vector3> onDirectionInput;

    public void Configure(Action<Vector3> onDirectionInput)
    {
        this.onDirectionInput = onDirectionInput;
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            Vector3 input = Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right;
            onDirectionInput?.Invoke(input.normalized);
        }
    }
}

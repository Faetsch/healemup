using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Vector2 movementInput;
    private Vector3 movement;
    private CharacterController characterController;

    public void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movement = new Vector3(movementInput.x, 0f, movementInput.y);
    }


    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        characterController.Move(movement * Time.deltaTime * speed);
    }
}

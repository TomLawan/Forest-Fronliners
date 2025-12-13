using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _playerSpeed = 5f;

    [SerializeField]
    private float _rotationSpeed = 10f;

    [SerializeField]
    private Camera _followcamera; 
    private void Start()

    {
        _controller = GetComponent<CharacterController>();

    }
    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalnput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, _followcamera.transform.eulerAngles.y, 0 ) * new Vector3 (horizontalnput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        if(movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime ); 
        }

        _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);
    }

} 



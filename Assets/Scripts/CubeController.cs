using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class CubeController : MonoBehaviour {
    private Vector2 _iMovement;
    private float _moveSpeed = 10f;
    
    private void OnMove(InputValue value) {
        Debug.Log($"Moving: {value.Get<Vector2>()}");
        
        _iMovement = value.Get<Vector2>();
    }
    
    private void OnJump() {
        Debug.Log("Moving up");
        transform.Translate(transform.up);
    }
    
    private void OnPunch() {
        Debug.Log("Moving up");
        transform.Translate(transform.up);
    }
    
    private void OnBlock() {
        Debug.Log("Moving up");
        transform.Translate(transform.up);
    }
    
    private void OnKick() {
        Debug.Log("Moving down");
        transform.Translate(-transform.up);
    }

    private void Update() {
        Move();
    }

    private void Move() {
        var movement = _moveSpeed * Time.deltaTime * _iMovement;
        transform.Translate(movement);
    }
}

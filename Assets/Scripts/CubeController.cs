using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class CubeController : MonoBehaviour {
    private Vector2 i_movement;
    private float moveSpeed;
    
    private void OnMoveStick(InputValue value) {
        Debug.Log($"Moving: {value.Get<Vector2>()}");
        
        i_movement = value.Get<Vector2>();
    }
    
    private void OnMoveUp() {
        Debug.Log("Moving up");
        transform.Translate(transform.up);
    }
    
    private void OnMoveDown() {
        Debug.Log("Moving down");
        transform.Translate(-transform.up);
    }

    void Update() {
        Move();
    }

    private void Move() {
        Vector3 movement = new Vector3(i_movement.x, 0, i_movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _mouseAimOffset;
    [SerializeField] private float _screenTiltOffset;

    public float movementSpeed;
    
    public void MoveCharacter(Vector2 direction)
    {
        if(direction != Vector2.zero) {
            Vector3 moveForceVector = new Vector3(direction.x , 0, direction.y) * movementSpeed;
            //_rigidBody.AddForce(moveForceVector, ForceMode.VelocityChange);
            _rigidBody.velocity = moveForceVector;
        }

        if(direction == Vector2.zero) {
            //_rigidBody.AddForce(-_rigidBody.velocity, ForceMode.VelocityChange);
            _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
        }

        if(_rigidBody.velocity.magnitude > movementSpeed)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * movementSpeed;
        }
    }

    public void RotateCharacter(Quaternion direction, float smoothing) {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, smoothing * Time.fixedDeltaTime);
    }

    public void RotateCharacter(Vector3 lookPoint) {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint + (transform.right * _mouseAimOffset * -1) + (Vector3.forward * _screenTiltOffset * -1));
    }

    public void IncreaseMovementSpeedByPercentage(float value) {
        movementSpeed += movementSpeed * value / 100;
    }
}

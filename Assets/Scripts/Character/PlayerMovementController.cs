using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _mouseAimOffset;
    [SerializeField] private float _screenTiltOffset;

    public float movementSpeed;
    [SerializeField] private float _dampeningSpeed = 1;

    private Transform _mainCameraTransform;

    private Vector3 _smoothDampVelocity = Vector3.zero;

    private void Awake() {
        _mainCameraTransform = Camera.main.transform;
    }
    
    public void MoveCharacter(Vector2 direction)
    {
        if(direction != Vector2.zero) {
            Vector3 moveForceVector = new Vector3(direction.x , 0, direction.y) * movementSpeed;
            moveForceVector = Quaternion.Euler(0, _mainCameraTransform.rotation.eulerAngles.y, 0) * moveForceVector;
            //_rigidBody.AddForce(moveForceVector, ForceMode.VelocityChange);
            // _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, moveForceVector, ref _smoothDampVelocity, _dampeningSpeed * Time.fixedDeltaTime);
            _rigidBody.velocity = new Vector3(moveForceVector.x, _rigidBody.velocity.y, moveForceVector.z);
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
        Vector3 lookVector = heightCorrectedPoint + (_mainCameraTransform.right * _mouseAimOffset * -1) + (_mainCameraTransform.forward * _screenTiltOffset * -1);
        Vector3 heightCorrectedLookVector = new Vector3(lookVector.x, transform.position.y, lookVector.z);
        transform.LookAt(heightCorrectedLookVector);
    }

    public void IncreaseMovementSpeedByPercentage(float value) {
        movementSpeed += movementSpeed * value / 100;
    }
}

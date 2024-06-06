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

    #region Dash
    private bool _isDashed = false;
    private bool _currentlyDashing = false;
    [SerializeField] private float _dashDuration = 0.1f;
    private float _tempDashDuration;
    [SerializeField] private float _dashCooldown = 1.0f;
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private Vector2 _lastMovementDirection;
    #endregion

    private void Awake() {
        _mainCameraTransform = Camera.main.transform;
    }
    
    public void MoveCharacter(Vector2 direction)
    {
        if (_isDashed) {
            if (!_currentlyDashing) {
                _lastMovementDirection = direction;
                _tempDashDuration = _dashDuration;
                StartCoroutine(CurrentDashingCooldown());
            }
            if (_tempDashDuration >= 0) {
                Vector3 moveForceVector = new Vector3(_lastMovementDirection.x , 0, _lastMovementDirection.y) * _dashSpeed;
                moveForceVector = Quaternion.Euler(0, _mainCameraTransform.rotation.eulerAngles.y, 0) * moveForceVector;
                _rigidBody.velocity = new Vector3(moveForceVector.x, _rigidBody.velocity.y, moveForceVector.z);
                _tempDashDuration -= Time.fixedDeltaTime;
            }
        } 
        
        if (direction != Vector2.zero && !_currentlyDashing) {
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

    public void Dash() {
        StartCoroutine(DashCooldown());
    }

    public void IncreaseMovementSpeedByPercentage(float value) {
        movementSpeed += movementSpeed * value / 100;
    }

    IEnumerator DashCooldown() {
        _isDashed = true;
        yield return new WaitForSeconds(_dashCooldown);
        _isDashed = false;
    }

    IEnumerator CurrentDashingCooldown() {
        _currentlyDashing = true;
        yield return new WaitForSeconds(_dashDuration);
        _currentlyDashing = false;
    }
}

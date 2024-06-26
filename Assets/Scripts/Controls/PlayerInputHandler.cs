using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    [SerializeField]
    private PlayerMovementController _playerMovementController;

    [SerializeField]
    private PlayerWeaponController _playerWeaponController;

    [SerializeField]
    private float _controllerDeadzone = 0.1f;
    [SerializeField]
    private float _gamepadRotateSmoothing = 1000f;

    private ControlSchemeEnum _currentControlScheme;

    private Vector2 _movementVector;
    private Quaternion _aimRotation;
    private Vector3 _aimLookPoint;
    private float _shootValue;
    private float _dashValue;

    private Transform _mainCameraTransform;
    
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _mainCameraTransform = Camera.main.transform;
    }
    
    void OnEnable() {
        _playerInputActions.Enable();
        _playerInputActions.Controls.Movement.performed += OnMovementPerformed;
        _playerInputActions.Controls.Movement.canceled += OnMovementCancelled;

        _playerInputActions.Controls.Aim.performed += OnAimPerformed;
        _playerInputActions.Controls.Aim.canceled += OnAimCancelled;

        _playerInputActions.Controls.Shoot.performed += OnShootPerformed;
        _playerInputActions.Controls.Shoot.canceled += OnShootCancelled;

        _playerInputActions.Controls.Dash.performed += OnDashPerformed;
        _playerInputActions.Controls.Dash.canceled += OnDashCancelled;
    }

    void OnDisable() {
        _playerInputActions.Disable();
        _playerInputActions.Controls.Movement.performed -= OnMovementPerformed;
        _playerInputActions.Controls.Movement.canceled -= OnMovementCancelled;

        _playerInputActions.Controls.Aim.performed -= OnAimPerformed;
        _playerInputActions.Controls.Aim.canceled -= OnAimCancelled;

        _playerInputActions.Controls.Shoot.performed -= OnShootPerformed;
        _playerInputActions.Controls.Shoot.canceled -= OnShootCancelled;

        _playerInputActions.Controls.Dash.performed -= OnDashPerformed;
        _playerInputActions.Controls.Dash.canceled -= OnDashCancelled;
    }

    void FixedUpdate() {
        _playerMovementController.MoveCharacter(_movementVector);
        
        switch (_currentControlScheme) {
            case ControlSchemeEnum.KeyboardAndMouse:
                _playerMovementController.RotateCharacter(_aimLookPoint);
                break;
            case ControlSchemeEnum.Gamepad:
                _playerMovementController.RotateCharacter(_aimRotation, _gamepadRotateSmoothing);
                break;
        }
        
        if (_shootValue > 0) {
            _playerWeaponController.ShootWeapon();
        }

        if (_dashValue > 0) {
            _playerMovementController.Dash();
        }
    }

    void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Vector2 oldMovementVector = _movementVector;
        Vector2 newMovementVector = context.ReadValue<Vector2>();
        _movementVector = context.ReadValue<Vector2>();
    }

    void OnMovementCancelled(InputAction.CallbackContext context)
    {
        _movementVector = Vector2.zero;
    }

    void OnAimPerformed(InputAction.CallbackContext context) {
        Vector2 aimVec = context.ReadValue<Vector2>();
        switch (_currentControlScheme) {
            case ControlSchemeEnum.KeyboardAndMouse:
                Ray ray = Camera.main.ScreenPointToRay(aimVec);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;
                if (groundPlane.Raycast(ray, out rayDistance)) {
                    Vector3 point = ray.GetPoint(rayDistance);
                    _aimLookPoint = point;
                }
                break;
            case ControlSchemeEnum.Gamepad:
                if (Mathf.Abs(aimVec.x) > _controllerDeadzone || Mathf.Abs(aimVec.y) > _controllerDeadzone) {
                    Vector3 playerDirection = Vector3.right * aimVec.x + Vector3.forward * aimVec.y;
                    if(playerDirection.sqrMagnitude > 0.0f) {
                        // TODO: Fix with PlayerMovementController.cs line 23
                        playerDirection = Quaternion.Euler(0, _mainCameraTransform.rotation.eulerAngles.y, 0) * playerDirection;
                        Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                        _aimRotation = newRotation;
                    }
                }
                break;
        }
    }

    void OnAimCancelled(InputAction.CallbackContext context) {
        
    }

    void OnShootPerformed(InputAction.CallbackContext context) {
        _shootValue = context.ReadValue<float>();
    }

    void OnShootCancelled(InputAction.CallbackContext context) {
        _shootValue = 0f;
    }

    void OnDashPerformed(InputAction.CallbackContext context) {
        _dashValue = context.ReadValue<float>();
    }

    void OnDashCancelled(InputAction.CallbackContext context) {
        _dashValue = 0f;
    }

    public void OnDeviceChange(PlayerInput pi) {
        switch (pi.currentControlScheme)
        {
            case "Gamepad":
                _currentControlScheme = ControlSchemeEnum.Gamepad;
                break;
            case "Keyboard and Mouse":
                _currentControlScheme = ControlSchemeEnum.KeyboardAndMouse;
                break;
        }
    }
}

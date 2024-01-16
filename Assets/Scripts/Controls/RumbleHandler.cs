using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleHandler : MonoBehaviour
{
    private Gamepad _gamepad;

    public void RumblePulse(float lowFrequency, float highFrequency) {
        _gamepad = Gamepad.current;

        if (_gamepad != null) {
            _gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
        }
    }
}

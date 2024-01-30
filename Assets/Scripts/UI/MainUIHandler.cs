using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIHandler : MonoBehaviour
{
    public static MainUIHandler instance;

    public TextMeshProUGUI fpsCounterText;
    private float _frameRefreshFrequency = 0.5f;
    private float _tempFrameRefreshFrequency;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        _tempFrameRefreshFrequency = _frameRefreshFrequency;
    }

    private void Update() {
        _tempFrameRefreshFrequency -= Time.deltaTime;
        if(_tempFrameRefreshFrequency < 0){
            fpsCounterText.text = Mathf.Round(1 / Time.smoothDeltaTime).ToString();
            _tempFrameRefreshFrequency = _frameRefreshFrequency;
        }
    }
}

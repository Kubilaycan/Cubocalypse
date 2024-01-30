using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    public static PlayerUIHandler instance;

    public Image healthBar;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
    }

    public void SetHealthBarPercentage(float percentage) {
        healthBar.fillAmount = percentage;
    }
}

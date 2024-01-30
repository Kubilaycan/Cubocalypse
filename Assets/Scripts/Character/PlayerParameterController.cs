using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterController : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _health;
    private void Awake() {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage) {
        if(_health > 0) {
            if(_health - damage > 0) {
                _health -= damage;
                PlayerUIHandler.instance.SetHealthBarPercentage(_health / _maxHealth);
            }
            else if(_health - damage <= 0) {
                _health = 0;
                PlayerUIHandler.instance.SetHealthBarPercentage(0);
            }
            
            if(_health == 0) {
                Die();
            }
        }
    }

    private void Die() {
        Debug.Log("Player died");
    }
}

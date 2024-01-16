using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterController : MonoBehaviour
{
    [SerializeField]
    private float _health = 100.0f;

    public void TakeDamage(float damage) {
        if(_health > 0) {
            if(_health - damage > 0) {
                _health -= damage;
            }
            else if(_health - damage <= 0) {
                _health = 0;
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

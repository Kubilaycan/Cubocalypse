using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _attackCooldown = 1.0f;
    private bool _canAttack = true;
    private bool _playerInRange = false;

    private PlayerParameterController _ppc;

    private void FixedUpdate() {
        if(_playerInRange && _canAttack) {
            Attack(_ppc);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            _ppc = other.gameObject.GetComponent<PlayerParameterController>();
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            _playerInRange = false;
        }
    }

    private void Attack(PlayerParameterController playerParameterController) {
        playerParameterController.TakeDamage(_damage);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown() {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}

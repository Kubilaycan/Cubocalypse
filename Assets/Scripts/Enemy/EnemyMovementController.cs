using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public float movementSpeed;

    private bool _canKnockback = true;
    private float _knockbackCooldown = 0.5f;

    [SerializeField] private Rigidbody _rigidBody;
    private void FixedUpdate() {
        Vector3 playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 heightCorrectedPoint = new Vector3(playerLocation.x, transform.position.y, playerLocation.z);
        transform.LookAt(heightCorrectedPoint);

        Vector3 movementDirection = (playerLocation - transform.position).normalized * movementSpeed;
        _rigidBody.velocity = new Vector3(movementDirection.x, _rigidBody.velocity.y, movementDirection.z);
    }

    public void Knockback(Vector3 knockbackForce) {
        if (_canKnockback) {
            _rigidBody.AddForce(knockbackForce, ForceMode.Impulse);
            StartCoroutine(KnockbackCoolDown());
        }
    }

    IEnumerator KnockbackCoolDown() {
        _canKnockback = false;
        yield return new WaitForSeconds(_knockbackCooldown);
        _canKnockback = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed = 1f;

    public float projectileSpeed { get {return _projectileSpeed;} }

    [SerializeField] private float _projectileDamage = 10f;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private int _penetrationCount = 1;

    [SerializeField] private float _knockbackForce = 1;

    [SerializeField] private AudioSource _impactSource;

    public void ApplyPowerUp(PowerUpType type, float value) {
        switch (type)
        {
            case PowerUpType.DoubleDamage:
                _projectileDamage *= 2;
                break;
            case PowerUpType.RapidFire:

            default:
                return;
        }
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy") {
            if(_penetrationCount > 0){
                collider.gameObject.GetComponent<EnemyParameterController>().TakeDamage(_projectileDamage);
                collider.gameObject.GetComponent<EnemyMovementController>().Knockback(_rigidbody.velocity.normalized * _knockbackForce);
                _impactSource.PlayOneShot(_impactSource.clip);
                _penetrationCount -= 1;
                    if(_penetrationCount == 0) {
                        Destroy(gameObject, _impactSource.clip.length);
                    }
            }
        }
    }
}

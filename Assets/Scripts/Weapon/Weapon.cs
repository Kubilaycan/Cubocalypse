using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ProjectileParent _projectile;
    [SerializeField] private float _projectileDelay;
    [SerializeField] [Range(0, 100)] private float _accuracy;
    [SerializeField] private Transform _projectileInstantiationLocation;
    [SerializeField] private AudioSource _firingAudioSource;

    public Transform rightHandIKTarget;
    public Transform leftHandIKTarget;

    private bool _canShoot = true;

    public void Shoot() {
        if(_canShoot) {
            Instantiate(_projectile, _projectileInstantiationLocation.position, _projectileInstantiationLocation.rotation *  Quaternion.Euler(0, Random.Range(-(100 - _accuracy), (100 -_accuracy)), 0));
            _firingAudioSource.PlayOneShot(_firingAudioSource.clip);
            _canShoot = false;
            StartCoroutine(ProjectileDelay());
        }
    }

    public void ApplyPowerUp(PowerUpType type, float value) {
        switch (type)
        {
            case PowerUpType.DoubleDamage:
                _projectile.ApplyPowerUp(type, value);
                break;
            case PowerUpType.RapidFire:
                _projectileDelay -= (_projectileDelay * value / 100);
                break;
            default:
                return;
        }
    }

    IEnumerator ProjectileDelay() {
        yield return new WaitForSeconds(_projectileDelay);
        _canShoot = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParent : MonoBehaviour
{
    [SerializeField]
    private float _projectileDestroyTime = 1f;

    [SerializeField]
    private Projectile[] _projectiles;

    public void ApplyPowerUp(PowerUpType type, float value) {
        switch (type)
        {
            case PowerUpType.DoubleDamage:
                for (int i = 0; i < _projectiles.Length; i++) {
                    _projectiles[i].ApplyPowerUp(type, value);
                }
                break;
            case PowerUpType.RapidFire:

            default:
                return;
        }
    }

    private void Awake() {
        foreach (var projectile in _projectiles)
        {
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectile.projectileSpeed;
        }
        Destroy(gameObject, _projectileDestroyTime);
    }
}

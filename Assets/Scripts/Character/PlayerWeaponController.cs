using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Weapon weapon;

    private void Awake() {
        if (!weapon) {
            weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        }
    }

    public void ShootWeapon() {
        weapon.Shoot();
    }
}

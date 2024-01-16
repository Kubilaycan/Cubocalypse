using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Weapon _weapon;

    public bool ikActive = false;

    private void Awake() {
        _weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    private void OnAnimatorIK(int layerIndex) {
        if(_animator) {
            if(ikActive) {
                if(_weapon != null) {
                    //Right
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    _animator.SetIKPosition(AvatarIKGoal.RightHand, _weapon.rightHandIKTarget.position);
                    _animator.SetIKRotation(AvatarIKGoal.RightHand, _weapon.rightHandIKTarget.rotation);

                    //Left
                    _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

                    _animator.SetIKPosition(AvatarIKGoal.LeftHand, _weapon.leftHandIKTarget.position);
                    _animator.SetIKRotation(AvatarIKGoal.LeftHand, _weapon.leftHandIKTarget.rotation);
                } else {
                    FindWeapon();
                }
            } else {
                //Left
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                //Left
                _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
    }

    private void FindWeapon() {
        _weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }
}

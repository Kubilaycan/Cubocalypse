using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    private void FixedUpdate() {
        float forwardVelocity = Vector3.Dot(_rigidbody.velocity.normalized, _rigidbody.transform.forward);
        float rightVelocity = Vector3.Dot(_rigidbody.velocity.normalized, _rigidbody.transform.right);

        _animator.SetFloat("Forward Velocity", forwardVelocity);
        _animator.SetFloat("Right Velocity", rightVelocity);
    }
}

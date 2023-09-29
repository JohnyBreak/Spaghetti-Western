using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _ragdollRoot;
    [SerializeField] private bool _startRagdoll = false;

    private Rigidbody[] _rigidbodies;
    private CharacterJoint[] _joints;

    void Awake()
    {
        _rigidbodies = _ragdollRoot.GetComponentsInChildren<Rigidbody>();
        _joints = _ragdollRoot.GetComponentsInChildren<CharacterJoint>();

        if (_startRagdoll) EnableRagdoll();
        else EnableAnimator();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) EnableAnimator();
        if (Input.GetKeyDown(KeyCode.K)) EnableRagdoll();
    }

    public void EnableRagdoll() 
    {
        _anim.enabled = false;
        foreach (var item in _joints)
        {
            item.enableCollision = true;
        }

        foreach (var item in _rigidbodies)
        {
            item.velocity = Vector3.zero;
            item.useGravity = true;
        }
    }

    public void EnableAnimator()
    {
        _anim.enabled = true;
        foreach (var item in _joints)
        {
            item.enableCollision = false;
        }

        foreach (var item in _rigidbodies)
        {
            item.useGravity = false;
        }
    }
}

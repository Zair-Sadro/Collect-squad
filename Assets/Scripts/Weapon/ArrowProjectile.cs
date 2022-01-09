using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : ASpawnedObject
{

    [SerializeField] private Rigidbody body;
    [SerializeField] private float gravity = -18;
    [SerializeField,Min(1)] private float heightOffset;

    private float _height;
    private Transform _target;

    public void Init( Transform target)
    {
        _target = target;
    }


    private void FixedUpdate()
    {
        GoToTarget();
    }

    private void GoToTarget()
    {
        Physics.gravity = Vector3.up * gravity;
        body.velocity = CallculateVelocity();
        transform.LookAt(CallculateVelocity());
    }

    private Vector3 CallculateVelocity()
    {
        float yDir = _target.position.y - transform.position.y;
        Vector3 xzDir = new Vector3(_target.position.x - transform.position.x, 0, _target.position.z - transform.position.z);
        _height = xzDir.magnitude / heightOffset;

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * _height);
        Vector3 velocityXZ = xzDir /(Mathf.Sqrt(-2 * _height / gravity) + Mathf.Sqrt(2 * (yDir - _height) / gravity));

        return velocityY + velocityXZ;
    }
}

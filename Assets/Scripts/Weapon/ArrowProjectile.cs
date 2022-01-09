using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : ASpawnedObject
{

    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed = 10;
    [SerializeField] private float timeToDisable = 2;
    [SerializeField] private DamageCollider damageColl;

    private float _height;
    private Transform _target;

    public void Init( Transform target, AWeapon bow, BattleUnit thisUnit)
    {
        _target = target;
        damageColl.Init(bow, thisUnit);
    }


    private void OnEnable()
    {
        GoToTarget();
    }


    private void OnDisable()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        body.velocity = Vector3.zero;
    }

    private void GoToTarget()
    {
        if (_target == null)
            return;

        StartCoroutine(Targeting(timeToDisable));
    }

    private IEnumerator Targeting(float time)
    {
        for (float i = 0; i < time; i+= Time.deltaTime)
        {
            if (_target == null)
                yield break;

            var dir = _target.position - transform.position;
            body.velocity = dir.normalized * speed;
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
    }

    
 
}

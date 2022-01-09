using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : ASpawner
{
    [SerializeField] private Transform firePoint;

    public override void StartSpawn()
    {
        StartCoroutine(SpawnRoutine(spawnerParams.SpawnTime));
    }

    protected override IEnumerator SpawnRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnArrow();
    }

    private void SpawnArrow()
    {
        var arrow = objectPooler.GetFreeObject();
        arrow.transform.position = firePoint.position;
    }
}

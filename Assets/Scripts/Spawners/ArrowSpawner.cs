using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : ASpawner
{
    public override void StartSpawn()
    {
        StartCoroutine(SpawnRoutine(spawnerParams.SpawnTime));
    }

    protected override IEnumerator SpawnRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        StartSpawn();
    }
}

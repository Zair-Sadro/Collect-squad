using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawner : ASpawner
{
    protected override void Awake()
    {
        base.Awake();
        StartSpawn();
    }

    public override void StartSpawn()
    {
            StartCoroutine(SpawnRoutine(spawnerParams.SpawnTime));
    }

    protected override IEnumerator SpawnRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnTile();
        StartSpawn();
    }

    private void SpawnTile()
    {
        var freeTile = objectPooler.GetFreeObject();
        freeTile.transform.localRotation = GetRandomRotation(0, 360);
        freeTile.transform.localPosition = GetRandomPosition(spawnerParams.MinX, spawnerParams.MaxX,
                                                             spawnerParams.MinZ, spawnerParams.MaxZ);
    }

    private Vector3 GetRandomPosition(float minX, float maxX, float minZ, float maxZ)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        var newPos = new Vector3(randomX, 0f, randomZ);
        newPos.y = 1;
        return newPos;
    }

    private Quaternion GetRandomRotation(float minYRot, float maxYRot)
    {
        float randomY = Random.Range(minYRot, maxYRot);

        var newRot = Quaternion.Euler(new Vector3(0, randomY, 0));
        return newRot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : ASpawnedObject
{
    [SerializeField] private Collider coll;
    [SerializeField] private Rigidbody body;

    public void OnBack()
    {
        coll.enabled = false;
        body.isKinematic = true;
    }

    public void OnGround()
    {
        transform.localScale = new Vector3(100, 100, 100);
        coll.enabled = true;
        body.isKinematic = false;
    }

}

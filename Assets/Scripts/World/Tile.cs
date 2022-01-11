using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : ASpawnedObject
{
    [SerializeField] private Collider coll;
    [SerializeField] private Rigidbody body;

    private bool _isTaken = false;

    public bool IsTaken => _isTaken;

    public void OnBack()
    {
        _isTaken = true;
        coll.enabled = false;
        body.isKinematic = true;
    }

    public void OnGround()
    {
        _isTaken = false;
        transform.localScale = new Vector3(100, 100, 100);
        coll.enabled = true;
        body.isKinematic = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBlocker"))
            this.gameObject.SetActive(false);
    }

}

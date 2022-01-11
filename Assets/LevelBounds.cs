using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tile t))
            t.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBot : MonoBehaviour
{
    public int radius;
    float timer;
    public float cooldown = 3;
    [SerializeField] GameObject plateEnemy;
    GameObject buf;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            float xPos = Random.Range(-radius, radius + 1);
            float zPos = Random.Range(-radius, radius + 1);
            float yRot = Random.Range(-180, 181);
            buf = Instantiate(plateEnemy, transform);
            buf.transform.position = new Vector3(xPos, transform.position.y + 0.2f, zPos);
            buf.transform.rotation = Quaternion.Euler(90, yRot, 0);
        }
    }
}

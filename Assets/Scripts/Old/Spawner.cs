using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int radius;
    float timer;
    public float cooldown = 3;
    [SerializeField] GameObject platePlayer;
    

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            float xPos = Random.Range(-radius, radius + 1);
            float zPos = Random.Range(-radius, radius + 1);
            float yRot = Random.Range(-180, 181);
            GameObject buf = Instantiate(platePlayer, transform);
            buf.transform.position = new Vector3(xPos, transform.position.y, zPos + 50);
            buf.transform.rotation = Quaternion.Euler(90, yRot, 0);
        }
    }
}

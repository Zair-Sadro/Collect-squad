using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrowPlayer : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject startPlace;
    PlayerSoldMovement movement;
    public float speed = 0.01f;
    float coolDown = 1;
    float timer = 0;

    private void Start()
    {
        movement = GetComponent<PlayerSoldMovement>();
    }

    private void Update()
    {
        if (movement.attack)
        {
            timer += Time.deltaTime;
            if (timer >= coolDown)
            {
                timer = 0;
                GameObject buffer = Instantiate(arrow, transform);
                buffer.transform.position = startPlace.transform.position;
                buffer.transform.rotation = Quaternion.identity;
                Destroy(buffer, 0.5f);
            }

        }


    }
}

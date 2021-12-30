using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    [SerializeField] Animator anim;
    public float hp = 1;
    public bool failed = false;
    [SerializeField] GameObject dust;


    [SerializeField] Rigidbody rb;
    [SerializeField] FloatingJoystick joystick;


    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * -speed, rb.velocity.y, joystick.Vertical * -speed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(-rb.velocity);
            anim.SetBool("Run", true);
            dust.SetActive(true);
        }

        else
        {
            anim.SetBool("Run", false);
            dust.SetActive(false);
        }
    }

    private void Update()
    {
        if (hp <= 0)
        {
            anim.SetBool("Die", true);
            speed = 0;
            failed = true;
            dust.SetActive(false);
        }

        if (TowerGeneration.FindObjectOfType<TowerGeneration>().broken == true)
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerTower"))
        {
            rb.velocity = Vector3.zero;
            joystick.enabled = true;
        }
    }
}

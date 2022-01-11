using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private UnitTeam team;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private TileSetter tileSetter;


    #region Properties

    public Vector3 PlayerDirection { get; set; }

    public TileSetter TileSetter => tileSetter;

    public Transform Transform => this.transform;

    #endregion


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        body.velocity = new Vector3(joystick.Horizontal * -speed, body.velocity.y, joystick.Vertical * -speed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            if(body.velocity != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(-body.velocity);

            playerAnimator.SetBool("Run", true);
            dustParticle.gameObject.SetActive(true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
            dustParticle.gameObject.SetActive(false);
        }
    }

   
}

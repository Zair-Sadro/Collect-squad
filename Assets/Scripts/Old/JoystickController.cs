using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public GameObject handler;
    public PlayerController playerContr;
    public GameObject stick;

    private Vector3 target;

    void Start()
    {
        handler.transform.position = stick.transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector3 touchPos = Input.GetTouch(0).position;
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                stick.transform.position = touchPos;
                stick.SetActive(true);
            }

            target = touchPos - stick.transform.position;

            if (target.magnitude < 50)
            {
                handler.transform.position = touchPos;
                playerContr.PlayerDirection = target;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                handler.transform.position = stick.transform.position;
                playerContr.PlayerDirection = Vector3.zero;
                stick.SetActive(false);
            }
        }
    }
}

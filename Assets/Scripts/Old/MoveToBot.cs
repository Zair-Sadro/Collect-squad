using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBot : MonoBehaviour
{
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject playerPanel;
    public float speed = 1;
    [SerializeField] Transform bot;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = true;
        joystick.SetActive(false);
        playerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bot.GetComponent<BotMovement>().hp <= 0)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, bot.transform.position + new Vector3(0, 15, 10), speed);
        }
    }
}

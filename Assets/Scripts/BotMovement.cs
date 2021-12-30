using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    public GameObject enemyPlate;
    Rigidbody rb;
    public int speed = 1;
    public int waitTime = 5;
    public bool waiting = false;
    [SerializeField] Animator anim;
    Vector3 startPosition;
    public float hp = 1;
    public bool win = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("Wait");
        startPosition = gameObject.transform.position;
    }


    void Update()
    {
        
        if ((gameObject.transform.position - startPosition) == Vector3.zero)
        {
            enemyPlate = GameObject.FindGameObjectWithTag("EnemyPlate");
            if (enemyPlate != null)
            {
                enemyPlate.GetComponent<Collider>().isTrigger = true;
            }
            GetComponent<BotBrickCounter>().taken = false;
            anim.SetBool("Run", false);
        }

        if (enemyPlate != null && GetComponent<BotBrickCounter>().taken == false && GetComponent<BotBrickCounter>().bricksCount != GetComponent<BotBrickCounter>().plates.Count && waiting == false && hp > 0)
        {
            anim.SetBool("Run", true);
            speed = 5;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, enemyPlate.transform.position, speed * Time.deltaTime);
            gameObject.transform.LookAt(enemyPlate.transform);
        }

        if (GetComponent<BotBrickCounter>().taken == true)
        {
            if (enemyPlate != null)
            {
                enemyPlate.tag = "Taken";
                
            }
            startPosition = gameObject.transform.position;
            enemyPlate = null;
            speed = 0;
            GetComponent<BotBrickCounter>().taken = false;
            return;
        }

        if (GetComponent<BotBrickCounter>().bricksCount == GetComponent<BotBrickCounter>().plates.Count && GetComponent<BotLosingBricks>().loseBricks != true && GetComponent<BotLosingBricks>().lostBricks == false && GameObject.FindGameObjectWithTag("BotTower") != null)
        {
            anim.SetBool("Run", true);
            speed = 5;
            gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("BotTower").transform);
            startPosition = gameObject.transform.position;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("BotTower").transform.position, speed * Time.deltaTime);
        }

        if (GetComponent<BotBrickCounter>().bricksCount == GetComponent<BotBrickCounter>().plates.Count && GetComponent<BotLosingBricks>().loseBricks != true && GetComponent<BotLosingBricks>().lostBricks == false && GameObject.FindGameObjectWithTag("BotTower") == null)
        {
            StartCoroutine("Wait");
            startPosition = gameObject.transform.position;
        }

        if (waiting == true)
        {
            StartCoroutine("Wait");
            startPosition = gameObject.transform.position;
        }

        if (hp <= 0)
        {
            anim.SetBool("Die", true);
            speed = 0;
            win = true;
        }
    }

    IEnumerator Wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }
}

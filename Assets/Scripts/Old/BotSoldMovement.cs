using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSoldMovement : MonoBehaviour
{
    //[SerializeField] float speed = 1;
    //public PlayerTowerGen generation;
    //public bool attack = false;
    //public bool attackTower = false;
    //public bool findPlayer = false;
    //public bool winner = false;
    //Animator anim;
    //float saveSpeed = 0;
    //public int hP = 1;
    //public int power = 1;
    //public GameObject player;
    //public PlayerController movement;
    //public GameObject enemyName;
    //public int vibrating = 0;
    //public ParticleSystem boom;

    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //    saveSpeed = speed;
    //    generation = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerTowerGen>();
    //    player = GameObject.Find("PlayerMain");
    //    movement = player.GetComponent<PlayerController>();
    //    if (PlayerPrefs.HasKey("Vibration"))
    //    {
    //        vibrating = PlayerPrefs.GetInt("Vibration");
    //    }
    //}


    //void Update()
    //{
    //    transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed);


    //    if (attack)
    //    {
    //        speed = 0;
    //        anim.SetTrigger("Attack");
    //        anim.SetBool("Run", false);
    //        if (enemyName == null)
    //        {
    //            attack = false;
    //        }
    //    }

    //    if (!attack)
    //    {
    //        anim.SetBool("Run", true);
    //        speed = saveSpeed;
    //    }

    //    if (hP <= 0)
    //    {
    //        anim.SetTrigger("Die");
    //        Destroy(gameObject, 3.5f);
    //        speed = 0;
    //    }

    //    if (generation.hp >= 0)
    //    {
    //        if (attackTower)
    //        {
    //            InvokeRepeating("fightTower", 0, 3);
    //        }
    //    }

    //    if (generation.hp <= 0)
    //    {
    //        attackTower = false;
    //        CancelInvoke();
    //    }

    //    if (findPlayer)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime * 5);
    //    }

    //    if (player.GetComponent<PlayerController>().hp <= 0)
    //    {
    //        speed = 0;
    //    }

    //    if (winner)
    //    {
    //        anim.SetBool("Run", false);
    //        anim.SetTrigger("Win");
    //        speed = 0;
    //        generation.enabled = false;
    //    }

    //}

    //private void fightTower()
    //{
    //    speed = 0;
    //    anim.SetTrigger("Attack");
    //    anim.SetBool("Run", false);
    //    generation.hp -= 0.01f * power;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.CompareTag("PlayerSwordLvl1") && gameObject.tag == "BotSwordLvl1") || (other.CompareTag("PlayerSwordLvl1") && gameObject.tag == "BotBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<PlayerSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("PlayerBowLvl1") && gameObject.tag == "BotSwordLvl1") || (other.CompareTag("PlayerBowLvl1") && gameObject.tag == "BotBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<PlayerSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("PlayerSpearLvl1") && gameObject.tag == "BotSpearLvl1") || (other.CompareTag("PlayerSpearLvl1") && gameObject.tag == "BotBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<PlayerSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("PlayerSwordLvl1") && gameObject.tag == "BotBowLvl1") || (other.CompareTag("PlayerBowLvl1") && gameObject.tag == "BotSpearLvl1") || (other.CompareTag("PlayerSpearLvl1") && gameObject.tag == "BotSwordLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<PlayerSoldMovement>().power * 2;
    //        enemyName = other.gameObject;
    //        print(other.GetComponent<PlayerSoldMovement>().power * 2);
    //    }

    //    if (other.CompareTag("PlayerSold"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<PlayerSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if (other.CompareTag("PlayerTower"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Built"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Player"))
    //    {
    //        movement.hp--;
    //        winner = true;
    //        boom.Play();
    //    }
    //}



    //private void OnTriggerExit(Collider other)
    //{

    //    if (other.CompareTag("BoostUp") && generation.hp <= 0)
    //    {
    //        attackTower = false;
    //        findPlayer = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //    if (other.CompareTag("Built"))
    //    {
    //        attackTower = false;
    //        findPlayer = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //}
}

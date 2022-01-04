using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoldMovement : MonoBehaviour
{
    //[SerializeField] float speed = 1;
    //public TowerGeneration generation;
    //public bool attack = false;
    //public bool attackTower = false;
    //public bool findBot = false;
    //public bool winner = false;
    //Animator anim;
    //float saveSpeed = 0;
    //public int hP = 1;
    //public int power = 1;
    //public GameObject bot;
    //public BotMovement movement;
    //public GameObject enemyName;
    //public int vibrating = 0;
    //public ParticleSystem boom;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    anim = GetComponent<Animator>();
    //    saveSpeed = speed;
    //    generation = gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponentInChildren<TowerGeneration>();
    //    bot = GameObject.Find("BotMain");
    //    movement = bot.GetComponent<BotMovement>();
    //    if (PlayerPrefs.HasKey("Vibration"))
    //    {
    //        vibrating = PlayerPrefs.GetInt("Vibration");
    //    }
    //}

    //// Update is called once per frame
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

    //    if (findBot)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, bot.transform.position, speed * Time.deltaTime * 5);
    //    }

    //    if (bot.GetComponent<BotMovement>().hp <= 0)
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
    //    if ((other.CompareTag("BotSwordLvl1") && gameObject.tag == "PlayerSwordLvl1") || (other.CompareTag("BotSwordLvl1") && gameObject.tag == "PlayerBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<BotSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("BotBowLvl1") && gameObject.tag == "PlayerSwordLvl1") || (other.CompareTag("BotBowLvl1") && gameObject.tag == "PlayerBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<BotSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("BotSpearLvl1") && gameObject.tag == "PlayerSpearLvl1") || (other.CompareTag("BotSpearLvl1") && gameObject.tag == "PlayerBowLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<BotSoldMovement>().power;
    //        enemyName = other.gameObject;
    //    }

    //    if ((other.CompareTag("BotSpearLvl1") && gameObject.tag == "PlayerSwordLvl1") || (other.CompareTag("BotSwordLvl1") && gameObject.tag == "PlayerBowLvl1") || (other.CompareTag("BotBowLvl1") && gameObject.tag == "PlayerSpearLvl1"))
    //    {
    //        attack = true;
    //        hP -= other.GetComponent<BotSoldMovement>().power * 2;
    //        enemyName = other.gameObject;
    //        print(other.GetComponent<BotSoldMovement>().power * 2);
    //    }

    //    if (other.CompareTag("BotTower"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Untagged"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Built"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Broken"))
    //    {
    //        attackTower = true;
    //    }

    //    if (other.CompareTag("Bot"))
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
    //        findBot = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //    if (other.CompareTag("Built"))
    //    {
    //        attackTower = false;
    //        findBot = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //    if (other.CompareTag("Untagged"))
    //    {
    //        attackTower = false;
    //        findBot = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //    if (other.CompareTag("Broken"))
    //    {
    //        attackTower = false;
    //        findBot = true;
    //        if (vibrating == 1)
    //        {
    //            Vibration.Vibrate(100);
    //        }
    //    }

    //}
}

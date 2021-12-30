using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateSoldiers : MonoBehaviour
{
    [SerializeField] GameObject sold1;
    [SerializeField] float coolDown = 3;
    [SerializeField] Image clockBar;
    [SerializeField] PlayerTowerGen generation;
    float timer = 0;

    private void Start()
    {
        timer = coolDown;
        clockBar.enabled = true;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        clockBar.fillAmount = timer / coolDown;
        if (timer <= 0)
        {
            timer = coolDown;
            if (generation.countStatic >= 0 && generation.countStatic < generation.countBricks)
            {
                
                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.6f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            
            if (generation.countStatic >= generation.countBricks)
            {
                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.6f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }

            if (generation.countStatic >= generation.countBricks * 2)
            {
                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.6f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }
    }
}

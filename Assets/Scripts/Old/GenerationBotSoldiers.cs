using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationBotSoldiers : MonoBehaviour
{
    [SerializeField] GameObject sold1;
    [SerializeField] float coolDown = 3;
    [SerializeField] TowerGeneration generation;
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= coolDown)
        {
            timer = 0;
            if (generation.countStatic >= 0 && generation.countStatic < generation.countBricks)
            {

                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.2f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.rotation = new Quaternion(0, 180, 0, 0);
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
            
            if (generation.countStatic >= generation.countBricks)
            {
                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.2f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.rotation = new Quaternion(0, 180, 0, 0);
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }

            if (generation.countStatic >= generation.countBricks * 2)
            {
                GameObject sold = Instantiate(sold1, transform.parent.gameObject.transform.parent.gameObject.transform);
                sold.transform.position = gameObject.transform.parent.position;
                sold.transform.position = new Vector3(sold.transform.position.x, sold.transform.position.y + 0.2f, sold.transform.position.z);
                sold.transform.rotation = Quaternion.identity;
                sold.transform.rotation = new Quaternion(0, 180, 0, 0);
                sold.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }
        }
    }
}

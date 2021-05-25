using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wearon : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotDirection;
    public Transform center;
    public GameObject shot;
    public int bulletAmount = 5;
    private float timeShot;
    public float wait;
    public Text ammunationtxt;

    public int BulletAmount
    {
        set
        {
            if (value <= 0)
            {
                bulletAmount = 0;
                ammunationtxt.color = Color.red;
            }
            else
            {
                bulletAmount = value;
                ammunationtxt.color = Color.green;
            }
                ammunationtxt.text = $"Патроны: {bulletAmount}";
        }
        get
        {
            return bulletAmount;

        }



    }

    // Start is called before the first frame update
    void Start()
    {
        BulletAmount = bulletAmount;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
       // transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
       center.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset); ;
        if (timeShot <= 0)
        {
            if (Input.GetMouseButtonDown(0) && BulletAmount > 0)
            {
                Instantiate(shot, shotDirection.position, Quaternion.identity);
                Instantiate(bullet, shotDirection.position, transform.rotation);
                timeShot = wait;
                BulletAmount --;
               // ammunationtxt.text = $"Ammunation: {bulletAmount}";
               
            }


        }
        else
        {
            timeShot -= Time.deltaTime;
        }

    }
}

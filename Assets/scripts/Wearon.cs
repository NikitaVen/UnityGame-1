using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wearon : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotDirection;


    private float timeShot;
    public float wait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        if (timeShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, shotDirection.position, transform.rotation);
                timeShot = wait;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }

    }
}

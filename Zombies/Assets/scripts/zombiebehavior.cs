using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class zombiebehavior : MonoBehaviour
{
    public float health;
    private Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed;
    private Animator animator;
    private Material matBlink;
    private Material matDefault;
    private Vector3 move;
    bool coll = false;


    private float timeShot;
    public float wait = 1;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;
        timeShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {


        if (timeShot <= 0)
        {
            if (!coll)
            {
                if (dead)
                    Destroy(gameObject);
                float x = player.transform.position.x - rb.transform.position.x;
                float y = player.transform.position.y - rb.transform.position.y;
                if (!coll)
                {
                    float cos = x / (float)Math.Pow(x * x + y * y, 0.5);
                    float sin = y / (float)Math.Pow(x * x + y * y, 0.5);
                    animator.SetFloat("moveX", cos);
                    animator.SetFloat("moveY", sin);
                    animator.SetBool("moving", true);

                    move = new Vector3(speed * cos, speed * sin, 0);
                      transform.Translate(move);
                   // rb.MovePosition(move);

                }
                else
                    animator.SetBool("moving", false);
            }
            else
            {
                animator.SetBool("moving", false);
                //тут анмация атаки игрока может быть
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
            animator.SetBool("moving", false);
         
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        timeShot = wait;
        if (health <= 0)
        {
            animator.SetBool("is dead", true);
            dead = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.name.Contains("player"))
            coll = true;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("player"))  
            coll = true;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        coll = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class zombiebehavior : MonoBehaviour
{
    public float distance = 10;
    public float health = 100;
    public float force = 20;
    public GameObject effect;
    public playerController pl_contr;
    private float MaxHealth;
    private Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed;
    private Animator animator;

    public float damage_wait;
    private float damage_timer;

    public float damage_wait_wait;
    public float damage_timer_wait;

    public HealthBar bar;
    private float timeShot;
    public float wait = 1;
    bool atacking = false;

    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;
        timeShot = 0;
        damage_wait = 0;
        damage_timer = 0;
        GameObject pl = GameObject.Find("player");
        pl_contr = pl.GetComponent<playerController>();
        player = pl.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (timeShot <= 0)
        {
            if (dead)
                Destroy(gameObject);
            float x = player.position.x - rb.position.x;
            float y = player.position.y - rb.position.y;
            if (Math.Pow(x * x + y * y, 0.5) > 0.5 && Math.Pow(x * x + y * y, 0.5)< distance )
            {
                float cos = x / (float)Math.Pow(x * x + y * y, 0.5);
                float sin = y / (float)Math.Pow(x * x + y * y, 0.5);
                animator.SetFloat("moveX", cos);
                animator.SetFloat("moveY", sin);
                animator.SetBool("moving", true);
                transform.Translate(speed * cos, speed * sin, 0);
            }
            else
                animator.SetBool("moving", false);
        }
        else
        {
            timeShot -= Time.deltaTime;
            animator.SetBool("moving", false);
         
        }
        
            damage_wait -= Time.deltaTime;
        if (atacking)
            damage_timer -= Time.deltaTime;

    }

    public void Damage(float damage)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        bar.fill = (health - damage) / MaxHealth;
        health -= damage;
        timeShot = wait;
        if (health <= 0)
        {
            animator.SetBool("is dead", true);
            rb.mass = 99999;
            dead = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "player" || collision.collider.name == "gun" )
        {

            damage_timer = 0;

        }
    }




    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.name == "player" || collision.collider.name == "gun")
        {

            if (damage_wait <= 0)
            {
                damage_wait = damage_wait_wait;

                atacking = true;
                animator.SetBool("atack", true);
                if (damage_timer <= 0)
                {    
                    pl_contr.Damage(force);
                    atacking = false;
                    damage_timer = damage_timer_wait;
                }

            }
     
        }
    }


}

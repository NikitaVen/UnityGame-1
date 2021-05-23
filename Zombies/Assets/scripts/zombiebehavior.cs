using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class zombiebehavior : MonoBehaviour
{
    public float health;
    private float MaxHealth;
    private Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed;
    private Animator animator;
    private Material matBlink;
    private Material matDefault;
    public HealthBar bar;
    private float timeShot;
    public float wait = 1;
    bool dead;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
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
            if (dead)
                Destroy(gameObject);
            float x = player.position.x - rb.position.x;
            float y = player.position.y - rb.position.y;
            if (Math.Pow(x * x + y * y, 0.5) > 0.5)
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
    }

    public void Damage(float damage)
    {
        bar.fill = (health - damage) / MaxHealth;
        health -= damage;
        timeShot = wait;
        if (health <= 0)
        {
            animator.SetBool("is dead", true);
            dead = true;
        }

    }

}

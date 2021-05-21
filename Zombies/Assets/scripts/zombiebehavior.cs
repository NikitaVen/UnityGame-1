using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class zombiebehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
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
}

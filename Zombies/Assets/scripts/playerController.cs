using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject gun;
    public HealthBar healthbar;
    public float health;
    private float MaxHealth;
    public float speed;
    private Rigidbody2D rb;
    private float timer = 1;
    private bool isdead;

   

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isdead = false;
        MaxHealth = health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       // motion = speed * v;
    }
    private void FixedUpdate()
    {
        if (isdead)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            float X = 0;
            float Y = 0;
            if (Input.GetKey(KeyCode.A))
            {
                X = -speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                X = speed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                Y = speed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Y = -speed;
            }
            if (!(X == 0 && Y == 0))
            {
                animator.SetFloat("moveX", X);
                animator.SetFloat("moveY", Y);
                animator.SetBool("moving", true);
                //  if(!col)
                transform.Translate(X, Y, 0);

            }
            else
            {
                animator.SetBool("damaged", true);
                animator.SetBool("moving", false);
            }
        }
       
        // rb.MovePosition(rb.position + motion);
    }

    public void Damage(float damage)
    {
        animator.SetBool("damaged", true);
        healthbar.fill = (health - damage) / MaxHealth;
        health -= damage;
        if (health <= 0)
        {
            rb.mass = 99999;
            isdead = true;
            Destroy(gun);
            animator.SetBool("isDead", true);
        }
        //if (health <= 0)
        //{
        //    animator.SetBool("is dead", true);
        //    //dead = true;
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}

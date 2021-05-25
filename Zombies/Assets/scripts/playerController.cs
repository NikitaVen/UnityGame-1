using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{
    public GameObject effect;
    public GameObject Death_effect;
    public GameObject gun;
    public HealthBar healthbar;
    public float health;
    private float MaxHealth;
    public float speed;
    public float Health
    {
        set
        {
            if (value > MaxHealth)
            {
                health = MaxHealth;
            }
            else if (value < 0)
                health = 0;
            else
                health = value;
            healthbar.fill = health / MaxHealth;
        }
        get
        {
            return health;

        }



    }




    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        // isdead = false;
        MaxHealth = health;
        animator = GetComponent<Animator>();
        //  rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // motion = speed * v;
    }
    private void FixedUpdate()
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
            animator.SetBool("moving", false);
        }


        // rb.MovePosition(rb.position + motion);
    }

    public void Damage(float damage)
    {
        animator.SetBool("damaged", true);
        health -= damage;
        healthbar.fill = (health) / MaxHealth;

        if (health <= 0)
        {
            Instantiate(Death_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
            Instantiate(effect, transform.position, Quaternion.identity);
        //if (health <= 0)
        //{
        //    animator.SetBool("is dead", true);
        //    //dead = true;
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Heal"))
        {
            Health += collision.GetComponent<heal>().life_energy;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Bullet"))
        {
            gun.GetComponent<Wearon>().BulletAmount += collision.GetComponent<bulletsbonus>().amount;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Exit"))
        {

            Destroy(GameObject.Find("player"));

        }
    }

}

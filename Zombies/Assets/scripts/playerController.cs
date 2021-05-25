using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject effect;
    public GameObject Death_effect;
    public GameObject gun;
    public HealthBar healthbar;
    public Text txtHealth;
    public Text GameOvertxt;
    public float health;
    private float MaxHealth;
    public float speed;
    private bool isDead = false;
    private float wait = 0.5f;
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
            txtHealth.text = $"Çהמנמגüו: {health}/{MaxHealth}";

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
        Health = health;
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
        if (!isDead)
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
        }
        else
        {
            wait -= Time.deltaTime;
            animator.SetBool("isDead", true);
            if (wait < 0 && Input.anyKeyDown && !(Input.GetMouseButtonDown(0)
            || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

            }

        }


    }

    public void Damage(float damage)
    {
       // animator.SetBool("damaged", true);
        Health -= damage;
        healthbar.fill = (health) / MaxHealth;

        if (health <= 0)
        {
            isDead = true;
            this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true ;
            Instantiate(Death_effect, transform.position, Quaternion.identity);
            Destroy(gun);
            animator.SetBool("isDead", false);
            GameOvertxt.text = "GAME OVER";
            //Destroy(gameObject.GetComponent<gun>());
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            //Destroy(gameObject);
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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 motion;
    private bool col = false;

    public float health = 10f;


    //public LayerMask enemy;
    //public LayerMask wall;
    //public float collRadiusWall;
    //public float collRadiusEnemy;

    public Rigidbody2D enemy;

    private Animator animator;

    bool coll = false;
    // Start is called before the first frame update
    void Start()
    {
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

            if (!coll)
                 transform.Translate(X, Y, 0);
                //rb.MovePosition(transform.position + new Vector3(X, Y, 0));



        }
        else
            animator.SetBool("moving", false);
       
        // rb.MovePosition(rb.position + motion);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("zombie"))
            health -= 10;

       // coll = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       // coll = false;
    }
}

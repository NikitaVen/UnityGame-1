using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    public float life_energy;
   // public playerController pl_contr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.name == "player")
        //{
        //    pl_contr.Health += life_energy;
        //    Destroy(gameObject);
        //}
    }
}

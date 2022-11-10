using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float speed=100f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void mudarposica()
    {
        /*if (isfacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {*/
            print("entrou+");
            rb.velocity = -transform.right * speed;
        //}//
    }

    public void mudarpositionY()
    {
        rb.velocity = transform.up * speed;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            col.GetComponent<vida>().tomarDano(25);
        }
    }
}
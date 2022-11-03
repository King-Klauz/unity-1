using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void mudarposica(bool isfacingRight)
    {
        if (isfacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else
        {
            rb.velocity = -transform.right * speed;
        }
    }

    public void mudarpositionY()
    {
        rb.velocity = transform.up * speed;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("inimigo"))
        {
            Destroy(gameObject);
            col.GetComponent<inimigo>().TakeDamage();
        }
    }
}
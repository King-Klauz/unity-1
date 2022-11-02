using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    /*void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }*/

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

}

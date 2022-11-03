using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    //public Collider2D rage;
    private Transform playerPosition;
    private bool check;
    private float speed;
    private float life;


    // Start is called before the first frame update
    void Start()
    {
        life = 5;
        speed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x + 7, playerPosition.position.y), step);
            //transform.position += (playerPosition.position - transform.position).normalized * speed * Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        life--;
        //anim.SetTrigger("TakeHit");
        if (life <= 0)
        {
            //anim.SetTrigger("Death");
            Destroy(gameObject, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerPosition = col.transform;
            check = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            check = false;
        }
    }
}
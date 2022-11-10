using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class inimigo : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    //public Collider2D rage;
    private Transform playerPosition;
    private bool check;
    private float speed;
    private float life;
    private vida vidapersonagem;
    private float cooldown;
    public vida vidaInimigo;



    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        life = 100;
        speed = 5;
        vidapersonagem = GameObject.FindGameObjectWithTag("Player").GetComponent<vida>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vidapersonagem.valorVida <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        if (check)
        {
            anim.SetBool("rage", true);
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, playerPosition.position.y), step);

            if(transform.position.x<playerPosition.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Math.Abs(transform.position.x - playerPosition.position.x)<5f && cooldown<=0)
            {
                cooldown = 2f;
                vidapersonagem.tomarDano(10);
                print(cooldown);
            }
            cooldown -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("rage", false);
        }
    }

    public void TakeDamage()
    {
        life-= 25;
        vidaInimigo.tomarDano(25);
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
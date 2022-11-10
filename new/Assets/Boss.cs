using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    //public Collider2D rage;
    private Transform playerPosition;
    [SerializeField]
    Transform shootingPoint;
    private bool check;
    private float speed;
    private float life;
    private vida vidapersonagem;
    private float cooldown;
    public vida vidaInimigo;
    public GameObject bulletPrefab;
    private GameObject currentBullet;
    public float delay = 0;
    private bool isFacingRight = false;
    private bool isDead = false;
    




    // Start is called before the first frame update
    void Start()
    { 
        cooldown = 2f;
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

        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }

        if (transform.position.x < playerPosition.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            
        }

        if (transform.position.x < playerPosition.position.x)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }


        if (check)
        {
            anim.SetBool("rage", true);

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, playerPosition.position.y), step);


            if (Math.Abs(transform.position.x - playerPosition.position.x) < 5f && cooldown <= 0)
            {
                cooldown = 2f;
                vidapersonagem.tomarDano(10);
                print(cooldown);
            }
            cooldown -= Time.deltaTime;
            atirar(isFacingRight);
        }
        else
        {
            anim.SetBool("rage", false);
        }
    }

    public void TakeDamage()
    {
        life -= 25;
        vidaInimigo.tomarDano(25);
        //anim.SetTrigger("TakeHit");
        if (life <= 0 && !isDead)
        {
            print("entrou enemy");
            isDead = true;
            anim.SetBool("dead", true);
            SceneManager.LoadScene("Win");
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
        //anim.SetBool("rage", false);
    }

    private void atirar(bool isfacingRight)
    {
        
        if (delay <= 0)
        {       
            currentBullet = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            currentBullet.GetComponent<Enemy_bullet>().mudarposica(isfacingRight);
            
            delay = 2f;
        }
       
    }
}
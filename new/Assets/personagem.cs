using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class personagem : MonoBehaviour
{
    [SerializeField]
    private Animator animePernas, animeDorsal;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    Transform dorsal, pernas;
    public float jumpForce;
    public float speed;
    public int score;
    public Text points;

    // Start is called before the first frame update

    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetAxis("Horizontal") < 0)
        {
            animeDorsal.SetInteger("Speed", 1);
            animePernas.SetInteger("Speed", 1);
            dorsal.localScale = new Vector3(-1, 1, 1);
            pernas.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            animeDorsal.SetInteger("Speed", 1);
            animePernas.SetInteger("Speed", 1);
            dorsal.localScale = new Vector3(1, 1, 1);
            pernas.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            animeDorsal.SetInteger("Speed", 0);
            animePernas.SetInteger("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && animePernas.GetBool("Grounded"))
        {
            animeDorsal.SetBool("Jump", true);
            animePernas.SetBool("Jump", true);
            animeDorsal.SetBool("Grounded", false);
            animePernas.SetBool("Grounded", false);
            body.AddForce(new Vector2(body.velocity.x, jumpForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            animeDorsal.SetBool("Jump", false);
            animePernas.SetBool("Jump", false);
            animeDorsal.SetBool("Grounded", true);
            animePernas.SetBool("Grounded", true);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            score++;
            points.text = "pontos:" + score;
            print(score);
        }
    }
}
	
    /*void Update()
    {
        //Se estiver vivo
        if (anime.GetInteger("Life") > 0)
        {
            //Idle -> Walk -> Run
            
            //Jump
            if (Input.GetKeyDown("space") && anime.GetBool("Grounded"))
            {
                body.AddForce(new Vector2(0.0f, jumpForce));
                anime.SetBool("Grounded", false);
                anime.SetBool("Jump", true);
            }
            //Dead
            if (Input.GetKey("z"))
            {
                anime.SetInteger("Life", 0);
            }
        }
        //Se estiver morto
        else anime.SetInteger("Life", -1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anime.SetBool("Grounded", true);
            anime.SetBool("Jump", false);
        }
		*//*if(collision.gameObject.CompareTag("Coin")){
			Destroy(collision.gameObject);
			score++;
			points.text = "pontos:" + score;
			print (score);
		}*//*
	}*/


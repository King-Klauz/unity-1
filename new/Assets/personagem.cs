using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class personagem : MonoBehaviour
{
    [SerializeField]
    private Animator anime, animeDorsal;
    private Rigidbody2D body;
    public float jumpForce;
    public float speed;
	public int score;
	public Text points;

    // Start is called before the first frame update

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
		score = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetAxis("Horizontal") != 0.0f)
            {
                transform.localScale = new Vector3(Input.GetAxis("Horizontal")>0?1:-1, 1f, 1f);
                body.velocity = new Vector2 (Input.GetAxis("Horizontal")*speed*Time.fixedDeltaTime, 0);
                
                //transform.Translate(speed*Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            }
    }

    void Update()
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
		/*if(collision.gameObject.CompareTag("Coin")){
			Destroy(collision.gameObject);
			score++;
			points.text = "pontos:" + score;
			print (score);
		}*/
	}

}
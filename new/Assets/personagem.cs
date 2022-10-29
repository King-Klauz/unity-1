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
    private bool legFixedRight, legFixedLeft, firstFix;

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
            if(!legFixedLeft)
                FixLegPosition();
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            animeDorsal.SetInteger("Speed", 1);
            animePernas.SetInteger("Speed", 1);
            dorsal.localScale = new Vector3(1, 1, 1);
            pernas.localScale = new Vector3(1, 1, 1);
            if(!legFixedRight)
                FixLegPosition();
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

    private void FixLegPosition()
    {
        if (!legFixedRight && firstFix)
        {
            legFixedRight = true;
            legFixedLeft = false;
            pernas.position = new Vector3(pernas.position.x - 0.08f, pernas.position.y, pernas.position.z);
        }else if (!legFixedLeft || firstFix)
        {
            firstFix = true;
            legFixedLeft = true;
            legFixedRight = false;
            pernas.position = new Vector3(pernas.position.x + 0.08f, pernas.position.y, pernas.position.z);
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
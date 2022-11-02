using System;
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
    Transform dorsal, pernas, shootingPoint;
    public float jumpForce;
    public float speed;
    public int score;
    public Text points;
    private bool legFixedRight, legFixedLeft, firstFix, shootFixed, isFacingRight, isFacingUp;
    public float delay = 0;
    public GameObject bulletPrefab;
    private GameObject currentBullet;

    // Start is called before the first frame update

    void Start()
    {
        shootFixed = true;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }

        if (Input.GetAxis("Vertical") > 0 && delay < 0)
        {
            animeDorsal.SetInteger("Up", 1);
            isFacingUp = true;
        }
        else
        {
            isFacingUp = false;
            animeDorsal.SetInteger("Up", -1);
        }


        if (Input.GetAxis("Vertical") < 0 && delay < 0)
        {
            isFacingUp = false;
            animeDorsal.SetInteger("Down", 1);
        }
        else
        {
            animeDorsal.SetInteger("Down", -1);
        }

        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        if (Input.GetAxis("Horizontal") < 0)
        {
            shootingPoint.transform.Rotate(0, 180, 0);
            animeDorsal.SetInteger("Speed", 1);
            animePernas.SetInteger("Speed", 1);
            if (delay < 0)
                dorsal.localScale = new Vector3(-1, 1, 1);
            pernas.localScale = new Vector3(-1, 1, 1);
            shootingPoint.localScale = new Vector3(-1, 1, 1);

            if (!legFixedLeft)
            {
                if (animeDorsal.GetBool("Jump"))
                {
                    FixAirPosition();
                }
                FixLegPosition();
            }
            isFacingRight = false;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            shootingPoint.transform.Rotate(0, 180, 0);
            animeDorsal.SetInteger("Speed", 1);
            animePernas.SetInteger("Speed", 1);
            if (delay < 0)
                dorsal.localScale = new Vector3(1, 1, 1);
            pernas.localScale = new Vector3(1, 1, 1);
            shootingPoint.localScale = new Vector3(1, 1, 1);
            if (!legFixedRight)
            {
                if (animeDorsal.GetBool("Jump"))
                {
                    FixAirPosition();
                }
                FixLegPosition();
            }
            isFacingRight = true;
        }
        else
        {
            animeDorsal.SetInteger("Speed", 0);
            animePernas.SetInteger("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && animePernas.GetBool("Grounded") && firstFix && delay < 0)
        {
            FixJumpPosition();
            animeDorsal.SetBool("Jump", true);
            animePernas.SetBool("Jump", true);
            animeDorsal.SetBool("Grounded", false);
            animePernas.SetBool("Grounded", false);
            body.AddForce(new Vector2(body.velocity.x, jumpForce));
        }
        if (!animePernas.GetBool("Jump"))
        {
            atirar();
        }
    }

   

    private void atirar()
    {
        if (Input.GetKeyDown("j") && delay <= 0 && firstFix)
        {
            currentBullet = Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
            
            if(isFacingRight && isFacingUp)
            {
                currentBullet.GetComponent<Bullet>().mudarpositionY();
            }
            else if(!isFacingRight && isFacingUp)
            {
                currentBullet.GetComponent<Bullet>().mudarpositionY();
            }
            else
            {
                currentBullet.GetComponent<Bullet>().mudarposica(isFacingRight);
            }
           
            
            shootFixed = false;
            animeDorsal.SetBool("Atirar", true);
            print("space key was pressed");
            FixShootPosition();
            delay = .25f;
        }
        else if (delay <= 0)
        {
            if (!shootFixed)
            {
                if (isFacingRight)
                {
                    dorsal.position = new Vector3(pernas.position.x + 0.037f * 8, pernas.position.y + 0.154f * 8, dorsal.position.z);
                }
                else
                {
                    dorsal.position = new Vector3(pernas.position.x - 0.037f * 8, pernas.position.y + 0.154f * 8, dorsal.position.z);
                }
                shootFixed = true;
            }
            animeDorsal.SetBool("Atirar", false);
        }
    }

    public void FixShootPosition()
    {
        if (isFacingUp)
        {
            dorsal.position = new Vector3(dorsal.position.x + .08f, dorsal.position.y + 0.2f * 8, dorsal.position.z);
        }
        else if (isFacingRight && !isFacingUp)
        {
            dorsal.position = new Vector3(dorsal.position.x + 0.08f * 8, dorsal.position.y, dorsal.position.z);
        }
        else if (!isFacingRight && !isFacingUp)
        {
            dorsal.position = new Vector3(dorsal.position.x - 0.08f * 8, dorsal.position.y, dorsal.position.z);
        }
    }

    private void FixLegPosition()
    {
        if (!legFixedRight && firstFix)
        {
            legFixedRight = true;
            legFixedLeft = false;
            pernas.position = new Vector3(pernas.position.x - 0.8f, pernas.position.y, pernas.position.z);
        }
        else if (!legFixedLeft || firstFix)
        {
            firstFix = true;
            legFixedLeft = true;
            legFixedRight = false;
            pernas.position = new Vector3(pernas.position.x + 0.8f, pernas.position.y, pernas.position.z);
        }
    }

    private void FixJumpPosition()
    {
        if (!legFixedRight)
        {
            dorsal.position = new Vector3(dorsal.position.x + 0.07f * 8, dorsal.position.y + 0.06f * 8, dorsal.position.z);
        }
        else if (!legFixedLeft)
        {
            dorsal.position = new Vector3(dorsal.position.x - 0.07f * 8, dorsal.position.y + 0.06f * 8, dorsal.position.z);
        }
    }

    private void FixAirPosition()
    {
        if (!legFixedRight)
        {
            dorsal.position = new Vector3(dorsal.position.x - 0.11f * 8, dorsal.position.y, dorsal.position.z);
        }
        else if (!legFixedLeft)
        {
            dorsal.position = new Vector3(dorsal.position.x + 0.11f * 8, dorsal.position.y, dorsal.position.z);
        }
    }

    private void FixGroundPosition()
    {
        if (!legFixedRight)
        {
            dorsal.position = new Vector3(pernas.position.x - 0.037f * 8, pernas.position.y + 0.154f * 8, dorsal.position.z);
        }
        else if (!legFixedLeft)
        {
            dorsal.position = new Vector3(pernas.position.x + 0.037f * 8, pernas.position.y + 0.154f * 8, dorsal.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            if (animeDorsal.GetBool("Jump"))
                FixGroundPosition();
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
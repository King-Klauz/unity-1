using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    // Start is called before the first frame update
    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private float m_speed, m_jumpForce;

    [SerializeField]
    private Animator m_anim;

    private bool isJumping;



    private void FixedUpdate()
    {
        if (!isJumping)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.localScale = Vector3.one;
                //m_rigidbody.MovePosition(transform.position + m_speed * Time.deltaTime * Vector3.right);
                m_anim.SetFloat("Speed", 1);
                m_rigidbody.velocity = Vector2.right * m_speed;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                m_anim.SetFloat("Speed", 1);
                m_rigidbody.velocity = Vector2.left * m_speed;
                //m_rigidbody.MovePosition(transform.position + m_speed * Time.deltaTime * Vector3.left);
            }
            else
            {
                m_rigidbody.velocity = Vector2.zero;
                m_anim.SetFloat("Speed", 0);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            m_rigidbody.AddForce(Vector2.up * m_jumpForce);
            m_anim.SetBool("IsJumping", true);
            StartCoroutine(CheckGround());
        }

    }

    IEnumerator CheckGround()
    {
        yield return new WaitForSeconds(.1f);
        while (isJumping)
        {
            if (m_rigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                m_anim.SetBool("IsJumping", false);
                isJumping = false;
            }
            yield return null;
        }
    }


}

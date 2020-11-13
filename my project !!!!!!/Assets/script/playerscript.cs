using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    private Rigidbody2D rb;// Loome kastid rigibody jaoks
    public float speed;// Loome muutujad kiiruse jaoks
    private bool facingright;
    private Animator anim;
    public float jumpforce;
    private static bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// Paneme rigibody kasti rigibody komponendi
        facingright = true;
        anim = GetComponent<Animator>();


    }

    void Move(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(horizontal));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "lava block")
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (collision.collider.tag == "ground")

        {

            isJumping = false;


        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")

        {

            isJumping = true;


        }
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingright || horizontal < 0 && facingright)
        {
            facingright = !facingright;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Loome liikumise suuna
        Move(horizontal);
        Flip(horizontal);

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }


    }
}
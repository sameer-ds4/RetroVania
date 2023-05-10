using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    private float extrajump;
    private bool jump;
    public int playerHealth;

    public ParticleSystem dust;
    public ParticleSystem puff;

    public Animator playeranimations;
    private float direction;

    private bool isgrounded;
    [SerializeField] private float radiuscheck;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask ground;

    private Rigidbody2D rb;
    public static playercontroller instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // --------------Player movement------------------------------------------------------------------------------------------
        direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * movespeed, rb.velocity.y);
        playeranimations.SetFloat("speed", Mathf.Abs(direction));

        // --------------Attack-----------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Z))
            playeranimations.SetTrigger("Attack");

        //----------------Sprite flip----------------------------------------------------------------------------------------------
        if (direction > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        // --------------ground check------------------------------------------------------------------------------------------------
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, radiuscheck, ground);
    }

    private void Update()
    {
        //----------------ground check--------------------------------------------------------------------------------------------
        if (isgrounded == true)
        {
            extrajump = 1;
            playeranimations.SetBool("jumping", false);
        }

        if (rb.velocity.x != 0 && isgrounded)
        {
            dust.Play();
        }
        else dust.Stop();

        //---------------Jump check and animation---------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playeranimations.SetBool("jumping", true);
            puff.Play();
        }
        else puff.Stop();

        //------------------jump physics-----------------------------------------------------------------------------------------------------------
        if (jump && extrajump > 0)
        {
            //rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            extrajump--;
            jump = false;
        }
        else if(jump && isgrounded && extrajump == 0)
        {
            //rb.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jump = false;
        }
        playeranimations.SetFloat("jump", rb.velocity.y);
    }

    //-------------------------player death logic------------------------------------------------------------------------------------------------
    public void deathlogic()
    {
        if(playerHealth < 1)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    }


    //-------------------------collision detection ------------------------------------------------------------------------------------------------
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            playerHealth -= 1;
        } 
        
        if(collision.gameObject.tag == "Food")
        {
            playerHealth += 1;
        }
    }
}

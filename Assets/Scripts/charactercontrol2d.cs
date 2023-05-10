using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontrol2d : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    [SerializeField] private Animator playeranims;

    private float movement;
    private float jump;
    private bool jumpstate;
    private Vector2 direction;
    private Rigidbody2D rb1;
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    private void Update()
    {
        // Get the inputs
        movement = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");

        // Storing the inputs collectively
        direction = new Vector2(movement, jump);

        // Flipping the sprite
        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movement < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        // Jump animation
        if (Input.GetButtonDown("Jump"))
        {
            playeranims.SetBool("jumping", true);
            jumping();
        } else
            playeranims.SetBool("jumping", false);
    }

    private void move()
    {
        rb1.velocity = (new Vector2(movement * movespeed, jump * jumpforce));
        playeranims.SetFloat("speed", Mathf.Abs(movement));

    }

    private void jumping()
    {
        rb1.velocity = new Vector2(rb1.velocity.x, 0);
        rb1.velocity += Vector2.up * jumpforce;
        //rb1.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
        //rb1.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
    }
}

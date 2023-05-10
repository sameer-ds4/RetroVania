using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterjump : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float fallmultiplier;
    [SerializeField] private float lowfallmultiplier;

    private Rigidbody2D rb1;
    private void Awake()
    {
        rb1 = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {

        if (rb1.velocity.y < 0)
        {
            rb1.gravityScale = fallmultiplier;
        }
        else if (rb1.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb1.gravityScale = lowfallmultiplier;
        }
        else
            rb1.gravityScale = 1f;
    }
}

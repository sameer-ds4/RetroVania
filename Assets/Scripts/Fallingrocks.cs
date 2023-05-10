using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallingrocks : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float distance;

    [SerializeField] private float shakeamount;
    private Vector3 originalposition;
    private Vector3 newposition;
    private bool shaking;

    private Rigidbody2D rb;
    private BoxCollider2D rock;
    private bool fall;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rock = GetComponent<BoxCollider2D>();
        originalposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(fall == false)
        {
            raycast(point1);
            raycast(point2);
        }
        if(shaking)
        {
            newposition = originalposition + Random.insideUnitSphere * (Time.deltaTime * shakeamount);
            transform.position = newposition;
        }
    }

    void raycast(Transform point)
    {
        RaycastHit2D hitpoint = Physics2D.Raycast(point.position, Vector2.down, distance);

        Debug.DrawRay(point.position, Vector2.down * distance, Color.red);

        if (hitpoint.collider != null)
        {
            if (hitpoint.collider.tag == "Player")
            {
                StartCoroutine("shakenow");
                Invoke("fallnow", 0.5f);
            }
        }
    }

    void fallnow()
    {
        rb.gravityScale = 1;
        fall = true;
    }

    IEnumerator shakenow()
    {
        originalposition = transform.position;
        if (shaking == false)
            shaking = true;
        yield return new WaitForSeconds(0.4f);
        shaking = false;
        transform.position = originalposition;
    }

}

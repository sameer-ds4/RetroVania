using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundfall : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D ground;

    [SerializeField] private float shakeamount;
    private Vector3 originalposition;
    private Vector3 newposition;
    private bool shaking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(shaking)
        {
            newposition = originalposition + Random.insideUnitSphere * (Time.deltaTime * shakeamount);
            transform.position = newposition;
        }    
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("shakenow");
            Invoke("fallnow", 0.5f);
        }
    }

    void fallnow()
    {
        rb.gravityScale = 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointmove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startpoint;
    [SerializeField] private Transform[] points;
    [SerializeField] private bool platform;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startpoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            SpriteFlip();
            if (i == points.Length)
                i = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, Time.deltaTime * speed);
    }

    private void SpriteFlip()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(platform)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.collider.transform.SetParent(transform);
            }    
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}

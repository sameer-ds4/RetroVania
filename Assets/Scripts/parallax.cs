using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    /* [SerializeField] private Vector2 parallaxmultiplier;

     private Transform cameratransform;
     private Vector3 lastcameraposition;
     private void Start()
     {
         cameratransform = Camera.main.transform;
         lastcameraposition = cameratransform.position;
     }

     private void LateUpdate()
     {
         Vector3 deltamovement = cameratransform.position - lastcameraposition;
         transform.position += new Vector3(deltamovement.x * parallaxmultiplier.x, deltamovement.y * parallaxmultiplier.y);
         lastcameraposition = cameratransform.position;
     }*/
    private float length, startpos, ypos;
    public GameObject cam;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        ypos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float ydist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startpos + dist, ypos + ydist, transform.position.z);
        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}

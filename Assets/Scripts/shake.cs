using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    [SerializeField] private float threshold;
    private bool shaking;
    public static Vector3 originalpos;

    // Start is called before the first frame update
    void Start()
    {
        originalpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
            Vector3 newpos = originalpos + Random.insideUnitSphere * (Time.deltaTime * threshold);
            transform.position = newpos;
        }
    }

    public void objshake()
    {
        StartCoroutine("shaker");
    }

    IEnumerator shaker()
    {
        originalpos = transform.position;
        if (shaking == false)
            shaking = true;
        yield return new WaitForSeconds(0.25f);

        shaking = false;
        transform.position = originalpos;
    }
}

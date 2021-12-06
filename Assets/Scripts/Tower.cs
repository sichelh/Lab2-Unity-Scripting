using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float force = 3.0f;
    public GameObject sphere;
    public int height = 10;

    private GameObject p;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        for (int y=0; y<height; ++y)
        {
            Instantiate(sphere, new Vector3(0, 3+y, 0), Quaternion.identity);
        }

        p = GameObject.FindWithTag("p");
        rb = p.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.right * force);
        force -= 0.2f;
       
    }
}

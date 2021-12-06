using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 2.0F;

    private bool ishide;
    void Update()
    {
        

        //player hide
        if (Input.GetKeyDown("h"))
        {
            ishide = !ishide;
            
        }
        if (!ishide)
        {
            transform.LookAt(target);

            Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);
            transform.Translate(movement);

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            //print("Hide!! Don't Follow!");
        }
    }

}

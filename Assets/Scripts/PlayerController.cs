using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100.0f;
    public static bool gameIsPaused;
    public AudioSource sound;

    public float radius = 5.0F;
    public float power = 200.0F;
    public float lift = 30;

    private int num = 0;
    private Rigidbody rb;
    private Renderer h;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        h = GetComponent<Renderer>();

        if( rb == null)
        {
            Debug.Log("[Error] No rb available");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //color
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        //mass
        if (Input.GetKeyDown("m"))
        {
            rb.mass++;
        }
        if (Input.GetKeyDown("n") && rb.mass>1)
        {
            rb.mass--;
        }

        //hide
        if (Input.GetKeyDown("h"))
        {
            if (h.enabled)
            {
                h.enabled = false;
            }
            else
            {
                h.enabled = true;
            }
        }

        //pause
        if (Input.GetKeyDown("p"))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }


    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontal, 0, vertical);
            rb.AddForce(movement * speed * Time.fixedDeltaTime);
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

        if (collision.gameObject.CompareTag("Bomb"))
        {
            Vector3 explosionPos = transform.position;

            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.GetComponent<Rigidbody>())
                    hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, lift);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            num++;
            Destroy(other.gameObject);
            print("Foods: " + num);
            sound.Play();
        }
    }


}

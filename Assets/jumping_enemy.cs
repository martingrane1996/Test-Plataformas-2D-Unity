using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumping_enemy : MonoBehaviour
{
    public float jumpForce;
    public float waitTime;

    public Vector3 startPos;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (transform.position.y == startPos.y)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }

        /*
        transform.Rotate(0f, 0f, 0f);
        Wait(waitTime);
        rb2d.gravityScale = 0f;
        */
    }

    private void Rotate()
    {
        while(transform.rotation.z < 180)
        {
            transform.Rotate(0f, 0f, transform.rotation.z + 1f * Time.deltaTime);
        }
    }

    private void Wait(float time)
    {
        while(time > 0)
        {
            time -= Time.deltaTime;
        }
    }
}

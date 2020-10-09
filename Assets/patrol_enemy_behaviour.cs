using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol_enemy_behaviour : MonoBehaviour
{
    public Transform groundDetector;
    public Transform wallDetector;

    public LayerMask groundLayer;
    public LayerMask wallLayer;

    public float movSpeed;
    private float movDirection;
    public float groundDetectDistance;
    public float wallDetectDistance;

    public bool groundDetected;
    public bool wallDetected;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        movDirection = 1;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
        DetectEnv();
    }

    private void Behaviour()
    {
        if (!groundDetected || wallDetected)
        {
            Flip();
        }

        rb2d.velocity = new Vector2(movSpeed * movDirection, rb2d.velocity.y);

        if (gameObject.GetComponent<health_for_enem_n_bosses>().getHealth() == 0)
        {
            Destroy(gameObject);
        }
    }

    private void DetectEnv()
    {
        wallDetected = Physics2D.Raycast(wallDetector.position, Vector2.right * movDirection, wallDetectDistance, wallLayer);
        groundDetected = Physics2D.Raycast(groundDetector.position, Vector2.down, groundDetectDistance, groundLayer);
    }

    private void Flip()
    {
        movDirection = -movDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool Esperar(int time)
    {
        while(time > 0)
        {
            time -= 1;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(wallDetector.position,
            new Vector3(wallDetector.position.x +  wallDetectDistance * movDirection, wallDetector.position.y, wallDetector.position.z));

        Gizmos.DrawLine(groundDetector.position,
            new Vector3(groundDetector.position.x, groundDetector.position.y - groundDetectDistance, groundDetector.position.z));
    }
}

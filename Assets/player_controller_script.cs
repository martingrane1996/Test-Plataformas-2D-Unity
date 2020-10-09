using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller_script : MonoBehaviour
{
    public bool touchingGround;
    public bool touchingWall;
    public bool spinning;
    public bool jumping;
    public bool wallClimbing;

    public Transform groundDetector;
    public Transform wallDetector;
    public Transform attackPosition;

    public float wallDetectDistance;
    public float groundDetectRadius;
    public float speedMovement;
    public float jumpHeight;
    public float facingDirection;
    private float movementDirection;
    public float attackRadius;
    public float verticalLookDirection;
    public float currentDirection;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        currentDirection = facingDirection = 1;
        rb2d = GetComponent<Rigidbody2D>();
        jumping = spinning = false;

        /*Ver como setear layers*/
        //groundLayer.value.Equals("platform");
        //wallLayer.value.Equals("wall");
    }

    // Update is called once per frame
    void Update()
    {
        MovementControler();
        ActivateWallClimbing();
        LookDirection();
        EnvDetectors();
        JumpandSpin();
        EnemyAttack();
        Flip();
    }

    private void MovementControler()
    {
        if(!wallClimbing)
        {
            HorizontalMovement();
            LookDirection();
        }
        else if(wallClimbing)
        {
            WallClimbing();
        }
    }
    private void HorizontalMovement()
    {
        movementDirection = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(speedMovement * movementDirection, rb2d.velocity.y);
    }

    private void LookDirection()
    {
        verticalLookDirection = Input.GetAxis("Vertical");
    }

    //Arreglar detección a la pared pq no funciona y el asunto de facingDirection xa walldetector

    private void JumpandSpin()
    {
        if(touchingGround)
        {
            jumping = spinning = false;

            if(Input.GetKeyDown("x"))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
                jumping = true;
            }
        }
        else
        {
            jumping = true;
        }

        if (jumping && Input.GetKey("x"))
        {
            spinning = true;
        }
        else if (jumping && !Input.GetKey("x"))
        {
            spinning = false;
        }

    }

    private void ActivateWallClimbing()
    {
        if (spinning && jumping && touchingWall)
        {
            wallClimbing = true;
            rb2d.gravityScale = 0;
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    //Arreglar WallClimbing y hacer salto de pared
    //Ver como hacer que quede mirando siempre para la pared
    private void WallClimbing()
    {

        if (wallClimbing && touchingWall)
        {
            rb2d.velocity = new Vector2(0, speedMovement * verticalLookDirection);
            jumping = false;
        }

        if (wallClimbing && Input.GetKeyDown("x"))
        {
            wallClimbing = !wallClimbing;
            jumping = true;
            rb2d.gravityScale = 1;
            rb2d.velocity = new Vector2(speedMovement * -movementDirection, jumpHeight * 0.8f);
        }

        if (wallClimbing && !touchingWall)
        {
            wallClimbing = !wallClimbing;
            rb2d.gravityScale = 1;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight/1.5f);
        }
    }

    //Ver de hacer salto para abajo en plataformasStandOn


    IEnumerator Esperar(float mins)
    {
        yield return new WaitForSeconds(mins);
    }

    private void EnvDetectors()
    {
        touchingGround = Physics2D.OverlapCircle(groundDetector.position, groundDetectRadius,groundLayer);
        
        if(!wallClimbing)
        {
            currentDirection = 0;
            touchingWall = Physics2D.Raycast(wallDetector.position,
                new Vector3(wallDetector.position.x * facingDirection, wallDetector.position.y, wallDetector.position.z),
                wallDetectDistance, wallLayer);
        }
        else if(wallClimbing)
        {
            if(currentDirection == 0)
            {
                currentDirection = facingDirection;
            }
            
            touchingWall = Physics2D.Raycast(wallDetector.position, 
                new Vector3(wallDetector.position.x * currentDirection, wallDetector.position.y, wallDetector.position.z),
                wallDetectDistance, wallLayer);
        }

        
    }

    private void EnemyAttack()
    {
        if(Input.GetKeyDown("z"))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, enemyLayer);

            foreach(Collider2D hitenemy in enemies)
            {
                hitenemy.GetComponent<health_for_enem_n_bosses>().takeDamage(1);
                //Aregar funcion en los enemigos para tomar daño
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundDetector.position, groundDetectRadius);
        Gizmos.DrawSphere(attackPosition.position, attackRadius);
        Gizmos.DrawLine(wallDetector.position,
            new Vector3(wallDetector.position.x + wallDetectDistance * facingDirection, wallDetector.position.y,
            wallDetector.position.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bouncer"))
        {
            if (Input.GetKey("x"))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight * 1.5f);
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight * 0.5f);
            }
        }

        if (collision.CompareTag("enemy") && Input.GetKey("x"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            Destroy(collision.gameObject);
        }
    }

    private void Flip()
    {
        if(Input.GetKey("right"))
        {
            if (facingDirection == -1)
            {
                facingDirection = 1;
                transform.Rotate(0f, 180f, 0f);
            }

        }

        if (Input.GetKey("left"))
        {
            if(facingDirection == 1)
            {
                facingDirection = -1;
                transform.Rotate(0f, 180f, 0f);
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemy_behaviour_shooter : MonoBehaviour
{
    public GameObject proyectile;

    public bool canMove;
    public bool objDetected;

    public float objDetectDistance;
    private float lookDirection;
    public float timeWaiting;

    public LayerMask detectLayer;

    // Start is called before the first frame update
    void Start()
    {
        lookDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
        DetectEnv();
    }

    private void Behaviour()
    {
        if(objDetected && TimePassed(timeWaiting))
        {
            Instantiate(proyectile,transform.position,Quaternion.identity);
        }
    }

    private void DetectEnv()
    {
        objDetected = Physics2D.Raycast(transform.position, Vector2.right, objDetectDistance * lookDirection, detectLayer);
    }

    private bool TimePassed(float time)
    {
        float countdown = 0;

        while (countdown < time)
        {
            countdown += Time.deltaTime;
        }

        Debug.Log("Tiempo ha pasado");
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + (objDetectDistance * lookDirection), transform.position.y, transform.position.z));
    }

    private void Flip()
    {
        lookDirection = -lookDirection;

        if(lookDirection == -1)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (lookDirection == 1)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }    
}

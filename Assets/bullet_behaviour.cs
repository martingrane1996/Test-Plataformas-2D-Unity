using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEditor.UI;
using UnityEngine;

public class bullet_behaviour : MonoBehaviour
{
    public bool isHoming;
    public bool destroysAfterSeconds;
    public bool constantlyRotates;

    public float secondsToDestroy;
    public float speed;
    public float rotationSpeed;

    public GameObject objetive;

    private Vector3 fixedposition;

    // Start is called before the first frame update
    void Start()
    {
        objetive = GameObject.FindGameObjectWithTag("ply");
        fixedposition = objetive.transform.position;
    }
    

    // Update is called once per frame
    void Update()
    {
        Trayectory();
        Destruction();
    }

    private void Trayectory()
    {
        //Ver esto de Debug.Log
        if(isHoming)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetive.transform.position, speed * Time.deltaTime);
            //Debug.Log("Es teledirigido");
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, fixedposition, speed * Time.deltaTime);
        }

        if(constantlyRotates)
        {
            transform.Rotate(0f, 0f, rotationSpeed);
        }
    }

    private void Destruction()
    {
        if(destroysAfterSeconds)
        {
            Destroy(gameObject,secondsToDestroy);
        }
    }
    private bool calculateDistance()
    {
        if(Vector2.Distance(transform.position,objetive.transform.position) >= 5f)
        {
            if(objetive.CompareTag("Ply"))
            {
                return true;
            }
        }

        return false;
    }
}

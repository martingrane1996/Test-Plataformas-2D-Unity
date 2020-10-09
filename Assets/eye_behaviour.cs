using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eye_behaviour : MonoBehaviour
{
    //public bool notBounce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
    }

    private void Behaviour()
    {
        if (gameObject.GetComponent<health_for_enem_n_bosses>().getHealth() == 0)
        {
            Destroy(gameObject);
        }
    }
}

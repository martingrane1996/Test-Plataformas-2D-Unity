using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_for_enem_n_bosses : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        //Ver como setear energia individualmente
    }

    public void takeDamage(int damagePoints)
    {
        health -= 1;
    }

    public int getHealth()
    {
        return health;
    }
}

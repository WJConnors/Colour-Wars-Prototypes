using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int health = 100;

    public int Health {get { return health; }set { health = value; }}

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

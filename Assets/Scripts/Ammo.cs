using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public List<GameObject> movers = new List<GameObject>();
    public GameObject mover = null;
    bool inTower;
    public Enemy target;
    public float speed = 1.0f;

    void Update()
    {
        if (target)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
       else if (!mover)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.6f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("AmmoMover") && !movers.Contains(collider.gameObject))
                {
                    mover = collider.gameObject;
                    movers.Add(mover);                    
                    GetComponent<Rigidbody2D>().velocity = collider.transform.position - transform.position;
                }
                else if (collider.CompareTag("Tower") && !inTower)
                {
                    inTower = true;
                    Tower tower = collider.GetComponent<Tower>();
                    tower.containedAmmo.Add(this);
                    transform.position = tower.transform.position;
                    GetComponent<SpriteRenderer>().enabled = false;
                    tower.ColorUpdate();
                }
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, mover.transform.position) > 0.6f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                mover = null;
            }


        }
    }
}
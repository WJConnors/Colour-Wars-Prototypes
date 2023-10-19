using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    Mine mine;
    [SerializeField] Ammo ammoPrefab;

    void Start()
    {
        CheckMine();
        StartCoroutine(SpawnAmmo());
    }

    void CheckMine()
    {
        //look for triggers with tag Mine within a 0.5 radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Mine"))
            {
                mine = collider.GetComponent<Mine>();
                GetComponent<SpriteRenderer>().color = mine.Color;
            }
        }
    }
    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("AmmoMover"))
                {
                    Vector2 spawnPoint = (collider.transform.position + transform.position) / 2;
                    Ammo ammo = Instantiate(ammoPrefab, spawnPoint, Quaternion.identity);
                    ammo.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                }
                else if (collider.CompareTag("Tower"))
                {
                    Ammo ammo = Instantiate(ammoPrefab, collider.transform.position, Quaternion.identity);
                    ammo.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }

}

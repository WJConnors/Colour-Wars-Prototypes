using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMover : MonoBehaviour
{
    
    ResourceManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<ResourceManager>();
        if (!gameManager.AmmoMovers.Contains(this))
            gameManager.AmmoMovers.Add(this);
        StartCoroutine(SpawnAmmo());
    }

    IEnumerator SpawnAmmo()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            gameManager.SpawnAmmo(this);
        }   
    }
}

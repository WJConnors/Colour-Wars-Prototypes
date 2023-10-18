using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    
    ResourceManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<ResourceManager>();
        GetComponent<SpriteRenderer>().color = gameManager.MinerOverlap(this);
        if (!gameManager.Miners.Contains(this))
            gameManager.Miners.Add(this);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    ResourceManager gameManager;
    public List<AmmoMover> movers = new List<AmmoMover>();
    public AmmoMover mover = null;


    void Start()
    {
        gameManager = FindObjectOfType<ResourceManager>();
    }

    void Update()
    {
        
    }
}
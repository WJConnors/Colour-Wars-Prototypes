using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Ore orePrefab;
    [SerializeField] int oreCount = 20;
    ResourceManager gameManager;

    public Color Color { get => color; }

    void Start()
    {
        gameManager = FindObjectOfType<ResourceManager>();
        if (!gameManager.Mines.Contains(this))
            gameManager.Mines.Add(this);
        for (int i = 0; i < oreCount; i++)
        {
            Ore ore = Instantiate(orePrefab, transform.position, Quaternion.identity, transform);
            ore.GetComponent<SpriteRenderer>().color = color;
        }
    }
}

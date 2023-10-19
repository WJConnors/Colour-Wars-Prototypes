using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Ore orePrefab;
    [SerializeField] int oreCount = 20;

    public Color Color { get => color; }

    void Start()
    {
        for (int i = 0; i < oreCount; i++)
        {
            Ore ore = Instantiate(orePrefab, transform.position, Quaternion.identity, transform);
            ore.GetComponent<SpriteRenderer>().color = color;
        }
    }
}

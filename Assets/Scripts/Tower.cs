using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Ammo> containedAmmo = new List<Ammo>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ColorUpdate()
    {
        Color newColor;
        int colorAmount;
        if (spriteRenderer.color == new Color(1f, 1f, 1f, 1f))
        {
            newColor = new Color(0f, 0f, 0f, 1f);
        } else
        {
            newColor = spriteRenderer.color;
        }
        if (containedAmmo.Count <= 5)
        {
            colorAmount = 5;
        } else
        {
            colorAmount = containedAmmo.Count;
        }

        foreach (Ammo ammo in containedAmmo)
        {
            newColor += ammo.GetComponent<SpriteRenderer>().color;
        }   
        newColor.r /= colorAmount;
        newColor.g /= colorAmount;
        newColor.b /= colorAmount;
        spriteRenderer.color = newColor;

    }

}

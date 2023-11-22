using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Ammo> containedAmmo = new List<Ammo>();
    public bool readyToFire = true;
    public float reloadTime = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f);
        List<Ammo> ammoToRemove = new List<Ammo>();
        foreach (Ammo ammo in containedAmmo)
        {
            foreach (Collider2D collider in colliders)
            {
                Debug.Log("foundsomething");
                if (collider.CompareTag("Enemy"))
                {
                    Debug.Log("foundenemy");
                    if (readyToFire)
                    {
                        StartCoroutine(Reload());
                        ammo.target = collider.GameObject();
                        ammoToRemove.Add(ammo);
                    }
                }
            }            
        }
        foreach (Ammo ammo2 in ammoToRemove)
        {
            containedAmmo.Remove(ammo2);
        }
        ColorUpdate();
        ammoToRemove.Clear();

    }

    IEnumerator Reload()
    {
        readyToFire = false;
        yield return new WaitForSeconds(reloadTime);
        readyToFire = true;
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

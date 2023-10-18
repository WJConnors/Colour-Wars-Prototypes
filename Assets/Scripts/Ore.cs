using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] float maxAlpha = 0.9f;
    [SerializeField] float minAlpha = 0.2f;
    [SerializeField] float mineBounds;
    bool alphaLowering;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float alphaChangeSpeed = 0.01f;
    Vector2 currentMovement;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alphaLowering = (Random.value > 0.5f);
        spriteRenderer.color += new Color (0,0,0,Random.value);
        currentMovement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = currentMovement;
        StartCoroutine(AlphaPhasing());
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            if (Mathf.Abs(transform.localPosition.x) > mineBounds || Mathf.Abs(transform.localPosition.y) > mineBounds)
            {
                currentMovement *= -1;
                rb.velocity = currentMovement;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator AlphaPhasing()
    {
        while (true)
        {
            if (alphaLowering)
            {
                spriteRenderer.color -= new Color(0,0,0, alphaChangeSpeed);
                if (spriteRenderer.color.a <= minAlpha)
                {
                    alphaLowering = false;
                }
            }
            else
            {
                spriteRenderer.color += new Color(0, 0, 0, alphaChangeSpeed);
                if (spriteRenderer.color.a >= maxAlpha)
                {
                    alphaLowering = true;
                }
            }
            yield return new WaitForSeconds(alphaChangeSpeed);
        }        
    }
}

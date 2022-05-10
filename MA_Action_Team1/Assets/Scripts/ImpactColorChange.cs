
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactColorChange : MonoBehaviour
{

    private float hits = 0;     //the # of this object's collisions
    private float r;               // red color, 0-1
    private float g;              // green color, 0-1
    private float b;              // blue color, 0-1

    void OnCollisionEnter2D(Collision2D other)
    {
        //if the impact has enough force
        if (other.relativeVelocity.magnitude > 5)
        {
            //increment hits #
            hits += 1;
            //randomize RGB color values, but go from warm to cool
            if (hits <= 4)
            {
                r = Random.Range(0.3f, 1f);
                g = Random.Range(0.1f, 0.5f);
                b = Random.Range(0f, 0.2f);
            }
            if (hits > 4)
            {
                r = Random.Range(0f, 0.3f);
                g = Random.Range(0f, 0.3f);
                b = Random.Range(0.3f, 1f);
            }

            SpriteRenderer squareRenderer = GetComponentInChildren<SpriteRenderer>();
            squareRenderer.color = new Color(r, g, b);
        }
    }
}
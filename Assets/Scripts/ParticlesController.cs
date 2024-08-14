using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public Color paintColor;

    // public float minRadius = 0.05f;
    // public float maxRadius = 0.2f;
    // public float strength = 1;
    // public float hardness = 1;
    public float tolerance = 0.01f;

    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if (other.TryGetComponent<Paintable>(out var p))
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(p.minRadius, p.maxRadius);

                // Check if the current paint color is allowed using the modified comparison
                if (p.allowedColors.Any(allowedColor => AreColorsSimilar(allowedColor, paintColor)))
                {
                    PaintManager.instance.Paint(p, pos, radius, p.hardness, p.strength, paintColor);

                    // Check if fully painted
                    if (p.IsFullyPainted())
                    {
                        Debug.Log("The paintable object is fully painted.");
                    }
                }
                else
                {
                    Debug.Log("Color not allowed");
                }
            }
        }
    }

    // Method to compare colors with a tolerance
    bool AreColorsSimilar(Color color1, Color color2)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance &&
               Mathf.Abs(color1.a - color2.a) < tolerance;
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;

// public class ParticlesController : MonoBehaviour
// {
//     public Color paintColor;

//     // public float minRadius = 0.05f;
//     // public float maxRadius = 0.2f;
//     // public float strength = 1;
//     // public float hardness = 1;
//     public float tolerance = 0.01f;

//     [Space]
//     ParticleSystem part;
//     List<ParticleCollisionEvent> collisionEvents;

//     void Start()
//     {
//         part = GetComponent<ParticleSystem>();
//         collisionEvents = new List<ParticleCollisionEvent>();
//     }

//     void OnParticleCollision(GameObject other)
//     {
//         int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

//         if (other.TryGetComponent<Paintable>(out var p))
//         {
//             for (int i = 0; i < numCollisionEvents; i++)
//             {
//                 Vector3 pos = collisionEvents[i].intersection;
//                 float radius = Random.Range(p.minRadius, p.maxRadius);

//                 // Check if the current paint color is allowed using the modified comparison
//                 if (p.allowedColors.Any(allowedColor => AreColorsSimilar(allowedColor, paintColor)))
//                 {
//                     PaintManager.instance.Paint(p, pos, radius, p.hardness, p.strength, paintColor);

//                     // Check if fully painted
//                     if (p.IsFullyPainted())
//                     {
//                         Debug.Log("The paintable object is fully painted.");
//                     }
//                 }
//                 else
//                 {
//                     Debug.Log("Color not allowed");
//                 }
//             }
//         }
//     }

//     // Method to compare colors with a tolerance
//     bool AreColorsSimilar(Color color1, Color color2)
//     {
//         return Mathf.Abs(color1.r - color2.r) < tolerance &&
//                Mathf.Abs(color1.g - color2.g) < tolerance &&
//                Mathf.Abs(color1.b - color2.b) < tolerance &&
//                Mathf.Abs(color1.a - color2.a) < tolerance;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    public Color paintColor;
    public float tolerance = 0.01f;
    public int raycastStep = 2; // Only raycast every nth particle
    public float maxRaycastDistance = 10f; // Maximum distance for raycasts
    public Paintable p;

    ParticleSystem part;
    ParticleSystem.Particle[] particles;
    List<Vector3> paintPositions;
    List<Paintable> paintables;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[part.main.maxParticles];
        paintPositions = new List<Vector3>();
        paintables = new List<Paintable>();
    }

    void Update()
    {
        int numParticlesAlive = part.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i += raycastStep)
        {
            Vector3 particlePosition = particles[i].position;
            Vector3 particleVelocity = particles[i].velocity;

            // Perform a raycast in the direction of the particle's velocity
            if (Physics.Raycast(particlePosition, particleVelocity.normalized, out RaycastHit hit, maxRaycastDistance))
            {
                if (hit.collider.TryGetComponent<Paintable>(out var paintable))
                {
                    p = paintable;
                    paintPositions.Add(hit.point);
                    paintables.Add(paintable);
                }
            }
        }

        // Batch process the paint positions after all raycasts
        for (int i = 0; i < paintPositions.Count; i++)
        {
            Vector3 pos = paintPositions[i];
            Paintable paintable = paintables[i];
            float radius = Random.Range(paintable.minRadius, paintable.maxRadius);

            if(paintable.restrictedColors) 
            {
                // Check if the current paint color is allowed
                if (paintable.allowedColors.Any(allowedColor => AreColorsSimilar(allowedColor, paintColor, p)))
                {
                    PaintManager.instance.Paint(paintable, pos, radius, paintable.hardness, paintable.strength, paintColor);

                    // // Check if fully painted
                    // if (paintable.IsFullyPainted())
                    // {
                    //     Debug.Log("The paintable object is fully painted.");
                    // }
                }
                // else
                // {
                //     Debug.Log("Color not allowed");
                // }
            } else 
            {
                PaintManager.instance.Paint(paintable, pos, radius, paintable.hardness, paintable.strength, paintColor);
            }
        }

        // Clear the lists after processing
        paintPositions.Clear();
        paintables.Clear();
    }

    // Method to compare colors with a tolerance
    bool AreColorsSimilar(Color color1, Color color2, Paintable p)
    {
        return Mathf.Abs(color1.r - color2.r) < p.tolerance &&
               Mathf.Abs(color1.g - color2.g) < p.tolerance &&
               Mathf.Abs(color1.b - color2.b) < p.tolerance &&
               Mathf.Abs(color1.a - color2.a) < p.tolerance;
    }
}

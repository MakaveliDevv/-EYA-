using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class Pillar : MonoBehaviour
{
    public VolumetricLineBehavior volumetricLineBehavior;
    private Paintable p;
    private bool beamsWereInitiallyActive = false;



    // Variables for interpolation
    private bool isInterpolating = false;
    private Vector3 startPos;
    private Vector3 targetEndPos;
    private float interpolationDuration = 2.0f; // Duration in seconds
    private float interpolationTime = 0.0f;

    void Start()
    {
        p = GetComponent<Paintable>();

        // Store the initial start position and end position
        if (volumetricLineBehavior != null)
        {
            startPos = volumetricLineBehavior.StartPos;
            targetEndPos = volumetricLineBehavior.EndPos;
        }
    }

    void Update()
    {
        if (p.IsFullyPainted())
        {
            // Debug.Log("Pillar Painted");
            volumetricLineBehavior.gameObject.SetActive(true);
            beamsWereInitiallyActive = true;
            // Start the interpolation
            // isInterpolating = true;
            // interpolationTime = 0.0f;
        }

        // if (isInterpolating)
        // {
        //     // Increase the interpolation time
        //     interpolationTime += Time.deltaTime;

        //     // Calculate the interpolation factor (0.0 to 1.0)
        //     float t = interpolationTime / interpolationDuration;

        //     // Interpolate between startPos and targetEndPos
        //     if (volumetricLineBehavior != null)
        //     {
        //         volumetricLineBehavior.EndPos = Vector3.Lerp(startPos, targetEndPos, t);
        //     }

        //     // Check if the interpolation is complete
        //     if (t >= 1.0f)
        //     {
        //         isInterpolating = false;
        //     }
        // }
    }
}

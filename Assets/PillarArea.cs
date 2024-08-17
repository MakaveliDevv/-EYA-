using System.Collections.Generic;
using UnityEngine;

public class PillarArea : MonoBehaviour
{
    public List<GameObject> beams = new();
    public GameObject colorOrb;
    public Transform spawnPos;
    private bool colorUnlocked = false;
    private bool beamsWereInitiallyInactive = false;

    // void Start()
    // {
    //     // Check if at least one beam is inactive at the start
    //     foreach (var beam in beams)
    //     {
    //         if (!beam.activeInHierarchy)
    //         {
    //             beamsWereInitiallyInactive = true;
    //             break;
    //         }
    //     }
    // }

    void Update()
    {
        // Check if the beams were initially inactive and are now all active
        if (!colorUnlocked && AreAllBeamsActive())
        {
            Debug.Log("Unlocked new color: White");
            Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
            colorUnlocked = true;  // Prevent further instantiations
        }
    }

    private bool AreAllBeamsActive()
    {
        foreach (var beam in beams)
        {
            if (!beam.activeInHierarchy)
            {
                return false; // If any beam is not active, return false
            }
            else 
            {
                beamsWereInitiallyInactive = true;
            }
        }
        return beamsWereInitiallyInactive; // All beams are active
    }
}
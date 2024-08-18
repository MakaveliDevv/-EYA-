using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternArea : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public new GameObject light;
    public Transform spawnPos;
    public GameObject colorOrb; // Assign the orb prefab in the inspector or instantiate programmatically.
    private bool orbSpawned = false; // Track whether the orb has already been spawned.

    void Update()
    {
        // Check if all lanterns are fully painted.
        bool allLanternsPainted = true;

        foreach (var lantern in paintables)
        {
            if (lantern.lantern) 
            {
                if (!lantern.IsFullyPainted()) 
                {
                    allLanternsPainted = false;
                    break; // No need to check further if one lantern isn't fully painted.
                }
            }
        }

        // If all lanterns are fully painted and the orb hasn't been spawned yet, spawn the orb.
        if (allLanternsPainted && !orbSpawned)
        {
            SpawnOrangeOrb();
            orbSpawned = true; // Prevent the orb from being spawned multiple times.
        }
    }

    void SpawnOrangeOrb()
    {
        // Instantiate the orange orb at a desired position and rotation.
        Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
        light.SetActive(true);
        Debug.Log("Orange orb spawned!");
    }
}

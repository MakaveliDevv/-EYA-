using System.Collections.Generic;
using UnityEngine;

public class LanternArea : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public List<GameObject> paintedLanterns = new();
    public int maxAmount = 6;

    public new GameObject light;
    public Transform spawnPos;
    public GameObject colorOrb; // Assign the orb prefab in the inspector.
    private bool orbSpawned; // Track whether the orb has already been spawned.
    bool allLanternsPainted;


    void Update()
    {
        // Check if all lanterns are fully painted.
        foreach (var lantern in paintables)
        {
            if(lantern.IsFullyPainted()) 
            {
                if(!paintedLanterns.Contains(lantern.gameObject)) 
                {
                    paintedLanterns.Add(lantern.gameObject);
                }
            }
        }

        if(paintedLanterns.Count == maxAmount) 
        {
            allLanternsPainted = true;
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
        if (colorOrb != null && spawnPos != null)
        {
            // Instantiate the orange orb at the desired position and rotation.
            Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
            if (light != null)
            {
                light.SetActive(true);
            }
            Debug.Log("Orange orb spawned!");
        }
        else
        {
            Debug.LogWarning("colorOrb or spawnPos is not assigned!");
        }
    }
}

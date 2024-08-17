using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inside : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public bool onPath;
    int requiredFullyPainted;
    int fullyPaintedCount = 0;
    public GameObject colorOrb;
    public Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Paintable _paintable in paintables)
        {
            if (_paintable.IsFullyPainted())
            {
                fullyPaintedCount++;
            }
            
            // If the required number is reached, trigger an event and break out of the loop
            if (fullyPaintedCount >= requiredFullyPainted)
            {
                // Debug.Log("80% of Paintables are fully painted!");
                // Trigger your desired event here
                TriggerEvent();
                break;
            }
        }
    }

    private void TriggerEvent()
    {
        Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
        throw new NotImplementedException();
    }
}

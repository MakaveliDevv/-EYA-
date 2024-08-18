// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Inside : MonoBehaviour
// {
//     public List<Paintable> paintables = new();
//     public bool onPath;
//     int requiredFullyPainted;
//     int fullyPaintedCount = 0;
//     public GameObject colorOrb;
//     public Transform spawnPos;
//     private bool orbSpawned; 


//     // Start is called before the first frame update
//     void Start()
//     {
//         requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.8f);
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         foreach (Paintable _paintable in paintables)
//         {
//             if (_paintable.IsFullyPainted())
//             {
//                 fullyPaintedCount++;
//             }
            
//             // If the required number is reached, trigger an event and break out of the loop
//             if (fullyPaintedCount >= requiredFullyPainted)
//             {
//                 Debug.Log("80% of Paintables are fully painted!");
//                 // Trigger your desired event here
//                 TriggerEvent();
//                 break;
//             }
//         }
//     }

//     private void TriggerEvent()
//     {
//         if(!orbSpawned) 
//         {
//             Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
//             orbSpawned = true;
//         }
//     }
// }


using System.Collections.Generic;
using UnityEngine;

public class Inside : MonoBehaviour
{
    public List<Paintable> paintables = new List<Paintable>();
    public bool onPath;
    int requiredFullyPainted;
    int fullyPaintedCount = 0;
    public GameObject colorOrb;
    public Transform spawnPos;
    private bool orbSpawned;

    void Start()
    {
        requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.1f);

        foreach (Paintable paintable in paintables)
        {
            paintable.OnFullyPainted += HandleFullyPainted;
            Debug.Log("Subscribed to OnFullyPainted event for: " + paintable.gameObject.name);
        }

        // // Test event trigger
        // if (paintables.Count > 0)
        // {
        //     paintables[0].OnFullyPainted?.Invoke(paintables[0]);
        // }
        
    }

    private void HandleFullyPainted(Paintable paintable)
    {
        fullyPaintedCount++;
        Debug.Log("Handled fully painted: " + paintable.gameObject.name + " | Count: " + fullyPaintedCount);

        if (fullyPaintedCount >= requiredFullyPainted && !orbSpawned)
        {
            Debug.Log("80% of Paintables are fully painted!");
            TriggerEvent();
        }
    }


    private void TriggerEvent()
    {
        if (!orbSpawned)
        {
            Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
            orbSpawned = true;
        }
    }

    private void OnDestroy()
    {
        foreach (Paintable paintable in paintables)
        {
            paintable.OnFullyPainted -= HandleFullyPainted;
        }
    }
}


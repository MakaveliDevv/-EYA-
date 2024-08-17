using System.Collections.Generic;
using UnityEngine;

public class FirstPath : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public bool onPath;
    int requiredFullyPainted;
    int fullyPaintedCount = 0;
    public GameObject tree;
    public GameObject colorOrb;
    public Transform spawnPos;

    // Variables for tree interpolation
    private bool isTreeMoving = false;
    private Vector3 treeStartPos;
    private Vector3 treeTargetPos;
    private float treeMoveDuration = 3.0f; // Duration in seconds
    private float treeMoveTime = 0.0f;

    void Start()
    {
        // Ensure the onPath is initially false
        requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.75f);

        // // Initialize tree positions
        // if (tree != null)
        // {
        //     treeStartPos = tree.transform.position;
        //     treeTargetPos = treeStartPos + new Vector3(0f, 11.5f, 0f); // Move the tree upwards by 10 units
        // }
    }

    void Update()
    {
        foreach (var _paintable in paintables)
        {
            if(_paintable.onPath) 
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
        
        if(tree.activeInHierarchy) 
        {
            Instantiate(colorOrb, spawnPos.transform.position, Quaternion.identity);
        }

        // // Handle tree interpolation
        // if (isTreeMoving)
        // {
        //     treeMoveTime += Time.deltaTime;
        //     float t = treeMoveTime / treeMoveDuration;

        //     if (tree != null)
        //     {
        //         tree.transform.position = Vector3.Lerp(treeStartPos, treeTargetPos, t);
        //     }

        //     if (t >= 1.0f)
        //     {
        //         isTreeMoving = false; // Stop moving once interpolation is complete
        //     }
        // }
    }

    void TriggerEvent()
    {
        // Your logic for what should happen when 80% of paintables are fully painted
        Debug.Log("Event Triggered: 80% of Paintables are fully painted.");
        tree.SetActive(true);

        // Start the tree movement
        // isTreeMoving = true;
        // treeMoveTime = 0.0f;
    }
}

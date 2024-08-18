using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    public List<FireThingy> fireThingies = new List<FireThingy>();
    public GameObject stoneDoor_L, stoneDoor_R;
    public float moveSpeed = .5f; // Speed at which the door moves

    public Vector3 startPosition_L = new (29.2f, -15.45f, -104.53f), targetPosition_L = new (29.2f - 5.0f, -15.45f, -104.53f - 5.0f); 
    public Vector3 startPosition_R = new (37.7f, -15.45f, -95.7f), targetPosition_R = new (37.7f + 5f, -15.45f, -95.7f + 5f); 
    private bool doorOpening_L, doorOpening_R;
    public bool entrance_open;

    void Start()
    {
        // Calculate the target position by moving to the left from the current local position
        if (stoneDoor_L != null && stoneDoor_R != null)
        {
            stoneDoor_L.transform.localPosition = startPosition_L;
            stoneDoor_R.transform.localPosition = startPosition_R;
        }
    }

    void Update()
    {
        // Check if all fireThingies are active
        bool allActive = true;
        foreach (var fireThingy in fireThingies)
        {
            if (!fireThingy.active)
            {
                allActive = false;
                break;
            }
        }

        // If all are active, start opening the door
        if (allActive && !doorOpening_L && !doorOpening_R)
        {
            doorOpening_L = true;
            doorOpening_R = true;
            // Debug.Log("All fireThingies are active, door may now open");
        }

        // Move the door if it should be opening
        if (doorOpening_L && doorOpening_R && stoneDoor_L != null)
        {
            // Smoothly move the door towards the target position
            stoneDoor_L.transform.localPosition = Vector3.Lerp(stoneDoor_L.transform.localPosition, targetPosition_L, moveSpeed * Time.deltaTime);

            // Stop moving if the door has reached the target position
            if (Vector3.Distance(stoneDoor_L.transform.localPosition, targetPosition_L) < 0.01f)
            {
                stoneDoor_L.transform.localPosition = targetPosition_L;
                doorOpening_L = false;
            }
            
            // Smoothly move the door towards the target position
            stoneDoor_R.transform.localPosition = Vector3.Lerp(stoneDoor_R.transform.localPosition, targetPosition_R, moveSpeed * Time.deltaTime);

            // Stop moving if the door has reached the target position
            if (Vector3.Distance(stoneDoor_R.transform.localPosition, targetPosition_R) < 0.01f)
            {
                stoneDoor_R.transform.localPosition = targetPosition_R;
                doorOpening_R = false;
            }

            entrance_open = true;
        }
    }
}

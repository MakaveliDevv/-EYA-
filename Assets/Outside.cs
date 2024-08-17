using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    public List<FireThingy> fireThingies = new();

    void Update() 
    {
        foreach (var fireThingy in fireThingies)
        {
            if(fireThingy.active) 
            {
                Debug.Log("Both firethingies are active, door may now open");
            }
        }
    }
}

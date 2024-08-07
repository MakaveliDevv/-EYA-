using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager i;

    // Reference the color inv
    public List<GameObject> colors = new List<GameObject>(); 

    void Awake() 
    {
        if(i == null) 
        {
            i  = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public static GameManager GetInstance() 
    {
        return i;
    }
}

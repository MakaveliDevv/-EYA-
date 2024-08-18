using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klock : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public bool painted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in paintables)
        {
            if(item.IsFullyPainted()) 
            {
                painted = true;
                Debug.Log("clock is painted");
            }
        }
    }
}

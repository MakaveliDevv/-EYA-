using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager i;

    // Reference the color inv
    public List<GameObject> colorOrbs = new(); 
    public List<GameObject> startingColorOrbs = new();
    public Transform spawnPointStartingOrbs;
    public Transform playerInv;

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

    void Start() 
    {
        foreach (var orb in startingColorOrbs)
        {
            GameObject color = Instantiate(orb, spawnPointStartingOrbs.position, Quaternion.identity);
            colorOrbs.Add(color);
            color.transform.SetParent(playerInv);
            color.SetActive(false);
        }
    }

    public static GameManager GetInstance() 
    {
        return i;
    }
}

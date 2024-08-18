using System.Collections.Generic;
using UnityEngine;

public class FirstPath : MonoBehaviour
{
    public List<Paintable> paintables = new();
    public bool onPath;
    int requiredFullyPainted;
    int fullyPaintedCount_RedGrass = 0;
    public GameObject tree;
    public GameObject colorOrb;
    public Transform spawnPos;
    private int fullyPaintedCount_TreePart = 0;


    void Start()
    {
        requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.75f);
    }

    void Update()
    {
        foreach (var redGrass in paintables)
        {
            if(redGrass.redGrass && redGrass.onRedPath) 
            {
                if (redGrass.IsFullyPainted())
                {
                    fullyPaintedCount_RedGrass++;
                }
                
                if (fullyPaintedCount_RedGrass >= requiredFullyPainted)
                {
                    Debug.Log("RedPath Fully Painted");
                    TriggerEvent();
                    break;
                }
            }

        }
        
        if(tree.activeInHierarchy) 
        {
            Tree _Tree = tree.GetComponent<Tree>();

            foreach (Paintable part in _Tree.treeParts)
            {
                if(part.tree) 
                {
                    if (part.IsFullyPainted())
                    {
                        fullyPaintedCount_TreePart++;
                        Debug.Log("Tree");
                    }
                    
                    if (fullyPaintedCount_TreePart >= requiredFullyPainted)
                    {
                        Instantiate(colorOrb, spawnPos.transform.position, Quaternion.identity);
                        Debug.Log("Orb spawned");
 
                        break;
                    }
                }
            }
        }
    }

    void TriggerEvent()
    {
        tree.SetActive(true);
    }
    
}

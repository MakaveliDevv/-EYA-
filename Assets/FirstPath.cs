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
    private bool orbSpawned; 
    public bool redPath_Painted, tree_Painted;


    void Start()
    {
        requiredFullyPainted = Mathf.CeilToInt(paintables.Count * 0.85f);
    }

    void Update()
    {
        if(!redPath_Painted) 
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
                        redPath_Painted = true;
                        Debug.Log("RedPath Fully Painted");
                        TriggerEvent();
                        break;
                    }
                }

            }
        }
        
        if(tree.activeInHierarchy) 
        {
            TreeBeh _Tree = tree.GetComponent<TreeBeh>();

            if(!tree_Painted) 
            {
                foreach (Paintable part in _Tree.treeParts)
                {
                    if(part.tree) 
                    {
                        if (part.IsFullyPainted())
                        {
                            fullyPaintedCount_TreePart++;
                        }
                        
                        if (fullyPaintedCount_TreePart >= requiredFullyPainted)
                        {
                            tree_Painted = true;
                            SpawnOrb();

                            break;
                        }
                    }
                }
            }
        }
    }

    void SpawnOrb() 
    {
        if(!orbSpawned) 
        {
            Instantiate(colorOrb, spawnPos.transform.position, Quaternion.identity);
            Debug.Log("Yellow Orb Spawned");
            orbSpawned = true;
        }
    }

    void TriggerEvent()
    {
        tree.SetActive(true);
    }
    
}

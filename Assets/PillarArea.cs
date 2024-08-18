using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class PillarArea : MonoBehaviour
{
    public List<Paintable> grass = new();
    public List<Paintable> pillars = new();
    public List<GameObject> beams = new();
    public GameObject colorOrb;
    public Transform spawnPos;
    private int requiredFullyPainted;
    private int fullyPaintedCount = 0;
    private bool orbSpawned; 
    private bool yellowGrass_Painted;

    void Start()
    {
        requiredFullyPainted = Mathf.CeilToInt(grass.Count * 0.85f);
    }

    void Update()
    {
        if(!yellowGrass_Painted) 
        {
            foreach (var _grass in grass)
            {
                if(_grass.yellowGrass && _grass.onYellowPath) 
                {
                    if(_grass.IsFullyPainted()) 
                    {
                        fullyPaintedCount++;
                    }
                }

                if(fullyPaintedCount >= requiredFullyPainted) 
                {
                    yellowGrass_Painted = true;
                    break;
                }
            }
        }

        if(yellowGrass_Painted) 
        {
            foreach (var _pillar in pillars)
            {
                _pillar.pillar = false;
            }
        }

        if (!orbSpawned && AreAllBeamsActive())
        {
            Debug.Log("Unlocked new color: White");
            Instantiate(colorOrb, spawnPos.position, Quaternion.identity);
            orbSpawned = true;  
        }
    }

    private bool AreAllBeamsActive()
    {
        foreach (var pillar in pillars)
        {
            Pillar vfx = pillar.GetComponent<Pillar>();

            if(!vfx.isActive) 
            {
                return false;
            }
        }

        return true;    
    }
}
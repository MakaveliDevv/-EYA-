using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireThingy : MonoBehaviour
{
    public GameObject vfx;
    public Paintable p;

    public ParticleSystem particle;
    public bool active;

    void Awake() 
    {
        particle = vfx.GetComponent<ParticleSystem>();
        p = GetComponent<Paintable>();

        //particle.Stop();
    }

    void Update() 
    {
        if (p.IsFullyPainted())
        {
            vfx.SetActive(true);
            //particle.Play();
            active = true;
        }        
    }
}

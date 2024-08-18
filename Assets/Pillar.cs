using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public GameObject beam;
    private Paintable p;
    public bool isActive;

    void Start()
    {
        p = GetComponent<Paintable>();
    }

    void Update()
    {
        if (p.IsFullyPainted())
        {
            beam.SetActive(true);
            isActive = true;
        }
    }
}

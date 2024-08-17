using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject particle, water, colorOrb;
    public Transform spawnPos; 
    public ParticleSystem p_system;
    public Paintable p;
    public bool pondFilled = false;

    void Awake()
    {
        p = GetComponent<Paintable>();
        p_system = particle.GetComponent<ParticleSystem>();

        p_system.Stop();
    }

    void Update()
    {
        if(p.IsFullyPainted()) 
        {
            particle.SetActive(true);
            p_system.Play();

            StartCoroutine(WaitALilBit());
        }

        if(pondFilled) 
        {
            water.SetActive(true);
            Instantiate(colorOrb, spawnPos.transform.position, Quaternion.identity);
        }  
    }

    private IEnumerator WaitALilBit() 
    {
        yield return new WaitForSeconds(1.5f);
        pondFilled = true;
    }
}

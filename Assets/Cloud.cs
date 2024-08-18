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
    private bool orbSpawned; 

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
            Debug.Log("Cloud is fully painted");
            particle.SetActive(true);
            p_system.Play();

            StartCoroutine(WaitALilBit());
        }

        if(pondFilled) 
        {
            water.SetActive(true);
            SpawnOrb();
        }  
    }

    private IEnumerator WaitALilBit() 
    {
        yield return new WaitForSeconds(1.5f);
        pondFilled = true;
    }

    private void SpawnOrb() 
    {
        if(!orbSpawned) 
        {
            Instantiate(colorOrb, spawnPos.transform.position, Quaternion.identity);
            orbSpawned = false;
        }
    }
}

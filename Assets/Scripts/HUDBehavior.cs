using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehavior : MonoBehaviour
{
    public string ColorName;
    private Collectable c;
    public GameObject bg;
    public Button btn;
    public bool locked = true;
    public bool selected = false;

    // Static list to hold reference to all HUDBehaviour instances
    private static List<HUDBehavior> instances = new List<HUDBehavior>();

    void Awake()
    {
        instances.Add(this);
    }    
    
    void Update()
    {
        if(GameManager.GetInstance().colors.Count > 0)
        {
            foreach (var item in GameManager.GetInstance().colors)
            {
                c = item.GetComponent<Collectable>();
                if(c.Name == ColorName)
                {
                    btn.gameObject.SetActive(true);
                    bg.SetActive(false);
                    locked = false;
                }
            }
        }
    }

    public void Selected() 
    {
        // Unselect all other instances
        foreach (var HUD in instances)
        {
            if(HUD != this) 
                HUD.selected = false;
            
        }
        
        selected = true;
        SplatGun.GetInstance().colors.Clear();
        SplatGun.GetInstance().colors.Add(c.color);

        SplatGun.GetInstance().p_Controller.paintColor = c.color;

        if(SplatGun.GetInstance().mainVisualParticle.TryGetComponent<ParticleSystemRenderer>(out var p_Renderer)) 
        {
            Material p_Material = p_Renderer.sharedMaterial;

            if(p_Material.HasProperty("_BaseColor"))
            {
                p_Material.SetColor("_BaseColor", c.color);
            }
            else 
            {
                p_Material.color = SplatGun.GetInstance().p_Controller.paintColor;
            }
        }
    }
}

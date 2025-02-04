using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBehavior : MonoBehaviour
{
    public string ColorName;
    public Collectable c;
    public GameObject bg;
    public Button btn;
    public bool locked = true;
    public bool selected = false;

    // Static list to hold reference to all HUDBehaviour instances
    private static readonly List<HUDBehavior> instances = new();

    void Awake()
    {
        instances.Add(this);
    }    
    
    void OnDestroy()
    {
        instances.Remove(this);
    }

    void Update()
    {
        if(locked && GameManager.GetInstance().colorOrbs.Count > 0)
        {
            foreach (var color in GameManager.GetInstance().colorOrbs)
            {
                Collectable collectable = color.GetComponent<Collectable>();
                if(collectable.Name == ColorName)
                {
                    c = collectable;
                    // btn.gameObject.SetActive(true);
                    bg.SetActive(false);
                    locked = false;
                }
            }
        }

        if(selected) 
        {
            ColorBlock cb = btn.colors;
            Color n_Color = cb.normalColor;

            n_Color.a = 1f;
            cb.normalColor = n_Color;
            btn.colors = cb;
        }
        else 
        {
            ColorBlock cb = btn.colors;
            Color n_Color = cb.normalColor;

            n_Color.a = .25f;
            cb.normalColor = n_Color;
            btn.colors = cb;
        }
    }

    public void Selected() 
    {
        // Unselect all other instances
        foreach (var HUD in instances)
        {
            if(HUD != this) 
                HUD.selected = false;

        
            if(c != null && c.Name == ColorName) 
            {
                // Clear the previous color
                SplatGun.GetInstance().colors.Clear();
                
                // Add new color
                SplatGun.GetInstance().colors.Add(c.color);

                // Set the particle material color to the color of the collected color
                SplatGun.GetInstance().p_Controller.paintColor = c.color;
                selected = true;

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
    }
}

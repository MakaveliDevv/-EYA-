using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SplatGun : MonoBehaviour
{
    public static SplatGun i;
    public List<Color> colors = new List<Color>();
    public ParticlesController p_Controller;
    public GameObject mainVisualParticle;

    void Awake() 
    {
        if(i == null) 
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() 
    {
        if(mainVisualParticle.TryGetComponent<ParticleSystemRenderer>(out var p_Renderer)) 
        {
            Material p_Material = p_Renderer.sharedMaterial;
            if(p_Material.HasProperty("_BaseColor"))
            {
                p_Material.SetColor("_BaseColor", new Color(241f / 255f, 71f / 255f, 121f / 255f));
            }
        }
    }

    public static SplatGun GetInstance() 
    {
        return i;
    }
}
   // Need a reference to the HUD 

    // If color "collected" then unlock the color in the HUD

    // Each color has it's own place in the HUD

    // A way to keep in track with the assigned HUD for the color is to attach a string

    // Check if the string is the same as the unlocked color name

    // Each unlocked color goes in an inventory with it's assigned name

    // Lets test for now with a key input to unlock a color
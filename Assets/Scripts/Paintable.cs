using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Paintable : MonoBehaviour {
    const int TEXTURE_SIZE = 1024;
    public float coverageThreshold = 0.75f; // 99% coverage for "fully painted"
    public float coverage;
    public int checkFrequency = 10; // Check every 10th collision event
    

    // Properties
    public float minRadius = 0.5f;
    public float maxRadius = 2f;
    public float strength = 1;
    public float hardness = 1;
    public float tolerance = 0.01f;
    public bool restrictedColors;
    public bool onRedPath, onYellowPath;
    public bool tree, redGrass, lantern, yellowGrass, pillar;
    public bool painted;


    public float extendsIslandOffset = 1;
    public List<Color> allowedColors; // Add this line to store allowed colors

    RenderTexture extendIslandsRenderTexture;
    RenderTexture uvIslandsRenderTexture;
    RenderTexture maskRenderTexture;
    RenderTexture supportTexture;
    
    Renderer rend;
    int paintCounter = 0;

    int maskTextureID = Shader.PropertyToID("_MaskTexture");

    public RenderTexture getMask() => maskRenderTexture;
    public RenderTexture getUVIslands() => uvIslandsRenderTexture;
    public RenderTexture getExtend() => extendIslandsRenderTexture;
    public RenderTexture getSupport() => supportTexture;
    public Renderer getRenderer() => rend;

    public event System.Action<Paintable> OnFullyPainted;

    void Start() {
        maskRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        extendIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        uvIslandsRenderTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        supportTexture = new RenderTexture(TEXTURE_SIZE, TEXTURE_SIZE, 0)
        {
            filterMode = FilterMode.Bilinear
        };

        rend = GetComponent<Renderer>();
        rend.material.SetTexture(maskTextureID, extendIslandsRenderTexture);

        PaintManager.instance.InitTextures(this);
    }

    void OnDisable(){
        maskRenderTexture.Release();
        uvIslandsRenderTexture.Release();
        extendIslandsRenderTexture.Release();
        supportTexture.Release();
    }

    public bool IsFullyPainted()
    {
        paintCounter++;
        if (paintCounter % checkFrequency != 0)
        {
            // Skip this check based on the frequency
            return false;
        }

        // Create a temporary Texture2D to read the RenderTexture
        Texture2D tex = new(TEXTURE_SIZE, TEXTURE_SIZE, TextureFormat.RGBA32, false);
        RenderTexture.active = maskRenderTexture;
        tex.ReadPixels(new Rect(0, 0, TEXTURE_SIZE, TEXTURE_SIZE), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        // Sample every nth pixel to reduce processing load
        int sampleStep = 4; // Change this to sample more/less pixels
        int paintedPixelCount = 0;
        int totalSampledPixels = 0;

        for (int y = 0; y < TEXTURE_SIZE; y += sampleStep)
        {
            for (int x = 0; x < TEXTURE_SIZE; x += sampleStep)
            {
                Color pixel = tex.GetPixel(x, y);
                if (pixel.a > 0.1f)
                {
                    paintedPixelCount++;
                }
                totalSampledPixels++;
            }
        }

        // Clean up the temporary texture
        Destroy(tex);

        // Calculate the coverage based on the sampled pixels
        coverage = (float)paintedPixelCount / totalSampledPixels;
        if(coverage >= coverageThreshold && !painted) 
        {
            painted = true;
            Debug.Log("Object fully painted: " + gameObject.name);
            OnFullyPainted?.Invoke(this);
        }
        // return coverage >= coverageThreshold;
        return painted;
    }

    public bool ReturnCoverage() 
    {
        return coverage >= coverageThreshold;
    }

    void OnTriggerStay(Collider collider) 
    {
        if(redGrass) 
        {
            if(collider.CompareTag("Player")) 
            {
                onRedPath = true;
            } 
        }

        if(yellowGrass) 
        {
            if(collider.CompareTag("Player")) 
            {
                onYellowPath = true;
            }
        }
    }
}



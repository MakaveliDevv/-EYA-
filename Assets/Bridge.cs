using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Bridge : MonoBehaviour
{
    public GameObject col;

    void OnTriggerStay(Collider collider) 
    {
        if(collider.CompareTag("Player")) 
        {
            if(GameManager.GetInstance().colorOrbs.Count == 8) 
            {
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // This will stop the game in the Unity Editor.
                #endif
            }
        }
    }
}

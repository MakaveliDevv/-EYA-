using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    public bool menuIsOpen = false;
    public GameObject HUD_menu;
    private MovementInput _MovementInput;
    private Camera cam;
    private CinemachineBrain cin;

    // public HUDBehavior colorHUD;

    void Start() 
    {
        _MovementInput = GetComponent<MovementInput>();
        cam = Camera.main;
        cin = cam.GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            if(!menuIsOpen) 
            {
                HUD_menu.SetActive(true);
                menuIsOpen = true;

                // Lock player's movement
                ToggleMovement();
            }
            else
            {
                HUD_menu.SetActive(false);
                menuIsOpen = false;

                // Unlock player's movement
                ToggleMovement();
            }
        }
    }

    private void ToggleMovement() 
    {
        if(menuIsOpen) 
        {
            _MovementInput.locked = true;
            cin.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            _MovementInput.locked = false;
            cin.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    private bool menu = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) {
            OpenMenu();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            CloseMenu();
        }
    }

    public void OpenMenu() {
        // Check that menu is closed
        if (!menu) {
            Debug.Log("Opening Menu");
            menu = !menu;
        }
    }

    public void CloseMenu() {
        // Check if menu is already open
        if (menu) {
            Debug.Log("Closing Menu");
            menu = !menu;
        }
    }
}

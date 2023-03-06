using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

public class MenuControl : MonoBehaviour
{
    private numButtons = 20;
    
    public GameObject menu;
    public GridObjectCollection gc;
    public ScrollingObjectCollection so;
    private EMGReader emgReader;
    private string control = "";
    private double debounceTime;
    private float speed;
    

    void Start()
    {
        emgReader = FindObjectOfType<EMGReader>();
        emgReader.StartReadingData();
        menu.SetActive(false);
        debounceTime = Time.time;
    }

    void FixedUpdate()
    {
        control = emgReader.ReadControlFromArmband();
        speed = emgReader.ReadSpeedFromArmband();
        if (control == "0" && Time.time - debounceTime > 1f) {
            // Hand Close - Open/Close Menu
            if(menu.activeSelf) {
                CloseMenu();
                debounceTime = Time.time;
            } else {
                OpenMenu();
                debounceTime = Time.time;
            }
        } else if (control == "3") {
            // Extension - Up Scroll
            DownScroll(speed);
        } else if (control == "4") {
            // Flexion - Up Scroll
            UpScroll(speed);
        } else if(control == "1") {
            
        }
    }

    void DownScroll(float speed) {
        so.MoveByTiers(1);
        gc.UpdateCollection();
    }

    void UpScroll(float speed) {
        so.MoveByTiers(-1);
        gc.UpdateCollection();
    }

    void OpenMenu() {
        Debug.Log("Opening Menu");
        menu.SetActive(true);
    }

    void CloseMenu() {
        Debug.Log("Closing Menu");
        menu.SetActive(false);
    }
}

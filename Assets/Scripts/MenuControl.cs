using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine.UI;
using TMPro;

public class MenuControl : MonoBehaviour
{
    private int numButtons = 50;

    public GameObject button, grid, scroll;
    public TextMeshPro text;
    private GridObjectCollection gc;
    private ScrollingObjectCollection so;
    private EMGReader emgReader;
    private string control = "";
    private double debounceTime;
    private float speed;
    private int count = 0;
    private Vector3 ogMenuPos;
    

    void Awake()
    {
        gc = FindObjectOfType<GridObjectCollection>();
        so = FindObjectOfType<ScrollingObjectCollection>();

        // Add the number of buttons desired
        for(int i=0; i<numButtons; i++) {
            var nButton = Instantiate(button, grid.transform);
            nButton.SetActive(true);
            nButton.name = "Button_" + (i+1);
            nButton.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Button " + (i+1);
        }
    }

    void Start() {
        // scroll.SetActive(false);
        ogMenuPos = scroll.transform.position;
        emgReader = FindObjectOfType<EMGReader>();
        debounceTime = Time.time;
    }

    void UpdateMenu()
    {
        gc.UpdateCollection();
        so.UpdateContent();
    }

    void FixedUpdate()
    {
        // Vector3 camPos = Camera.main.gameObject.transform.position;
        // scroll.transform.position = camPos + ogMenuPos;
        UpdateMenu();
        control = emgReader.ReadControlFromArmband();
        speed = Mathf.Pow(emgReader.ReadSpeedFromArmband(), 3f);
        if (control != "") {
            text.text = control;
        }
        if (speed < 0) {
            speed = 0.01f;
        }
        if (control == "0" && Time.time - debounceTime > 1f) {
            // Hand Close - Open/Close Menu
            if(scroll.activeSelf) {
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
        so.MoveByTiers(Mathf.RoundToInt(5 * speed));
        gc.UpdateCollection();
    }

    void UpScroll(float speed) {
        so.MoveByTiers(Mathf.RoundToInt(-5 * speed));
        gc.UpdateCollection();
    }

    void OpenMenu() {
        Debug.Log("Opening Menu");
        scroll.SetActive(true);
    }

    void CloseMenu() {
        Debug.Log("Closing Menu");
        scroll.SetActive(false);
    }
}

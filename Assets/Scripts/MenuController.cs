using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject button1, button2, button3, button4, button5;
    private int activeButton = 2; // Default to middle button
    private List<GameObject> buttons = new List<GameObject>();
    private Color ogColor;
    private MyoReaderClient emgReader;
    private float debounce;

    void Start() {
        //Get initial color 
        Renderer r = (button1.GetComponentsInChildren<Renderer>())[0];
        if(ogColor == null) {
            ogColor = r.material.color;
        }
        // Add all buttons to list
        buttons.Add(button1);
        buttons.Add(button2);
        buttons.Add(button3);
        buttons.Add(button4);
        buttons.Add(button5);
        // EMG Reader 
        emgReader = FindObjectOfType<MyoReaderClient>();
        debounce = Time.time;
    }

    void Update() {
        buttons[0] = GameObject.Find("B1");
        buttons[1] = GameObject.Find("B2");
        buttons[2] = GameObject.Find("B3");
        buttons[3] = GameObject.Find("B4");
        buttons[4] = GameObject.Find("B5");

        if (Time.time - debounce > 0.5f) {
            Debug.Log(activeButton);
            if (emgReader.control == "2"){
                Debug.Log("Click");
                debounce = Time.time;
            } else if (emgReader.control == "1") { // Hand Open - Up
                if (activeButton == 2 || activeButton == 4) {
                    activeButton -= 2;
                }
                debounce = Time.time;
            } else if (emgReader.control == "0") { // Hand Close - Down
                if (activeButton == 0 || activeButton == 2) {
                    activeButton += 2;
                }
                debounce = Time.time;
            } else if (emgReader.control == "5") { // Flexion - right
                if (activeButton == 2 || activeButton == 3) {
                    activeButton -= 1;
                }
                debounce = Time.time;
            } else if (emgReader.control == "4") { // Extension - Left
                if (activeButton == 1 || activeButton == 2) {
                    activeButton += 1;
                }
                debounce = Time.time;
            }
        }
        UpdateSelectedButton();
    }

    private void UpdateSelectedButton() {
        for(int i=0; i<5; i++) {
            if(i == activeButton) {
                ChangeButtonColor(buttons[i], Color.red);
            } else {
                ChangeButtonColor(buttons[i], ogColor);
            }
        }
    }

    private void ChangeButtonColor(GameObject button, Color color) {
        var renderer = button.GetComponentsInChildren<MeshRenderer>()[0];
        renderer.material.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInspector : MonoBehaviour
{
    public Toggle myToggle;
    public Slider steeringSlider;
    public Slider speedSlider;
    public ObjectFollow cube;
    public TextMeshProUGUI speedText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedText.SetText("Speed : "+ cube.speed);
    }

    public void ButtonClick()
    {
        if(myToggle.isOn)
        {
            cube.isLooping = true;
        }
        else
        {
            cube.isLooping = false;
        }
    }

    public void SteeringSlider()
    {
        cube.steeringInertia = steeringSlider.value;
    }

    public void SpeedSlider()
    {
        cube.speed = speedSlider.value;
    }
}
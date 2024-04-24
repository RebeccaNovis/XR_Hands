using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;


public class PlayTheChuck : MonoBehaviour
{
    public HandVisualizer handVisualizer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ChuckSubInstance>().RunCode(@" 
            440 => global float InFreq;
            SinOsc s => dac;
            while( true )
            { 
                0.2 => s.gain;
                InFreq => s.freq;
                10::ms => now;
            }
        ");

    }

    // Update is called once per frame
    void Update()
    {
        float position = handVisualizer.xAxisPosition;

        GetComponent<ChuckSubInstance>().SetFloat("InFreq", position * 1000);
    }
}

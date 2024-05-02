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
            0.2 => global float InGain;
            0 => global int InBar;
            ModalBar s => dac;
            
            while( true )
            { 
                InBar => s.preset;
                1 => s.noteOn;
                InGain => s.gain;
                forceMajor() => s.freq;
                100::ms => now;
                1 => s.noteOff;
                0 => s.gain;
                100::ms => now;
            }
            fun int forceMajor()
            {
                Std.ftoi(InFreq) => int myFreq;
                [0,0,2,2,4,5,5,7,7,9,9,11] @=> int major[];

                myFreq % 12 => int pitchClass;
                myFreq / 12 => int octave;
                major[pitchClass] => int majorPitchClass;
                majorPitchClass + (octave * 12) => int majorNote;

                return majorNote;
            }
        ");

    }

    // Update is called once per frame
    void Update()
    {
        float rightXPosition = handVisualizer.xAxisPosition;
        float leftYPosition = handVisualizer.leftWristYAxisPos;
        float indexYPosition = handVisualizer.leftIndexYPos;

        float myGain = Mathf.Clamp((leftYPosition - .4f), 0f, 1f);

        int bar = Mathf.RoundToInt(((indexYPosition - 1) * 10));
        int myBar = Mathf.Clamp(bar, 0, 6);

        GetComponent<ChuckSubInstance>().SetFloat("InFreq", Mathf.Clamp(rightXPosition * 1000, 50f, 500f));
        
        GetComponent<ChuckSubInstance>().SetFloat("InGain", myGain);

        //GetComponent<ChuckSubInstance>().SetFloat("InBar", myBar);

        Debug.Log("Freq: " + Mathf.Clamp(rightXPosition * 1000, 50f, 500f));
        Debug.Log("Gain: " + myGain);
        Debug.Log("my bar: " + myBar + "bar: " + bar);
    }


}

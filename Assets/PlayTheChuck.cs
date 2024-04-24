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
            ModalBar s => dac;
            1 => s.preset;
            while( true )
            { 
                1 => s.noteOn;
                InGain => s.gain;
                forceMajor() => s.freq;
                10::ms => now;
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

        GetComponent<ChuckSubInstance>().SetFloat("InFreq", rightXPosition * 1000);
        GetComponent<ChuckSubInstance>().SetFloat("InGain", leftYPosition);
    }
}

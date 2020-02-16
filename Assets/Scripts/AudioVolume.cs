using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioVolume : MonoBehaviour
{
    public Image output;
    [Range(0.0f,1.0f)]
    public float Cutoff;
    
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        clipSampleData = new float[sampleDataLength];
    }

    void Update()
    {
        currentUpdateTime += Time.deltaTime;

        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
            //Debug.Log(clipLoudness);
            if (clipLoudness > Cutoff)
            {
                output.color = Color.white;
            }
            else
            {
                output.color = Color.clear;
            }
            
        }

    }
}

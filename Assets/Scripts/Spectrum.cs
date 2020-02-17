using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spectrum : MonoBehaviour
{
    public float[] Frequencies;
    public float[] FrequencyTolerance;
    [Range(0.0f, 1.0f)]
    public float[] Cutoffs;
    public int[] ParticleAmounts;
    public ParticleSystem[] Systems;


    public int NumSamples = 1024;

    private float MaxFrequency;

    private float[] FreqData;

    public void Start()
    {
        MaxFrequency = AudioSettings.outputSampleRate / 2;
    }

    void Update()
    {
        FreqData = new float[NumSamples];

        for (int i = 0; i < Frequencies.Length; i++)
        {
            float LowFrequency = Frequencies[i] - FrequencyTolerance[i];
            float HighFrequency = Frequencies[i] + FrequencyTolerance[i];

            AudioListener.GetSpectrumData(FreqData, 0, FFTWindow.BlackmanHarris);
            int n1 = (int)Mathf.Floor(LowFrequency * NumSamples / MaxFrequency);
            int n2 = (int)Mathf.Floor(HighFrequency * NumSamples / MaxFrequency);
            float sum = 0;

            for (var j = n1; j <= n2; j++)
            {
                sum += FreqData[j];
            }

            if (sum > Cutoffs[i])
            {
                Systems[i].Emit(ParticleAmounts[i]);
            }
        }

    }
}

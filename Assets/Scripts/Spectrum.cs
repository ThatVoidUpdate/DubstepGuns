using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Spectrum : MonoBehaviour
{

    public ParticleSystem[] particleSystems;
    [Range(0.0f,1.0f)]
    public float[] Cutoffs;
    public float[] Scalers;
    void Update()
    {
        float[] spectrum = new float[2048];
        float[] CompressedSpectrum = new float[32];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            //Debug.DrawLine(new Vector3(i - 1, spectrum[i - 1] *100, 0), new Vector3(i, spectrum[i] * 100, 0), Color.red);
            //Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            //Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }



        for (int i = 1; i < CompressedSpectrum.Length; i++)
        {
            for (int j = i * spectrum.Length / CompressedSpectrum.Length; j < (i + 1) * spectrum.Length / CompressedSpectrum.Length; j++)
            {
                CompressedSpectrum[i] += spectrum[j];
            }
            //system.Emit((int)(CompressedSpectrum[i] * 10));
            Debug.DrawLine(new Vector3((i - 1) * (spectrum.Length / CompressedSpectrum.Length), CompressedSpectrum[i] * 100, 0), new Vector3(i * (spectrum.Length / CompressedSpectrum.Length), CompressedSpectrum[i] * 100, 0), Color.yellow);
            if (particleSystems[i] != null && CompressedSpectrum[i] > Cutoffs[i])
            {
                particleSystems[i].Emit((int)(CompressedSpectrum[i] * 100 * Scalers[i]));
            }
            
        }
    }
}

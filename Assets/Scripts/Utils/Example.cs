using UnityEngine;
using System;

public class Example: MonoBehaviour, AudioProcessor.AudioCallbacks {
    public AudioProcessor processor;

    void Start() {//ou CoroutineStart
        processor.addAudioCallback(this);
    }
    
    public void onOnbeatDetected() {
        Debug.Log("Beat!!!");
    }
    
    public void onSpectrum(float[] spectrum) {
        for (int i = 0; i < spectrum.Length; ++i) {
            Vector3 start   = new Vector3(i, 0, 0);
            Vector3 end     = new Vector3(i, spectrum[i], 0);
            Debug.DrawLine(start, end);
        }
    }
}
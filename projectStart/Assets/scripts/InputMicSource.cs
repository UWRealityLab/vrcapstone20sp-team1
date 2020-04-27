using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class InputMicSource : MonoBehaviour
{
    private AudioSource audioSource;
    private int minFreq;
    private int maxFreq;
    private bool micConnected = false;
    private string deviceName;      // For debug

    // Display the mic connection status
    public Text micConnectedStatus;


    // Start is called before the first frame update
    void Start()
    {
        if (Microphone.devices.Length <= 0)
        {
            Debug.LogWarning("Microphone not connected!");
        }
        else
        {
            deviceName = Microphone.devices[0];
            micConnected = true;
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            if (minFreq == 0 && maxFreq == 0)
            {
                maxFreq = 44100;
            }

            audioSource = this.GetComponent<AudioSource>();
            audioSource.clip = Microphone.Start(null, true, 20, maxFreq);
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (micConnected)
        {
            micConnectedStatus.text = "Mic Detected";
        }
        else
        {
            micConnectedStatus.text = "No Mic Detected";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
public class InputMicSource : MonoBehaviour
{
    public AudioSource audioSource;
    public Dropdown dropdown;
    List<string> micList = new List<string>();
    int previousMicCount = 0;
    private int minFreq = 0;
    private int maxFreq = 44100;
    private bool micConnected = false;
    private string activeMic;      // For debug

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
            GetMicList();
            micConnected = true;
            audioSource = this.GetComponent<AudioSource>();
            SelectMic(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartSource();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            StopSource();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            dropdown.gameObject.SetActive(!dropdown.gameObject.active);
        }
        if (Microphone.devices.Length != previousMicCount)
        {
            GetMicList();
        }
        if (micConnected)
        {
            micConnectedStatus.text = "Mic Detected: " + activeMic;
        }
        else
        {
            micConnectedStatus.text = "No Mic Detected";
        }
    }
    void GetMicList()
    {
        dropdown.ClearOptions();
        micList.Clear();
        foreach (string mic in Microphone.devices)
        {
            micList.Add(mic);
            Debug.Log("Mic: " + mic);
        }
        dropdown.AddOptions(micList);
        previousMicCount = Microphone.devices.Length;
    }

    public void SelectMic(int micNumber)
    {
        Debug.Log("New Mic Selected" + dropdown.value);
        activeMic = micList[dropdown.value];
    }

    void StartSource()
    {
        Debug.Log(maxFreq);
        Microphone.GetDeviceCaps(activeMic, out minFreq, out maxFreq);
        audioSource.clip = Microphone.Start(activeMic, true, 20, maxFreq);
        while(!(Microphone.GetPosition(activeMic) > 0)){ }
        audioSource.Play();
    }
    void StopSource()
    {
        audioSource.clip = null;
        Microphone.End(activeMic);
        audioSource.Stop();
    }
}

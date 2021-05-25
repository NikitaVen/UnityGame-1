using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; 


    void SetVolume (float volume)
    {

        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

    }

}

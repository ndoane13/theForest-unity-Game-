using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer am;
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        am.SetFloat("Volume", volume);
    }
}

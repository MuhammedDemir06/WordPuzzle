using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Manager")]
    [SerializeField] private AudioSource[] sounds;
    private bool active;

    public void SoundsActive(bool isActive)
    {
        if(isActive)
        {
            active = true;
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].enabled = true;
            }
        }
        else
        {
            active = false;
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].enabled = false;
            }
        }
    }
    //Buttons
    public void ClickSoundButton(AudioSource sound)
    {
        if (sound != null && active)
            sound.Play();
    }
}

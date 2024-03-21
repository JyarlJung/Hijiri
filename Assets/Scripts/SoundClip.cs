using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SoundClip
{
    public string soundName = string.Empty;
    public float endPoint=0f;
    public float startPoint = 0f;
    public AudioClip audioClip = null;
    public AudioClip GetClip()
    {
        return audioClip;
    }
}
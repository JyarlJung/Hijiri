using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : Singleton<SoundManager>
{
    [HideInInspector]
    public bool isBgmOn=false;
    public float bgmVolume;
    public float seVolume;
    public List<SoundClip> bgmPool = new List<SoundClip>();
    public List<SoundClip> sePool = new List<SoundClip>();
    private AudioSource bgmListener;
    private AudioSource seListener;
    private float bgmEndPoint;
    private float bgmStartPoint;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        bgmListener = gameObject.AddComponent<AudioSource>();
        bgmListener.playOnAwake = false;
        seListener = gameObject.AddComponent<AudioSource>();
        bgmListener.playOnAwake = false;
    }
    public float GetBgmTime()
    {
        if (bgmListener)
        {
            if (bgmListener.isPlaying)
            {
                return bgmListener.time;
            }
        }
            return -1000f;
    }
    public float GetBgmSpeed()
    {
        if (bgmListener)
        {
            if (bgmListener.isPlaying)
            {
                return bgmListener.pitch;
            }
        }
        return 1f;
    }
    private void Update()
    {
        if(isBgmOn && bgmListener.time >= bgmEndPoint)
        {
            bgmListener.time = bgmStartPoint + (bgmListener.time - bgmEndPoint);
        }
    }
    public bool PlayBgm(string itemName,float speed=1f)
    {
        SoundClip clip = GetPoolItem(itemName,bgmPool);
        if (clip == null)
            return false;
        bgmListener.clip = clip.GetClip();
        bgmListener.volume=bgmVolume;
        bgmListener.Play();
        bgmListener.pitch = speed;
        bgmStartPoint = clip.startPoint;
        bgmEndPoint = clip.endPoint;
        isBgmOn = true;
        return true;
    }
    public void ChangeBgm(string name=null, float time=0.1f)
    {
        if (name!=null)
        {
            SoundClip clip = GetPoolItem(name, bgmPool);
            if (clip == null)
                return;
        }
        StartCoroutine(PadeBgm(name,time));
    }
    private IEnumerator PadeBgm(string name = null, float time = 0.1f)
    {
        while (bgmListener.volume > 0)
        {
            bgmListener.volume -= (bgmVolume * 0.01f);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(time);
        PlayBgm(name);
    }
    public bool PlaySe(string itemName, bool overrap=true)
    {
        SoundClip clip = GetPoolItem(itemName,sePool);
        if (clip == null)
            return false;

        if (overrap)
        {
            seListener.PlayOneShot(clip.GetClip(), seVolume);
        }
        else
        {
            seListener.clip = clip.GetClip();
            seListener.Play();
        }
        return true;
    }
    SoundClip GetPoolItem(string itemName, List<SoundClip> list)
    {
        for (int ix = 0; ix < list.Count; ++ix)
        {
            if (list[ix].soundName.Equals(itemName))
                return list[ix];
        }

        Debug.LogWarning("There's no matched pool list.");
        return null;
    }
}
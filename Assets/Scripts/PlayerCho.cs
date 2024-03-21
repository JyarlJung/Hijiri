using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCho : MonoBehaviour
{
    public Animator head;
    public Animator hand;
    public GameObject point;
    private float originbeat;
    public float bgmbeat;
    public float bpm;
    private float count=0;
    public GameObject bar;
    public int key;
    // Start is called before the first frame update
    void Start()
    {
        originbeat = bgmbeat;
    }

    // Update is called once per frame
    void Update()
    {
        if(SoundManager.Instance.GetBgmTime()>bgmbeat + (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()))
        {
            head.Play("shake", -1, 0f);
            count++;
            bgmbeat = originbeat+(60f/bpm)*count;
        }
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                if (Input.anyKeyDown)
                {
                    if (Global.Instance.stage.start == 0)
                    {
                        bar.SetActive(true);
                        SoundManager.Instance.PlaySe("mok");
                        Global.Instance.stage.start = 1;
                        Global.Instance.stage.startTime = Global.Instance.time + 1.5f;
                        GameObject.FindGameObjectWithTag("title").SetActive(false);
                        Global.Instance.stage.text.gameObject.SetActive(true);
                        key = (int)kcode;
                    }
                    else if (key !=(int)kcode)
                    {
                        Global.Instance.ester = false;
                    }
                    hand.Play("shake", -1, 0f);
                    GameObject hiteff = ObjectPool.Instance.PopFromPool("Hit", null, false);
                    hiteff.GetComponent<Reticle>().color = Global.Instance.stage.HitFunc();
                    hiteff.transform.position = point.transform.position;
                    hiteff.SetActive(true);
                    Global.Instance.CameraShake(0.03f, 0f);
                }
            }
        }
    }
    
}

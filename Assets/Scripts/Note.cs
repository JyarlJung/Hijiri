using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public string poolItemName;
    public float point;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if (SoundManager.Instance)
        {
            transform.position = new Vector3((point+ (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()) - SoundManager.Instance.GetBgmTime())
                *Global.Instance.speed, 0.05f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((point + (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()) - SoundManager.Instance.GetBgmTime())
            * Global.Instance.speed, 0.05f);
        if (point + (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()) - SoundManager.Instance.GetBgmTime() < 0)
        {
            GetComponent<Reticle>().color.a -= 0.06f;
            GetComponent<Reticle>().SetColor();
        }
        if (point + (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()) - SoundManager.Instance.GetBgmTime() < -0.15f)
        {
            Global.Instance.stage.HitFunc(true);
            PushNote();
        }
    }
    public void PushNote()
    {
        GetComponent<Reticle>().color.a = 0.7f;
        ObjectPool.Instance.PushToPool(poolItemName, gameObject);
    }
}

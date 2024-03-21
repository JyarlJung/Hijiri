using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManage : MonoBehaviour
{
    public List<float> note = new List<float>();
    public List<bool> goldNote = new List<bool>();
    private List<GameObject> noteObList = new List<GameObject>();
    public int count=0;
    public float bgmsp;
    [TextArea]
    public string akbo;
    public int combo;
    public int score;
    public TextMesh text;
    private float originbeat;
    private float bpm;
    public int start = 0;
    public float startTime=0f;
    public Image fade;

    // Start is called before the first frame update
    void Start()
    {
        originbeat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCho>().bgmbeat+0.025f;
        bpm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCho>().bpm;
        float umpyo32 = (60f / bpm)*0.25f;
        int count=0;
        int totalCount = note.Count;
        foreach (float f in note)
        {
            goldNote.Add(false);
        }

        foreach (char c in akbo)
        {
            if((float)c>=49 &&(float)c<=58)
            {
                goldNote.Add(false);
                note.Add(originbeat+(count * umpyo32));
                count += c - 48;
            }
            if(c==33)
            {
                goldNote.RemoveAt(goldNote.Count-1);
                goldNote.Add(true);
            }
        }
    }
    private IEnumerator FadeOut()
    {
        while (fade.color.a < 1)
        {
            Color color = fade.color;
            color.a += 0.02f;
            fade.color=color;
            yield return new WaitForSeconds(0.02f);
        }
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update()
    {
        text.text = combo.ToString() + "\nCombo!";
        if (start == 2 && SoundManager.Instance.GetBgmTime()== -1000f)
        {
            start = 3;
            if(score>225000)
            {
                Global.Instance.rank = 3;
            }
            else if (score > 174000)
            {
                Global.Instance.rank = 2;
            }
            else if (score > 0)
            {
                Global.Instance.rank = 1;
            }
            else
            {
                Global.Instance.rank = 0;
            }
                StartCoroutine(FadeOut());
        }
        if (Global.Instance.time>startTime && start==1)
        {
            start = 2;
            SoundManager.Instance.PlayBgm("wak", bgmsp);
            note.Add(400f);
        }
        if (note[count]<SoundManager.Instance.GetBgmTime()+2.5f- Global.Instance.delay)
        {
            GameObject noteOb = ObjectPool.Instance.PopFromPool("Note",null,false);
            noteOb.GetComponent<Note>().point = note[count];
            noteOb.SetActive(true);
            if (goldNote[count])
            {
                noteOb.GetComponent<Reticle>().color = Color.yellow;
                noteOb.GetComponent<Reticle>().color.a = 0.6f;
                noteOb.GetComponent<Reticle>().SetColor();
            }
            else
            {
                noteOb.GetComponent<Reticle>().color = Color.white;
                noteOb.GetComponent<Reticle>().color.a = 0.6f;
                noteOb.GetComponent<Reticle>().SetColor();
            }
            noteObList.Add(noteOb);
            count++;
        }
        if(count>=note.Count)
        {
            count = 0;
        }
    }
    public Color HitFunc(bool flag=false)
    {
        if(flag)
        {
            combo = 0;
            noteObList.RemoveAt(0);
            return Color.white;
        }
        if(noteObList.Count>0)
        {
            Note noteOb = noteObList[0].GetComponent<Note>();
            float panjung = Mathf.Abs(noteOb.point + (Global.Instance.delay * SoundManager.Instance.GetBgmSpeed()) - SoundManager.Instance.GetBgmTime());
            if (panjung < 0.15f)
            {
                noteOb.PushNote();
                noteObList.RemoveAt(0);
                if (panjung < 0.057f)
                {
                    combo++;
                    score += 500 + combo;
                    return Color.green;
                }
                else if (panjung < 0.11f)
                {
                    combo++;
                    score += 200 + combo;
                    return Color.yellow;
                }
                else
                {
                    combo = 0;
                    score += 50;
                    return Color.red;
                }
            }
        }
        return Color.white;
    }
}

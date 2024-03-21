using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultStage : MonoBehaviour
{
    public TextMesh tex;
    public int i=0;
    public int ch=0;
    public List<string> ranli = new List<string>();
    public List<string> chli = new List<string>();
    public List<string> dali = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Go1());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && i==1)
        {
            Destroy(Global.Instance.gameObject);
            Destroy(SoundManager.Instance.gameObject);
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator Go1()
    {
        ch = Random.Range(0, 2);
        yield return new WaitForSeconds(1f);
        tex.text = chli[ch]+"의 평가";
        yield return new WaitForSeconds(1.6f);
        tex.text += "\n\n"+ dali[(ch*4)+(Global.Instance.rank)];
        yield return new WaitForSeconds(1.6f);
        tex.text += "\n\n\n 랭크 : " + ranli[Global.Instance.rank];
        SoundManager.Instance.PlaySe("mok");
        if(Global.Instance.ester && Global.Instance.rank>=2)
        {
            tex.text += "\n이스터에그 달성 : 한 손가락으로 충분";
        }
        yield return new WaitForSeconds(3f);
        tex.text += "\n\n다시하려면 R키를 누르세여";
        i++;
    }
}

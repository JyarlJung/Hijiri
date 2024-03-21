using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : Singleton<Global>
{
    public float speed = 1f;
    public new Camera camera;
    public float time = 0f;
    public int frameTime = 0;
    private float scaledTime = 0;
    private Coroutine coroutine;
    public float delay = 0f;
    public Vector2 resolution;
    public StageManage stage;
    public int rank;
    public bool ester=true;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        Screen.SetResolution((int)resolution.x, (int)resolution.y, false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Return))
        {
            if (!Screen.fullScreen)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.MaximizedWindow);
            }
            else
            {
                Screen.SetResolution((int)resolution.x, (int)resolution.y, false);
            }
        }
        camera = Camera.main;
        time += Time.deltaTime;
        scaledTime += Time.timeScale;
        frameTime=Mathf.FloorToInt(scaledTime);
    }
    public Vector3 MousePosition()
    {
        Vector3 temp= camera.ScreenToWorldPoint(Input.mousePosition);
        temp.z = 0;
        return temp;
    }
    public void CameraShake(float x, float y)
    {
        MovingCamera temp = camera.GetComponent<MovingCamera>();
        if(temp)
        {
            temp.Shake(x, y);
        }
    }
    public void CameraShake(Vector3 degree)
    {
        MovingCamera temp = camera.GetComponent<MovingCamera>();
        if (temp)
        {
            temp.Shake(degree.x,degree.y);
        }
    }

    public void TimeStop(float time,float timeScale=0f)
    {
        if(coroutine!=null)StopCoroutine(coroutine);
        coroutine = StartCoroutine(TimeStopCoroutine(time,timeScale));
    }
    IEnumerator TimeStopCoroutine(float time, float timeScale)
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }
}

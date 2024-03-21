using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public GameObject followObject;
    public Vector3 screenMove=Vector3.zero;
    public Vector3 moveAngle;
    public float moveDistance;
    private Vector3 shake = new Vector3(0f, 0f);
    private Vector3 truePosition = new Vector3(0f, 0f);
    private Vector3 moveVec;
    private bool shakebool;
    // Start is called before the first frame update
    void Start()
    {
        truePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        shakebool = !shakebool;
        moveVec = Vector3.zero;
        if (followObject)
        {
            moveVec = (followObject.transform.position-truePosition+
                screenMove+ MathMng.AngleMove(Vector3.zero, moveAngle, moveDistance));

            moveVec.z = 0f;
        }

        if (MathMng.RepeatTimer(Global.Instance.time,0.03f))
        {
            if (Mathf.Abs(shake.x) > 0.007f)
            {
                shake.x *= -1f;
                shake.x -= MathMng.GetNegPos(shake.x) * 0.01f;
            }
            else
            {
                shake.x = 0;
            }
            if (Mathf.Abs(shake.y) > 0.007f)
            {
                shake.y *= -1f;
                shake.y -= MathMng.GetNegPos(shake.y) * 0.01f;
            }
            else
            {
                shake.y = 0;
            }
        }

        truePosition += moveVec * Time.deltaTime * 5f ;
        Vector3 snapPosition = shake + truePosition;
        snapPosition.x = Mathf.Round(snapPosition.x*100) *0.01f;
        snapPosition.y = Mathf.Round(snapPosition.y * 100) * 0.01f;
        transform.position = snapPosition;
    }
    public void Shake(float x, float y)
    {
        shake.x = x;
        shake.y = y;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class Reticle : MonoBehaviour
{
    public string poolItemName;
    private LineRenderer lineBg;
    private Animator animator;
    public Color color;
    public float radius;
    void Awake()
    {
        animator = GetComponent<Animator>();
        lineBg = GetComponent<LineRenderer>();
        lineBg.positionCount = 25;
        lineBg.startColor = color;
        lineBg.endColor = color;
    }
    private void OnEnable()
    {
        if (animator)
        {
            animator.Play("idle", -1, 0f);
            color.a = 0.6f;
        }
        lineBg.startColor = color;
        lineBg.endColor = color;
    }
    public void SetColor()
    {
        lineBg.startColor = color;
        lineBg.endColor = color;
    }
    void Update()
    {
        for (int i = 0; i < lineBg.positionCount; i++)
        {
            lineBg.SetPosition(i, MathMng.AngleMove(Vector3.zero, new Vector3(0, 0, (i / 25f) * 360f), radius));
        }
        if (animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                ObjectPool.Instance.PushToPool(poolItemName, gameObject);
            }
        }
    }
}

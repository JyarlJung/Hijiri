using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathMng
{
    public static bool RepeatTimer(float time, float timer)
    {
        if (time <= Time.deltaTime)
        {
            return false;
        }
        if (time % timer < Time.deltaTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool Timer(float time, float timer)
    {
        if (time - timer >= -Time.deltaTime && time - timer < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool Timer(float time, float timer,float deltaTime)
    {
        if (timer-time >= 0 && timer-time < deltaTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Vector3 RotTovec(Vector3 rot)
    {
        Vector3 ret = new Vector3(Mathf.Cos(rot.z * Mathf.Deg2Rad),
                Mathf.Sin(rot.z * Mathf.Deg2Rad));

        return ret;
    }
    public static Vector3 QuatTovec(Quaternion qua)
    {
        Vector3 ret = new Vector3(Mathf.Cos(qua.z * Mathf.Deg2Rad),
                Mathf.Sin(qua.z * Mathf.Deg2Rad));

        return ret;
    }

    public static Vector3 Rotate(Vector3 rot,float deg)
    {
        Vector3 ret = new Vector3(rot.x,rot.y,rot.z+deg);

        return ret;
    }

    public static Vector3 PositionMove(Vector3 pos,float x, float y)
    {
        Vector3 ret = new Vector3(pos.x + x, pos.y + y);
        return ret;
    }

    public static Vector3 PositionMove(Vector3 pos, Vector3 rot, float sp)
    {
        Vector3 ret = new Vector3(pos.x + (rot.x *sp), pos.y + (rot.y *sp));
        return ret;
    }
    public static Vector3 AngleMove(Vector3 pos, Vector3 rot, float sp)
    {
        Vector3 rota = RotTovec(rot);
        Vector3 ret = new Vector3(pos.x + (rota.x * sp), pos.y + (rota.y * sp));
        return ret;
    }
    public static Vector3 ToParticle(Vector3 rot)
    {
        Vector3 ret = new Vector3(rot.z+180f,-90f,90f);
        return ret;
    }
    public static float LookAt(Vector3 from, Vector3 to)
    {
        return Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;
    }
    public static Vector3 LookAtVec(Vector3 from, Vector3 to)
    {
        return new Vector3(0f,0f,Mathf.Atan2(to.y - from.y, to.x - from.x) * Mathf.Rad2Deg);
    }
    public static float Distance(Vector3 cho, Vector3 ene)
    {
        return Mathf.Sqrt(Mathf.Pow((cho.x - ene.x), 2f) + Mathf.Pow((cho.y - ene.y), 2f));
    }
    public static float GetNegPos(float num)
    {
        if(num>0)
        {
            return 1f;
        }
        else if(num<0)
        {
            return -1f;
        }
        else
        {
            return 0;
        }
    }
    public static float MaxX(BoxCollider2D col)
    {
        return col.transform.position.x + (col.offset.x * col.transform.lossyScale.x) + (col.size.x / 2 * Mathf.Abs(col.transform.lossyScale.x));
    }
    public static float MinX(BoxCollider2D col)
    {
        return col.transform.position.x + (col.offset.x * col.transform.lossyScale.x) - (col.size.x / 2 * Mathf.Abs(col.transform.lossyScale.x));
    }

    public static float MaxY(BoxCollider2D col)
    {
        return col.transform.position.y + (col.offset.y * col.transform.lossyScale.y) + (col.size.y / 2 * Mathf.Abs(col.transform.lossyScale.y));
    }
    public static float MinY(BoxCollider2D col)
    {
        return col.transform.position.y + (col.offset.y * col.transform.localScale.y) - (col.size.y / 2 * Mathf.Abs(col.transform.localScale.y));
    }
    public static float WidthSide(BoxCollider2D col)
    {
        return (col.size.x/ 2) * Mathf.Abs(col.transform.lossyScale.x);
    }
    public static float HeightSide(BoxCollider2D col)
    {
        return (col.size.y / 2) * Mathf.Abs(col.transform.lossyScale.y);
    }
}

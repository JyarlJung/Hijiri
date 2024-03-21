using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFont : MonoBehaviour
{
    public string sortingName="Default";
    public int sortingOrder=0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().font.material.mainTexture.filterMode=FilterMode.Point;
        GetComponent<MeshRenderer>().sortingLayerName = sortingName;
        GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
}

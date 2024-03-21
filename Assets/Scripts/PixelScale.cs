using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelScale : MonoBehaviour {
    [Range(0.01f,2f)]
    public float scale = 1f;
	private int renderWidth;
	private int renderHeight;


	void Update() {
        if (Global.Instance)
        {
            renderHeight = (int)(Global.Instance.resolution.y * scale);
            renderWidth = (int)(Global.Instance.resolution.x * scale);
        }
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture destination) {
		RenderTexture buffer = RenderTexture.GetTemporary(renderWidth, renderHeight, -1);
		
		buffer.filterMode = FilterMode.Point;
		source.filterMode = FilterMode.Point;
		
		Graphics.Blit(source, buffer);
		Graphics.Blit(buffer, destination);
		
		RenderTexture.ReleaseTemporary(buffer);
	}
}
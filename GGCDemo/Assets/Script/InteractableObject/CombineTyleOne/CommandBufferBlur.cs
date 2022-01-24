using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class CommandBufferBlur : MonoBehaviour
{
    [Tooltip("模糊程度")]
    public float BufferSize = 0.5f;
    public Shader m_BlurShader;
    private Material m_Material;

    private Camera m_Cam;

    private Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();

    // Remove command buffers from all cameras we added into
    private void Cleanup()
    {
        foreach (var cam in m_Cameras)
        {
            if (cam.Key)
            {
                cam.Key.RemoveCommandBuffer(CameraEvent.AfterForwardOpaque, cam.Value);
            }
        }
        m_Cameras.Clear();
        Object.DestroyImmediate(m_Material);
    }

    public void OnEnable()
    {
        Cleanup();
        SetCommandBuffer();
    }

    public void OnDisable()
    {
        Cleanup();
    }

    // Whenever any camera will render us, add a command buffer to do the work on it
    public void SetCommandBuffer()
    {
        var act = gameObject.activeInHierarchy && enabled;
        if (!act)
        {
            Cleanup();
            return;
        }

        var cam = Camera.main;
        if (!cam)
            return;

        CommandBuffer buf = null;
        // Did we already add the command buffer on this camera? Nothing to do then.
        if (m_Cameras.ContainsKey(cam))
            return;

        if (!m_Material)
        {
            m_Material = new Material(m_BlurShader);
            m_Material.hideFlags = HideFlags.HideAndDontSave;
        }

        buf = new CommandBuffer();
        buf.name = "Grab screen and blur";
        m_Cameras[cam] = buf;

        // copy screen into temporary RT
        int screenCopyID = Shader.PropertyToID("_ScreenCopyTexture");
        buf.GetTemporaryRT(screenCopyID, -1, -1, 0, FilterMode.Bilinear);
        buf.Blit(BuiltinRenderTextureType.CurrentActive, screenCopyID);

        // get two smaller RTs
        int blurredID = Shader.PropertyToID("_Temp1");
        int blurredID2 = Shader.PropertyToID("_Temp2");
        buf.GetTemporaryRT(blurredID, -2, -2, 0, FilterMode.Bilinear);
        buf.GetTemporaryRT(blurredID2, -2, -2, 0, FilterMode.Bilinear);

        // downsample screen copy into smaller RT, release screen RT
        buf.Blit(screenCopyID, blurredID);
        buf.ReleaseTemporaryRT(screenCopyID);

        // horizontal blur
        buf.SetGlobalVector("offsets", new Vector4(2.0f * BufferSize / Screen.width, 0, 0, 0));
        buf.Blit(blurredID, blurredID2, m_Material);
        // vertical blur
        buf.SetGlobalVector("offsets", new Vector4(0, 2.0f * BufferSize / Screen.height, 0, 0));
        buf.Blit(blurredID2, blurredID, m_Material);
        // horizontal blur
        buf.SetGlobalVector("offsets", new Vector4(4.0f * BufferSize / Screen.width, 0, 0, 0));
        buf.Blit(blurredID, blurredID2, m_Material);
        // vertical blur
        buf.SetGlobalVector("offsets", new Vector4(0, 4.0f * BufferSize / Screen.height, 0, 0));
        buf.Blit(blurredID2, blurredID, m_Material);

        buf.Blit(blurredID, BuiltinRenderTextureType.CameraTarget);

        cam.AddCommandBuffer(CameraEvent.AfterForwardOpaque, buf);
    }
}
using UnityEngine;
using System.IO;

public class SaveTexture : MonoBehaviour
{
    public RenderTexture renderTexture;

    private bool isTextureReady = false;
    private bool didRender = false;

    private void Update()
    {
        if (!didRender && isTextureReady)
        {
            // Check if the texture is ready before saving
            if (isTextureReady)
            {
                // Convert RenderTexture to Texture2D
                Texture2D texture = RenderTextureToTexture(renderTexture);

                if (texture != null)
                {
                    // Save the Texture2D as a PNG file in the persistent data path
                    string filePath = Path.Combine(Application.persistentDataPath, "SavedTexture.png");
                    SaveTextureToFile(texture, filePath);

                    // Reset the flag to avoid repetitive saving
                    isTextureReady = false;
                    didRender = true;
                }
                else
                {
                    Debug.LogError("Failed to convert RenderTexture to Texture2D.");
                }
            }
        }
        else
        {
            isTextureReady = true;
        }
    }

    private Texture2D RenderTextureToTexture(RenderTexture rt)
    {
        RenderTexture.active = rt;
        Texture2D texture = new Texture2D(rt.width, rt.height);

        try
        {
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            texture.Apply();
        }
        catch (UnityException e)
        {
            Debug.LogError($"Failed to read pixels from RenderTexture: {e.Message}");
            texture = null;
        }
        finally
        {
            RenderTexture.active = null;
        }

        return texture;
    }

    private void SaveTextureToFile(Texture2D texture, string filePath)
    {
        try
        {
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
            Debug.Log($"Texture saved to: {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save texture to file: {e.Message}");
        }
    }

    // Call this method when the rendering is complete
    public void OnRenderingComplete()
    {
        isTextureReady = true;
    }
}

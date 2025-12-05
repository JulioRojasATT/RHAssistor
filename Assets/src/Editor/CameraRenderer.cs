using System.IO;
using UnityEngine;

public class CameraRenderer : MonoBehaviour
{
    public int fileCounter;
    [SerializeField] private Camera camera;

    public void Capture()
    {
        Debug.Log("Excuse me");
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        Debug.Log("Taking picture");
#pragma warning disable RS0030 // Do not use banned APIs
        File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + fileCounter + ".png", bytes);
#pragma warning restore RS0030 // Do not use banned APIs
        fileCounter++;
    }

    [SerializeField] private int resWidth = 2550;
    [SerializeField] private int resHeight = 3300;

    public void TakeHiResShot()
    {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToJPG();
        string filename = Application.dataPath + "/Backgrounds/" + fileCounter + ".jpg";
#pragma warning disable RS0030 // Do not use banned APIs
        System.IO.File.WriteAllBytes(filename, bytes);
        //File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + fileCounter + ".png", bytes);
#pragma warning restore RS0030 // Do not use banned APIs
        fileCounter++;
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}
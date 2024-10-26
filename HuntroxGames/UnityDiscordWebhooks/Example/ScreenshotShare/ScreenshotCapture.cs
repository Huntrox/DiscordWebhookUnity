using UnityEngine;
using UnityEngine.Events;

namespace HuntroxGames.Examples
{
    public class ScreenshotCapture : MonoBehaviour
    {
        public UnityEvent<Texture2D> onCaptureFinished;
        [SerializeField]private Camera captureCam;
        
        private Vector2Int captureSize;
        private Texture2D preview;
        private bool captureFrame;
        
        

        public void Capture()
        {
            captureFrame = true;
            captureSize = new Vector2Int(Screen.width, Screen.height);
            captureCam.targetTexture = RenderTexture.GetTemporary(captureSize.x, captureSize.y, 16);
        }
        
        private void OnPostRender()
        {
            if (!captureFrame) 
                return;
        
            captureFrame = false;
            preview = new Texture2D(captureSize.x, captureSize.y, TextureFormat.RGB565, false);
            var rect = new Rect(0, 0, captureSize.x, captureSize.y);
            preview.name = "Screenshot Preview";
            preview.ReadPixels(rect, 0, 0);
            preview.Apply();
            RenderTexture.ReleaseTemporary(captureCam.targetTexture);
            captureCam.targetTexture = null;
            onCaptureFinished.Invoke(preview);
        }
    }
}

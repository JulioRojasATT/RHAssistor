using UnityEngine;
using UnityEngine.Video;

public class WebGLVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    public void PlayVideoByURL(string url)
    {
        videoPlayer.url = url;

        videoPlayer.targetCameraAlpha = 1.0f;        
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPlayerPrepared;
    }

    private void OnVideoPlayerPrepared(VideoPlayer player) {
        player.Play();
    }
}

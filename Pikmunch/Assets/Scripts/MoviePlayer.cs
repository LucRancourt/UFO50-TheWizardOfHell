using UnityEngine;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour
{
    [SerializeField] private MovieScene[] movieScenes;
    [SerializeField] private Image movieScreen;

    private int _sceneIndex;
    private int _frameIndex;

    private void Awake()
    {
        _sceneIndex = 0;
        _frameIndex = 0;
    }

    public void Next()
    {
        _frameIndex++;

        if (_sceneIndex >= movieScenes.Length)
        {
            return;
        }

        if (_frameIndex >= movieScenes[_sceneIndex].sceneImages.Length)
        {
            _frameIndex = 0;
            _sceneIndex++;
        }

        if (_sceneIndex >= movieScenes.Length)
        {
            return;
        }

        SetImage();
    }

    private void SetImage()
    {
        movieScreen.sprite = movieScenes[_sceneIndex].sceneImages[_frameIndex];
    }
}

[System.Serializable]
public class MovieScene
{
    public Sprite[] sceneImages;
}
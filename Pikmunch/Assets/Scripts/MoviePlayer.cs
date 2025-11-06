using UnityEngine;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour
{
    [SerializeField] private MovieScene[] movieScenes;
    [SerializeField] private Image movieScreen;

    [SerializeField] private Image blackForeground;

    private int _sceneIndex;
    private int _frameIndex;

    private float _fadeValue;
    private bool _timeToFade;

    private bool _wasChecked;
    private bool _toBlack;

    private bool _switchImageSuccessful;

    private bool _startDelay;


    private void Awake()
    {
        _sceneIndex = 0;
        _frameIndex = 0;
        _switchImageSuccessful = true;

        _startDelay = true;
        Invoke("StartDelay", 3.0f);
    }

    private void StartDelay()
    {
        _startDelay = false;
    }

    public void Next()
    {
        if (_startDelay) return;


        if (_sceneIndex >= movieScenes.Length)
        {
            return;
        }


        if (_frameIndex + 1 >= movieScenes[_sceneIndex].sceneImages.Length)
        {
            _frameIndex = 0;
            _sceneIndex++;

            if (_sceneIndex >= movieScenes.Length)
            {
                return;
            }

            _switchImageSuccessful = false;
            Fade();
            Invoke("Fade", 2.0f);
            Invoke("SetImage", 2.0f);
            _switchImageSuccessful = false;

            return;
        }

        if (!_switchImageSuccessful) return;

        _frameIndex++;
        SetImage();
    }

    private void SetImage()
    {
        if (_startDelay) return;

        _startDelay = true;

        movieScreen.sprite = movieScenes[_sceneIndex].sceneImages[_frameIndex];
        _switchImageSuccessful = true;
        CancelInvoke();

        Invoke("StartDelay", 1.0f);
    }

    private void Start()
    {
        _fadeValue = 1.0f;
        SetOpacity(_fadeValue);

        Fade();
    }

    public void Fade()
    {
        _timeToFade = true;
    }

    private void SetOpacity(float alphaValue)
    {
        Color tempColor = blackForeground.color;

        tempColor.a = Mathf.Clamp01(alphaValue);

        blackForeground.color = tempColor;
    }

    private void Update()
    {
        if (_timeToFade)
        {
            if (!_wasChecked)
            {
                if (_fadeValue >= 1.0f)
                    _toBlack = false;
                else
                    _toBlack = true;

                _wasChecked = true;
            }

            if (_toBlack)
            {
                _fadeValue += Time.deltaTime;
                SetOpacity(_fadeValue);

                if (_fadeValue >= 1.0f)
                {
                    _timeToFade = false;
                    _wasChecked = false;
                }
            }
            else
            {
                _fadeValue -= Time.deltaTime;
                SetOpacity(_fadeValue);

                if (_fadeValue <= 0.0f)
                {
                    _timeToFade = false;
                    _wasChecked = false;
                }
            }
        }
    }
}

[System.Serializable]
public class MovieScene
{
    public Sprite[] sceneImages;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text Text;

    private float _updateTime;

    private void Start()
    {
        Screen.SetResolution(1280, 720, true);
    }

    private void Update()
    {
        _updateTime += Time.unscaledDeltaTime;
        if(_updateTime >= 0.3f)
        {
            var fps = 1f / Time.unscaledDeltaTime;
            Text.text = $"FPS: {fps:N0}";
            _updateTime = 0f;
        }
    }
}

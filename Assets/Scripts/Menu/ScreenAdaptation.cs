using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAdaptation : MonoBehaviour
{
    public Vector2 ScreenRatio;
    private Camera _camera;
    private CanvasScaler _myCanvasScaler;

    void Start()
    {
        _camera = Camera.main;
        _myCanvasScaler = GetComponent<CanvasScaler>();
    }

    void Update()
    {
        if (_camera.aspect < (ScreenRatio.x/ScreenRatio.y))
        {
            _myCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;
        }
        else
        {
            _myCanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
    }
}

using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    public void OnZoomChanged(float value)
    {
        Camera.fieldOfView = Mathf.Lerp(60, 30, value);
    }
}
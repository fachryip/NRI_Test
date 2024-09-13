using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private MeshRenderer Renderer;

    private void OnEnable()
    {
        Renderer.material.color = Random.ColorHSV();
    }
}
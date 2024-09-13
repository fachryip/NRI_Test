using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;

    private Camera _mainCamera;

    public Action<GameObject> OnMushroomSpawned;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR_WIN
        if (Input.GetMouseButtonDown(0))
        {
            TrySpawnMushroom(Input.mousePosition);
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TrySpawnMushroom(touch.position);
            }
        }
#endif
    }

    private void TrySpawnMushroom(Vector2 screenPosition)
    {
        if(Physics.Raycast(_mainCamera.ScreenPointToRay(screenPosition), out RaycastHit hit, 100f, 1 << 6))
        {
            var mushroom = Instantiate(Prefab, hit.point, Quaternion.identity, transform);
            mushroom.gameObject.SetActive(true);
            OnMushroomSpawned?.Invoke(mushroom);
        }
    }
}

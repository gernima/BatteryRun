using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 0.4f;
    private Transform playerTransform;
    private float cameraVelocity;
    private Vector3 offset;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = this.transform;
        offset = mainCamera.transform.position - playerTransform.position;
    }
    private void LateUpdate()
    {
        Vector3 newCameraPosition = mainCamera.transform.position;
        mainCamera.transform.position = new Vector3(
            Mathf.SmoothDamp(newCameraPosition.x, playerTransform.position.x, ref cameraVelocity, cameraSpeed * Time.deltaTime),
            newCameraPosition.y, playerTransform.position.z + offset.z);
    }
}

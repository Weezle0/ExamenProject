using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    Camera mainCamera;

    public void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        // Zoom In/Out
        float zoomDelta = Input.GetAxis("Zoom") * zoomSpeed * Time.deltaTime;
        ZoomCamera(zoomDelta);

        // Move Left/Right and Up/Down
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        MoveCamera(horizontalInput, verticalInput);
    }

    void ZoomCamera(float delta)
    {

        // Calculate new zoom level
        float newZoom = mainCamera.fieldOfView - delta;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Apply the new zoom
        mainCamera.fieldOfView = newZoom;
    }

    void MoveCamera(float horizontalInput, float verticalInput)
    {
        // Move the camera horizontally and vertically
        Vector3 newPosition = transform.position;
        newPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
        newPosition.z += verticalInput * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }
}

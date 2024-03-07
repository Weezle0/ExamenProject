using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        mainCamera = this.GetComponent<Camera>();
    }
    void Update()
    {
        // zoom in/out
        float zoomDelta = Input.GetAxis("Zoom") * zoomSpeed * Time.deltaTime;
        ZoomCamera(zoomDelta);

        // move left/right and up/down
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        MoveCamera(horizontalInput, verticalInput);

        if(Input.GetMouseButtonDown(0))
        {
            if (CheckUpgradable())
            {

            }
        }
    }

    void ZoomCamera(float delta)
    {

        // calculate new zoom level
        float newZoom = mainCamera.fieldOfView - delta;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // apply the new zoom
        mainCamera.fieldOfView = newZoom;
    }

    void MoveCamera(float horizontalInput, float verticalInput)
    {
        // move the camera horizontally and vertically
        Vector3 newPosition = transform.position;
        newPosition.x += horizontalInput * moveSpeed * Time.deltaTime;
        newPosition.z += verticalInput * moveSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    private bool CheckUpgradable()
    {
        Debug.Log("Checking...");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit target))
        {
            Debug.Log(target.transform.name);
            if (target.transform.gameObject.layer == 3)
            {
                Debug.Log("Upgradeable");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.Log(target.transform.name);
        }
        return false;
    }
}

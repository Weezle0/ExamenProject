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
    private WorkerManager workerManager;
    Camera mainCamera;

    public void Start()
    {
        workerManager = WorkerManager.instance;
        mainCamera = gameObject.GetComponent<Camera>();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit target))
            {
                Debug.DrawLine(Camera.main.transform.position, target.transform.position);
                // if the selected target is a worker update the worker manager
                if(target.transform.GetComponent<WorkerScript>())
                {
                    Debug.Log("worker");
                    var selectedWorker = target.transform.GetComponent<WorkerScript>();
                    workerManager.selectedWorker = selectedWorker;
                }


                // if the selected target is a machine check if it already has a worker
                if(target.transform.GetComponent<MachineClass>())
                {
                    MachineClass selectedMachine = target.transform.GetComponent<MachineClass>();
                    if(selectedMachine.hasWorker)
                    {
                        // if it has a worker, enable the upgrade menu
                    }

                    // if it has no worker and there is a worker selected make it go the the machine
                    else if(!selectedMachine.hasWorker && workerManager.selectedWorker != null)
                    {
                        workerManager.selectedWorker.ChangeDestination(selectedMachine.transform);
                        workerManager.selectedWorker = null;
                    }
                }


                // if the selected target is the bar, open the menu to upgrade it and open the sell menu
                if(target.transform.GetComponent<BarHandler>())
                {
                    BarHandler selectedBar = target.transform.GetComponent<BarHandler>();
                    if (selectedBar.hasWorker)
                    {
                        // enable upgrade menu
                    }

                    // if the bar has no worker and a worker is selected make the worker go to the bar
                    else if (!selectedBar.hasWorker && workerManager.selectedWorker != null)
                    {
                        workerManager.selectedWorker.ChangeDestination(selectedBar.transform);
                    }
                }
            }
            else
            {
                Debug.Log("Nothing Hit");
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

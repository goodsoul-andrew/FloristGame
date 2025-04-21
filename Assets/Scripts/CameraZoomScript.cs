using UnityEngine;
using UnityEngine.InputSystem;

public class OrthographicZoom : MonoBehaviour
{
    public Camera cam;
    private PlayerInput cameraInput;
    public float maxZoom = 5;
    public float minZoom = 20;
    public float sensitivity = 1;

    void Start()
    {
        cam = GetComponent<Camera>();
        cameraInput = GetComponent<PlayerInput>();
        cameraInput.actions["Zoom"].performed += HandleMouseScroll;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        var newZoom = cam.orthographicSize - input.y * sensitivity;
        //Debug.Log($"newZoom is {newZoom}");
        newZoom = Mathf.Clamp(newZoom, maxZoom, minZoom);
        //Debug.Log($"newZoom after clamp is {newZoom}");
        cam.orthographicSize = newZoom;
        //Debug.Log($"Zoom is {cam.orthographicSize}");
    }
}
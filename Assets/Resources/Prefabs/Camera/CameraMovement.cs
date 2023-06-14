using UnityEngine;
using UnityEngine.InputSystem;



public class CameraMovement : MonoBehaviour
{
    private Move cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;

    [SerializeField]
    private float acceleration = 10f;

    [SerializeField]
    private float damping = 15f;

    [SerializeField]
    private float stepSize = 2f;

    [SerializeField]
    private float zoomDampening = 7.5f;

    [SerializeField]
    private float minHeight = 5f;

    [SerializeField]
    private float maxHeight = 50f;

    [SerializeField]
    private float zoomSpeed = 10f;


    [SerializeField]
    private float maxRotationSpeed = 1f;

    [SerializeField]
    [Range(0f, 0.1f)]
    private float edgeTolerance = 0.05f;

    //value set in various functions 
    //used to update the position of the camera base object.
    private Vector3 targetPosition;

    private float zoomHeight;

    //used to track and maintain velocity w/o a rigidbody
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    //tracks where the dragging action started
    Vector3 startDrag;

    private void Awake()
    {
        cameraActions = new Move();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.z;
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;

        movement = cameraActions.Camera.Movement;
        
        //cameraActions.Camera.Rotate.performed += RotateCamera;
        //cameraActions.Camera.Zoom.performed += ZoomCamera;
        cameraActions.Camera.Enable();

    }

    private void OnDisable()
    {
        //cameraActions.Camera.Rotate.performed -= RotateCamera;
        //cameraActions.Camera.Zoom.performed -= ZoomCamera;
        cameraActions.Camera.Disable();
    }

    private void Update()
    {
        //inputs
        GetKeyboardMovement();
        ZoomCamera();
        //CheckMouseAtScreenEdge();
        //DragCamera();

        //move base and camera objects
        //UpdateVelocity();
        UpdateBasePosition();
        //UpdateCameraPosition();
    }

    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = this.transform.position;
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                    + movement.ReadValue<Vector2>().y * GetCameraUp();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            targetPosition += inputValue;
    }

    //private void DragCamera()
    //{
    //    if (!Mouse.current.rightButton.isPressed)
    //        return;

    //    //create plane to raycast to
    //    Plane plane = new Plane(Vector3.up, Vector3.zero);
    //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

    //    if (plane.Raycast(ray, out float distance))
    //    {
    //        if (Mouse.current.rightButton.wasPressedThisFrame)
    //            startDrag = ray.GetPoint(distance);
    //        else
    //            targetPosition += startDrag - ray.GetPoint(distance);
    //    }
    //}

    //// 滑鼠碰到螢幕邊緣時捲動
    //private void CheckMouseAtScreenEdge()
    //{
    //    //mouse position is in pixels
    //    Vector2 mousePosition = Mouse.current.position.ReadValue();
    //    Vector3 moveDirection = Vector3.zero;

    //    //horizontal scrolling
    //    if (mousePosition.x < edgeTolerance * Screen.width)
    //        moveDirection += -GetCameraRight();
    //    else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
    //        moveDirection += GetCameraRight();

    //    //vertical scrolling
    //    if (mousePosition.y < edgeTolerance * Screen.height)
    //        moveDirection += -GetCameraUp();
    //    else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
    //        moveDirection += GetCameraUp();

    //    targetPosition += moveDirection;
    //}

    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            //create a ramp up or acceleration
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            //create smooth slow down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        //reset for next frame
        targetPosition = Vector3.zero;
    }

    private void ZoomCamera()
    {
        Vector3 inputValue = cameraActions.Camera.Zoom.ReadValue<Vector2>().y * GetCameraForward() / 40f;


        while (inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
            inputValue.z /= 1.05f;
            //zoomValue -= 0.02f;

            //Debug.Log(inputValue);
        }


        //if (inputValue.sqrMagnitude > 0.1f)
        //{
        //    targetPosition += inputValue;
        //    //Debug.Log("Scroll" + inputValue);
        //}

    }

    private void UpdateCameraPosition()
    {
        //set zoom target
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, zoomHeight);
        //add vector for forward/backward zoom
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.z) * Vector3.forward;

        //cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(this.transform);
    }

    private void RotateCamera(InputAction.CallbackContext obj)
    {
        if (!Mouse.current.middleButton.isPressed)
            return;

        float inputValue = obj.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, inputValue * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    //gets the horizontal forward vector of the camera
    private Vector3 GetCameraUp()
    {
        Vector3 up = cameraTransform.up;
        up.z = 0f;
        return up;
    }

    //gets the horizontal right vector of the camera
    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.x = 0f;
        return forward;
    }
}
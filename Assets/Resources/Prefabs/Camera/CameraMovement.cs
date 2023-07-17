using UnityEngine;
using UnityEngine.InputSystem;



public class CameraMovement : MonoBehaviour
{
    public static CameraMovement _instance;

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
    private float zoomSpeed = 200f;

    // 基於上次的位置更新相機位置
    private Vector3 targetPosition;


    // 追蹤和維持速度
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    // 儲存光源
    private Light[] lights;


    private void Awake()
    {
        _instance = this;

        cameraActions = new Move();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;

        lights = FindObjectsOfType<Light>();
    }

    private void OnEnable()
    {
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;

        movement = cameraActions.Camera.Movement;

        cameraActions.Camera.Enable();

    }

    private void OnDisable()
    {
        cameraActions.Camera.Disable();
    }

    private void Update()
    {
        if (Data.moveCamera) 
        {
            GetKeyboardMovement();
            ZoomCamera();
            UpdateBasePosition();
        }
        if (Data.resetCamera)
        {
            Data.resetCamera = false;

            transform.position = new Vector3(0, 0, -12);
            
        }
    }


    // 讀取鍵盤WASD移動視角
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                    + movement.ReadValue<Vector2>().y * GetCameraUp();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            targetPosition += inputValue;
    }

    // 更新視角位置
    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        targetPosition = Vector3.zero;

        // 更新光源的位置

        float distance = 30f; // 光源與相機的距離，可以根據需求調整
        float forwardOffset = 12f; // 向前的偏移量，依相機至軌道距離調整

        for (int i = 0; i < lights.Length; i++)
        {
            Vector3 lightPositionOffset = GetLightPositionOffset(i);
            Vector3 newLightPosition = transform.position + lightPositionOffset.normalized * distance + cameraTransform.forward * forwardOffset;
            lights[i].transform.position = newLightPosition;
        }
    }

    private Vector3 GetLightPositionOffset(int index)
    {
        // 根據索引返回光源對應的偏移量
        switch (index)
        {
            case 0:
                return cameraTransform.forward * -1f + cameraTransform.up + cameraTransform.right;
            case 1:
                return cameraTransform.forward * -1f + cameraTransform.up * -1f + cameraTransform.right;
            case 2:
                return cameraTransform.forward * -1f + cameraTransform.up + cameraTransform.right * -1f;
            case 3:
                return cameraTransform.forward * -1f + cameraTransform.up * -1f + cameraTransform.right * -1f;
            default:
                return Vector3.zero;
        }
    }

    // 滾輪放大縮小畫面
    private void ZoomCamera()
    {
        Vector3 inputValue = cameraActions.Camera.Zoom.ReadValue<Vector2>().y * GetCameraForward() / 40f;

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {

            targetPosition += inputValue * zoomSpeed;
        }
    }


    // 相機視角上下移動
    private Vector3 GetCameraUp()
    {
        Vector3 up = cameraTransform.up;
        up.z = 0f;
        return up;
    }

    // 相機視角左右移動
    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }

    // 相機視角前後移動
    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.x = 0f;
        return forward;
    }

}
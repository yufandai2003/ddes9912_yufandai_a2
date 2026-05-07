using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;
    public float gamerotateSpeed = 5f;
    public Transform target;
    public float zoomSpeed = 10f;
    public float minZoom = 1f;
    public float maxZoom = 10f;

    private Vector3 lastMousePosition;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.CompareTag("mubiao"))
            {
                target = hit.transform;
            }
        }

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(zoom) > 0.01f && Input.GetKey(KeyCode.LeftAlt))
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - zoom * zoomSpeed, minZoom, maxZoom);
            GameManager.ins.Tiop.text = "Zooming view";
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            transform.Translate(-delta.x * moveSpeed * Time.deltaTime, -delta.y * moveSpeed * Time.deltaTime, 0);
            GameManager.ins.Tiop.text = "Panning camera";
        }

        if (Input.GetMouseButton(1) && target != null)
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            transform.RotateAround(target.position, Vector3.up, horizontal);
            transform.RotateAround(target.position, transform.right, -vertical);
            GameManager.ins.Tiop.text = "Rotating view";
        }

        lastMousePosition = Input.mousePosition;
    }
}
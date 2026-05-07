using UnityEngine;

// Attach to the parent object of the upper roller: UpRollerParent
public class UpRollerParentSpring : MonoBehaviour
{
    [Header("Spring Target Height (Lowest Point = Initial Position)")]
    public float targetY = 0f;

    [Header("Spring Force")]
    public float springForce = 50f;

    [Header("Damping")]
    public float damping = 5f;

    [Header("Max Bounce Height: 0.02 above lowest point")]
    public float maxOffsetY = 0.02f;

    [Header("Bounce Frequency (Smaller = Slower)")]
    public float shakeInterval = 0.15f;

    private float shakeTimer;
    private Rigidbody rb;
    private float baseY;
    private HandleRotator _handle;

    void Start()
    {
        baseY = transform.position.y;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        _handle = GetComponentInChildren<UpRollerSpring>()?.handleRotator;
    }

    void FixedUpdate()
    {
        float yDelta = targetY - transform.position.y;
        rb.linearVelocity = new Vector3(0, yDelta * springForce - rb.linearVelocity.y * damping, 0);
    }

    void Update()
    {
        if (_handle == null || _handle.rotateState == 0)
        {
            shakeTimer = 0;
            return;
        }

        shakeTimer += Time.deltaTime;
        if (shakeTimer >= shakeInterval)
        {
            shakeTimer = 0;
            float randomY = Random.Range(baseY, baseY + maxOffsetY);
            transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
        }
    }
}
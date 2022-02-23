using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float mouseSpeed = 20;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject armObject;

    Rigidbody body;
    float cameraRotX;
    float bodyRotY;

    Vector3 originalArmPos;
    Vector3 extendedArmPos;
    Vector3 targetArmPos;

    int choppingFrames;
    const int activeChoppingFrames = 5;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originalArmPos = armObject.transform.localPosition;
        targetArmPos = originalArmPos;
        extendedArmPos = armObject.transform.localPosition + armObject.transform.forward * 1;
    }

    private void FixedUpdate()
    {
        float movementZ = Input.GetAxisRaw("Vertical");
        float movementX = Input.GetAxisRaw("Horizontal");

        var cameraForward = cameraObject.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        var direction = cameraForward * movementZ + cameraObject.transform.right * movementX;
        if (direction.magnitude > 1)
        {
            direction.Normalize(); 
        }
        body.AddForce(direction * moveSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {


        float cameraX = Mathf.Clamp(Input.GetAxis("Mouse Y"), -15f, 15f);
        float cameraY = Mathf.Clamp(Input.GetAxis("Mouse X"), -15f, 15f);

        cameraRotX += cameraX * mouseSpeed * Time.deltaTime;
        cameraRotX = Mathf.Clamp(cameraRotX, -60f, 60f);
        bodyRotY += cameraY * mouseSpeed * Time.deltaTime;
        if (bodyRotY < 0)
        {
            bodyRotY = (bodyRotY + 360f) % 360f; 
        }
        //transform.eulerAngles = new Vector3(0, bodyRotY, 0);
        cameraObject.transform.localEulerAngles = new Vector3(-cameraRotX, bodyRotY, 0);


        armObject.transform.localPosition = Vector3.Lerp(armObject.transform.localPosition, originalArmPos, Time.deltaTime * 10);

        if (choppingFrames > 0)
        {
            choppingFrames--;
        }

        if (Input.GetMouseButtonDown(0))
        {
            armObject.transform.localPosition = extendedArmPos;
            choppingFrames = activeChoppingFrames;
        }
    }

    internal void OnHandTrigger(Enemy enemy)
    {
        if (choppingFrames > 0)
        {
            choppingFrames = 0;
            Vector3 direction = enemy.transform.position - transform.position;
            enemy.GetChopped(direction.normalized);
        }
    }
    
}

using System;
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

    private void Update()
    {
        float movementZ = Input.GetAxisRaw("Vertical");
        float movementX = Input.GetAxisRaw("Horizontal");

        float cameraX = Input.GetAxis("Mouse Y");
        float cameraY = Input.GetAxis("Mouse X");

        var direction = transform.forward * movementZ + transform.right * movementX;
        direction.Normalize();

        body.AddForce(direction * moveSpeed);

        cameraRotX += cameraX * mouseSpeed * Time.deltaTime;
        bodyRotY += cameraY * mouseSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, bodyRotY, 0);
        cameraObject.transform.localEulerAngles = new Vector3(-cameraRotX, 0, 0);

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

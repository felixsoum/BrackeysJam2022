using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float mouseSpeed = 20;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] GameObject cameraObject;
    [SerializeField] GameObject armObject;
    [SerializeField] GameObject beerObject;
    [SerializeField] GameObject thrownBeerPrefab;

    [SerializeField] Transform beerSpawn;

    public Action<int> OnBeerCountChanged;

    Rigidbody body;
    float cameraRotX;
    float bodyRotY;

    Vector3 originalArmPos;
    Vector3 extendedArmPos;
    Vector3 targetArmPos;

    float upArmRot = -30;
    float downArmRot = 30;
    float targetArmRot;

    bool hasBeer = true;
    bool isAiming;
    float beerChargeTime;

    int beerCount;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
        bodyRotY = transform.eulerAngles.y;
        beerObject.SetActive(false);
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

        cameraObject.transform.localEulerAngles = new Vector3(-cameraRotX, bodyRotY, 0);

        if (Input.GetMouseButtonDown(0) && hasBeer && !isAiming && beerCount > 0)
        {
            isAiming = true;
            targetArmRot = upArmRot;
            beerChargeTime = 0;
        }
        else if (Input.GetMouseButtonUp(0) && hasBeer && isAiming)
        {
            OnBeerCountChanged?.Invoke(--beerCount);
            isAiming = false;
            hasBeer = false;
            beerObject.SetActive(false);
            armObject.transform.localEulerAngles = new Vector3(upArmRot, 0, 0);
            targetArmRot = downArmRot;

            var thrownBeer = Instantiate(thrownBeerPrefab, beerSpawn.position, beerSpawn.rotation).GetComponent<ThrownBeer>();
            float beerForce = Mathf.Min(beerChargeTime * 10, 100);
            thrownBeer.Throw(beerSpawn.forward * beerForce);
        }
        else if (!hasBeer && Mathf.Abs(armObject.transform.localEulerAngles.x - targetArmRot) < 1)
        {
            hasBeer = true;
            if (beerCount > 0)
            {
                beerObject.SetActive(true); 
            }
            targetArmRot = 0;
        }

        if (hasBeer && isAiming)
        {
            beerChargeTime += Time.deltaTime;
        }

        float nextAngle = Mathf.LerpAngle(armObject.transform.localEulerAngles.x, targetArmRot, Time.deltaTime * 5);
        armObject.transform.localEulerAngles = new Vector3(nextAngle, 0, 0);
    }

    internal void OnHandTrigger(Enemy enemy)
    {
        //if (choppingFrames > 0)
        //{
        //    choppingFrames = 0;
        //    Vector3 direction = enemy.transform.position - transform.position;
        //    enemy.GetChopped(direction.normalized);
        //}
    }

    internal void RefillBeer()
    {
        hasBeer = true;

        if (!beerObject.activeInHierarchy && beerCount == 0)
        {
            beerObject.SetActive(true);
            armObject.transform.localEulerAngles = new Vector3(downArmRot, 0, 0);
            targetArmRot = 0;
        }

        beerCount = 6;
        OnBeerCountChanged?.Invoke(beerCount);
    }

}

using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    [SerializeField] GameObject visualObject;
    protected Player player;
    protected Camera mainCamera;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainCamera = Camera.main;    
    }

    protected virtual void Update()
    {
        visualObject.transform.forward = mainCamera.transform.forward;
    }

    internal virtual void OnBeerHit()
    {

    }

    protected bool IsPlayerInVision()
    {
        var dir = player.transform.position - transform.position;
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, dir, out hitInfo);
        return hitInfo.collider.gameObject.CompareTag("Player");
    }
}

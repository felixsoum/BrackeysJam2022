using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject visual;
    Rigidbody body;
    Player player;
    Camera playerCamera;

    float flipTimer = 0.5f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            //agent.SetDestination(player.transform.position);
            flipTimer -= Time.deltaTime;
            if (flipTimer <= 0)
            {
                flipTimer += 0.5f;
                var nextScale = visual.transform.localScale;
                nextScale.x *= -1;
                visual.transform.localScale = nextScale;
            }
        }

        transform.rotation = playerCamera.transform.rotation;
    }

    internal void GetChopped(Vector3 direction)
    {
        body.AddForce(direction * 20, ForceMode.Impulse);
    }
}

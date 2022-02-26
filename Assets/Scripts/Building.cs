using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] MeshRenderer buildingRenderer;
    [SerializeField] Material[] materials;
    [SerializeField] int materialIndex;
    [SerializeField] bool isFake;

    GameObject player;
    Vector3 originalPos;

    private void OnValidate()
    {
        buildingRenderer.material = materials[materialIndex];
    }

    private void Start()
    {
        originalPos = transform.position;
        if (isFake)
        {
            player = GameObject.FindGameObjectWithTag("Player"); 
        }
    }

    private void Update()
    {
        if (!isFake)
        {
            return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 15f)
        {
            var nextPos = originalPos;
            nextPos.z += 0.05f * Mathf.Sin(Time.time * 20f);
            transform.position = nextPos;
        }
        else
        {
            transform.position = originalPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFake && collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

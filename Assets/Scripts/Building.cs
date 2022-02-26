using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] MeshRenderer buildingRenderer;
    [SerializeField] Material[] materials;
    [SerializeField] int materialIndex;
    [SerializeField] bool isFake;

    private void OnValidate()
    {
        buildingRenderer.material = materials[materialIndex];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFake && collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}

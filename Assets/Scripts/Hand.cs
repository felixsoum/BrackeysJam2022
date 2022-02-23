using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Player player;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.OnHandTrigger(other.gameObject.GetComponent<Enemy>());
        }
    }
}

using UnityEngine;

public class ThrownBeer : MonoBehaviour
{
    [SerializeField] Rigidbody body;
    float lifetime = 5f;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            body.AddForce(Vector3.down * Time.deltaTime * 10, ForceMode.VelocityChange);
        }
    }

    public void Throw(Vector3 force)
    {
        body.AddForce(force, ForceMode.VelocityChange);
        body.AddTorque(Random.onUnitSphere * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        var baseNPC = collision.gameObject.GetComponent<BaseNPC>();
        if (baseNPC == null)
        {
            return;
        }

        baseNPC.OnBeerHit();
    }
}

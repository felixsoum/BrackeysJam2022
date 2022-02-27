using UnityEngine;

public class BaseNPC : MonoBehaviour
{
    [SerializeField] protected GameObject visualObject;
    [SerializeField] int life = 3;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] Collider bodyCollider;
    [SerializeField] SpriteRenderer visualRenderer;
    protected Player player;
    protected Camera mainCamera;
    protected bool isDead;
    protected bool isHarmful = true;

    float spriteAlpha;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mainCamera = Camera.main;
        spriteAlpha = visualRenderer.color.a;
    }

    protected virtual void Update()
    {
        visualObject.transform.localScale = Vector3.Lerp(visualObject.transform.localScale, Vector3.one, Time.deltaTime * 10);
        visualObject.transform.forward = mainCamera.transform.forward;

        Color white = Color.white;
        white.a = spriteAlpha;
        visualRenderer.color = Color.Lerp(visualRenderer.color, white, Time.deltaTime * 10);

        if (isHarmful && !isDead && Vector3.Distance(transform.position, player.transform.position) < 6f && IsPlayerInVision())
        {
            player.AddInsanity(0.05f * Time.deltaTime);
        }
    }

    internal virtual void OnBeerHit()
    {
        if (--life > 0)
        {
            visualObject.transform.localScale = new Vector3(1.25f, 0.8f, 1f);
            Color red = Color.red;
            red.a = spriteAlpha;
            visualRenderer.color = red;
        }
        else
        {
            if (!isDead)
            {
                visualObject.SetActive(false);
                bodyCollider.enabled = false;
                isDead = true;
                deathAudio.Play();
                Invoke("Die", 2f);
                OnDie();
            }
        }
    }

    protected virtual void OnDie()
    {

    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    protected bool IsPlayerInVision()
    {
        var dir = player.transform.position - transform.position;
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, dir, out hitInfo);
        return hitInfo.collider.gameObject.CompareTag("Player");
    }
}

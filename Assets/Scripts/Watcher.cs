using UnityEngine;

public class Watcher : BaseNPC
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;
    [SerializeField] AudioSource alertAudio;
    [SerializeField] GameObject alertVisual;

    float alert;
    Vector3 lastPlayerPosition;

    protected override void Update()
    {
        float x = Mathf.Sin(Time.time * alert) * alert * 0.002f;
        var pos = visualObject.transform.localPosition;
        pos.x = x;
        visualObject.transform.localPosition = pos;
        base.Update();
    }

    private void FixedUpdate()
    {
        bool isAlerted = false;
        if (Vector3.Distance(transform.position, player.transform.position) < 12f && IsPlayerInVision())
        {
            var distance = Vector3.Distance(lastPlayerPosition, player.transform.position);

            if (Vector3.Distance(lastPlayerPosition, player.transform.position) > 0.1f)
            {
                isAlerted = true;
            }
        }

        if (isAlerted)
        {
            alert += Time.fixedDeltaTime * 30f;
        }
        else
        {
            alert -= Time.fixedDeltaTime * 30f;
        }
        alertVisual.SetActive(alert >= 20);

        alert = Mathf.Clamp(alert, 0, 59);

        if (alert > 50 && !isDead)
        {
            player.AddInsanity(0.5f * Time.fixedDeltaTime);
            alertAudio.volume = 1;
        }
        else
        {
            alertAudio.volume = 0;
        }

        int spriteIndex = (int)(alert / 10);
        spriteRenderer.sprite = sprites[spriteIndex];
        lastPlayerPosition = player.transform.position;
    }

    internal override void OnBeerHit()
    {
        alert = 60;
        base.OnBeerHit();
    }
}

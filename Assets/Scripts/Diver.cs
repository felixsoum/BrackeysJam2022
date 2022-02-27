using UnityEngine;

public class Diver : BaseNPC
{
    [SerializeField] AudioSource voiceAudio;
    bool isAppearing;

    protected override void Start()
    {
        isHarmful = false;
        base.Start();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isAppearing && collision.gameObject.CompareTag("Player"))
        {
            voiceAudio.Play();
            isAppearing = true;
            Invoke("Leave", 17f);
        }
    }

    public void Leave()
    {
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        if (isAppearing)
        {
            visualObject.transform.localPosition = Vector3.Lerp(visualObject.transform.localPosition, new Vector3(0, 0.5f, 0), Time.deltaTime);
        }
        base.Update();
    }

    protected override void OnDie()
    {
        voiceAudio.Stop();
        base.OnDie();
    }
}

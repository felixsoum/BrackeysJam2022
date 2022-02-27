using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image[] beerIcons;
    [SerializeField] Image sanityImage;
    [SerializeField] RectTransform sanityTransform;
    Player player;
    private float sanity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnBeerCountChanged += OnBeerCountChanged;
        player.OnSanityChanged += OnSanityChanged;

        OnBeerCountChanged(0);
    }

    private void Update()
    {
        sanityTransform.localScale = new Vector3(1 + 0.2f * Mathf.Sin(Time.time * (1 + 5*sanity))*sanity, 1, 1);
    }

    private void OnSanityChanged(float value)
    {
        sanity = value;
        sanityImage.fillAmount = value;
    }

    private void OnBeerCountChanged(int beerCount)
    {
        for (int i = 0; i < 6; i++)
        {
            beerIcons[i].enabled = i < beerCount;
        }
    }
}

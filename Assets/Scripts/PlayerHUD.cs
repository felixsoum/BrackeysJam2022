using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image[] beerIcons;
    [SerializeField] Image sanityImage;
    [SerializeField] RectTransform sanityTransform;
    [SerializeField] Image blackImage;
    [SerializeField] Image ladyImage;
    [SerializeField] Sprite[] ladySprites;
    int ladySpriteIndex;

    Player player;
    private float sanity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnBeerCountChanged += OnBeerCountChanged;
        player.OnSanityChanged += OnSanityChanged;

        OnBeerCountChanged(0);
        InvokeRepeating("SpriteChange", 0.1f, 0.1f);
    }

    public void SpriteChange()
    {
        ladySpriteIndex = (ladySpriteIndex + 1) % ladySprites.Length;
        ladyImage.sprite = ladySprites[ladySpriteIndex];
    }

    private void Update()
    {
        sanityTransform.localScale = new Vector3(1 + 0.2f * Mathf.Sin(Time.time * (1 + 5 * sanity)) * sanity, 1, 1);

        if (sanity == 1)
        {
            Color color = Color.black;
            color.a = Mathf.Lerp(blackImage.color.a, 1, Time.deltaTime * 2);
            blackImage.color = color;
        }

        if (player.isEnding)
        {
            Color blackColor = Color.black;
            blackColor.a = Mathf.Lerp(blackImage.color.a, 1, Time.deltaTime * 0.5f);
            blackImage.color = blackColor;

            Color color = Color.white;
            color.a = Mathf.Lerp(ladyImage.color.a, 1, Time.deltaTime * 0.5f);
            ladyImage.color = color;
        }
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

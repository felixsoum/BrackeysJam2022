using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image[] beerIcons;
    [SerializeField] Image sanityImage;
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnBeerCountChanged += OnBeerCountChanged;
        player.OnSanityChanged += OnSanityChanged;

        OnBeerCountChanged(0);
    }

    private void OnSanityChanged(float value)
    {
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

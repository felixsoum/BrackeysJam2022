using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image[] beerIcons;
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnBeerCountChanged += OnBeerCountChanged;

        OnBeerCountChanged(0);
    }

    private void OnBeerCountChanged(int beerCount)
    {
        for (int i = 0; i < 6; i++)
        {
            beerIcons[i].enabled = i < beerCount;
        }
    }
}

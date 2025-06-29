using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    public Text idText;
    public Text costText;
    public Text damageText;
    public Image elementIcon;

    private Card _card;

    public void Setup(Card data)
    {
        _card = data;

    }
}

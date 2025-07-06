using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private Image _image;
    //[SerializeField] private Image elementIcon;

    private CardData cardData;

    public void Initialize(CardData card)
    {
        cardData = card;

        _costText.text = card._Cost.ToString();
        _damageText.text = card._Damage.ToString();

        
    }

    public void ResetView()
    {
        cardData = null;
        _costText.text = "";
        _damageText.text = "";
    }

    private Color GetColorByElement(Enum.Element element)
    {
        switch (element)
        {
            case Enum.Element.Fire: return Color.red;
            case Enum.Element.Ice: return Color.cyan;
            case Enum.Element.Wind: return Color.green;
            case Enum.Element.Rock: return new Color(0.6f, 0.4f, 0.2f);
            case Enum.Element.Elect: return Color.yellow;
            default: return Color.gray;
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable All

public class CardView : MonoBehaviour
{
    //ī�� UI������Ʈ Ŭ�����Դϴ�.
    
    //
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private Image _image;
    //[SerializeField] private Image elementIcon;

    private CardData _cardData;

    public void Initialize(CardData card)
    {
        _cardData = card;

        _costText.text = card._Cost.ToString();
        _damageText.text = card._Damage.ToString();

        
    }

    public void ResetView()
    {
        _cardData = null;
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

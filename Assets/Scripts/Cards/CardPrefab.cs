using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private Image _image;
    [SerializeField] private Image elementIcon;

    private Card cardData;

    public void Initialize(Card card)
    {
        cardData = card;

        _costText.text = card._Cost.ToString();
        _damageText.text = card._Damage.ToString();

        // 예시: 속성에 따른 색상 처리
        //elementIcon.color = GetColorByElement(card._element);
    }

    public void ResetView()
    {
        cardData = null;
        _costText.text = "";
        _damageText.text = "";
        //elementIcon.color = Color.white;
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

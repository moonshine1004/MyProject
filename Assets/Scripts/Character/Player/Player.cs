using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    //player ������ ������ �ٷ�� ��ũ��Ʈ �Դϴ�.
    [SerializeField] private UsingCardList _UsingCardList;

    [SerializeField] private CardPooling _cardPooling; //ī�� Ǯ ����
    [SerializeField] private List<CardData> _cardData = new List<CardData>(); //ī�� ������(��ũ���ͺ� ������Ʈ) ����Ʈ
    private CardView _currentCard; //ī�� ������
    [SerializeField] private PlayerController _playerController; //���� ��� ���� ī�� �ε����� ���� �޾ƿ�

    private void Start()
    {
        _UsingCardList.AddCardToHand(_cardData);
    }
    //��ų �׼� �̺�Ʈ�� �κ�ũ �Ǹ� ī�� Ǯ�� �ִ� ī�带 ���� ��
    public void CardKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            _currentCard = _cardPooling.GetCardView(_cardData[_playerController.NowSkillIndex]);
        }
    }


    [ContextMenu("Reset Counter")]
    private void fill()
    {
        _cardData = new List<CardData>(_UsingCardList.hand);
        Debug.Log("test");
    }
}

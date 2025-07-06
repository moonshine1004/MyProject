using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //player ������ ����
    [SerializeField] private CardPooling _cardPooling; //ī�� Ǯ ����
    [SerializeField] private List<CardData> _cardData=new List<CardData>(); //ī�� ������(��ũ���ͺ� ������Ʈ) ����Ʈ
    private CardView _currentCard; //ī�� ������
    [SerializeField] private PlayerController _playerController; //���� ��� ���� ī�� �ε����� ���� �޾ƿ�
    

    //��ų �׼� �̺�Ʈ�� �κ�ũ �Ǹ� ī�� Ǯ�� �ִ� ī�带 ���� ��
    public void CardKeyInput(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            _currentCard = _cardPooling.GetCardView(_cardData[_playerController.NowSkillIndex]);
        }
    }

}

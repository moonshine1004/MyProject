using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CardPooling : MonoBehaviour
{
    //ī�� Ǯ���� ���� Ŭ����
    [SerializeField] private CardView _cardViewPrefab; //ī�� ������ ����
    [SerializeField] private Transform _cardParent; //ī�尡 ������ �θ� ������Ʈ
    private ObjectPool<CardView> _cardPool; //ī�� ������ Ǯ
    private void Start()
    {
        //ī�� ������Ʈ Ǯ ����
        _cardPool = new ObjectPool<CardView>(
            CreateCard,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyCard,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 20
        );
        //�ʱ� ī�� 12�� ����
        for (int i = 0; i < 12; i++)
        {
            var card = CreateCard();
            _cardPool.Release(card);
        }
    }
    //ī�� ���� �޼���
    private CardView CreateCard()
    {
        //ī�� �������� �ν��Ͻ�ȭ �� ��ȯ
        var card = Instantiate(_cardViewPrefab, _cardParent);
        card.gameObject.SetActive(false); //�ν��Ͻ�ȭ �� ��Ȱ��ȭ
        return card; 
    }
    //ī�� Ǯ���� ī�带 ���� ��(Ȱ��ȭ)
    private void OnTakeFromPool(CardView card)
    {
        card.gameObject.SetActive(true);
    }
    //ī�带 Ǯ�� ��ȯ(��Ȱ��ȭ)
    private void OnReturnedToPool(CardView card)
    {
        card.ResetView();
        card.gameObject.SetActive(false);
    }
    //Ǯ�� ũ�⸦ �ʰ����� �� ���͸� ����
    private void OnDestroyCard(CardView card)
    {
        Destroy(card.gameObject);
    }

    // ī�� ������ �޼���
    public CardView GetCardView(CardData data)
    {
        var card = _cardPool.Get();
        card.Initialize(data);
        return card;
    }

    // ī�� ��ȯ �޼���
    public void ReturnCardView(CardView card)
    {
        _cardPool.Release(card);
    }



}

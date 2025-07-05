using UnityEngine;
using UnityEngine.Pool;

public class CardPooling : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent; // Canvas 하위 빈 오브젝트 (예: CardPanel)

    private ObjectPool<CardPrefab> cardPool;

    private void Awake()
    {
        cardPool = new ObjectPool<CardPrefab>(
            createFunc: () => {
                GameObject go = Instantiate(cardPrefab, cardParent);
                return go.GetComponent<CardPrefab>();
            },
            actionOnGet: card => card.gameObject.SetActive(true),
            actionOnRelease: card => {
                card.ResetView();
                card.gameObject.SetActive(false);
            },
            actionOnDestroy: card => Destroy(card.gameObject),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );
    }

    public CardPrefab GetCard(Card cardData, Sprite image)
    {
        var card = cardPool.Get();
        card.Initialize(cardData);
        return card;
    }

    public void ReleaseCard(CardPrefab card)
    {
        cardPool.Release(card);
    }
}

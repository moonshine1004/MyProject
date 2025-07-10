﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UsingCardList : MonoBehaviour
{
    private List<CardData> undealtDeck = new List<CardData>(); //드로우 전 카드 리스트
    private List<CardData> discardPile = new List<CardData>(); //사용된 카드 리스트
    public CardData[] hand = new CardData[5]; //qwert키에 할당되는 카드 배열
    private const int initialHandSize = 5; //드로우 카드 수
    [SerializeField] private Deck _deck; //카드 덱 위임

    
    public void Init(Deck deck)
    {
        List<CardData> shuffled = new List<CardData>(deck.cardDeck); //카드 덱 위임
        //카드 덱을 셔플
        Shuffle(shuffled);
        //드로우 전 카드를 드로우 카드 수 만큼 hand로 이동
        AddCardToHand(shuffled);
        //남은 카드를 드로우 전 카드 리스트에 추가
        for (int i = initialHandSize; i < shuffled.Count; i++)
        {
            undealtDeck.Add(shuffled[i]);
        }

    }

    public void UseCard(int handIndex)
    {
        var usedCard = hand[handIndex]; //handIndex 칸에 있는 카드를 사용된 카드로 옮김
        discardPile.Add(usedCard); //사용된 카드 리스트 사용한 카드를 추가
        hand[handIndex] = null; //사용한 카드는 hand에서 제거(null로 초기화)
        //드로우 전 카드 리스트가 비어있을 때 리필
        if (undealtDeck.Count == 0)
        {
            RefillUndealtDeck();
        }
        //드로우 전 카드 리스트에서 사용한 카드 칸에 새 카드를 뽑아옴
        if (undealtDeck.Count > 0)
        {
            hand[handIndex] = undealtDeck[0];
            undealtDeck.RemoveAt(0);
        }
    }
    private void RefillUndealtDeck()
    {
        if (discardPile.Count == 0) return; //사용된 카드 리스트가 0이면 리필하지 않음
        Shuffle(discardPile); //사용된 카드 리스트를 셔플
        undealtDeck.AddRange(discardPile); //사용된 카드 리스트를 드로우 전 카드 리스트에 추가
        discardPile.Clear(); //사용된 카드 리스트를 비움
    }
    //Fisher–Yates Shuffle
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //i부터 리스트의 마지막 인덱스 중 랜덤으로 선택해서 j에 대입
            int j = Random.Range(i, list.Count);
            //i번째와 랜덤하게 선택된 j번째 요소를 교환
            //스왑하는 스크립트
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    //드로우 카드 리스트를 채우는 메서드
    public void AddCardToHand(List<CardData> list)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == null)
            {
                hand[i] = list[i];
            }
        }
    }

    

}

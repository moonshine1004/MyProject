using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class Deck : MonoBehaviour
{
    //플레이어가 구성한 카드 덱 클래스 입니다
    //카드 덱 리스트
    [SerializeField] public List<CardData> cardDeck =new List<CardData>(12);

    
}

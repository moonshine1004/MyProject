using UnityEngine;

//Card라는 이름의 파일을 만듦
[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
//스크립터블 오브젝트에 대해 정의하는 스크립트
public class CardData : ScriptableObject
{
    //카드 스크립터블 오브젝트 클래스 입니다
    public int CardID;
    public int _Cost;
    public float _Damage;
    public Enum.Element _element;
    public Enum.RangeType _RangeType;
    public Enum.TargetType _TargetType;


}

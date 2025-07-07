using UnityEngine;

//Card��� �̸��� ������ ����
[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
//��ũ���ͺ� ������Ʈ�� ���� �����ϴ� ��ũ��Ʈ
public class CardData : ScriptableObject
{
    //ī�� ��ũ���ͺ� ������Ʈ Ŭ���� �Դϴ�
    public int CardID;
    public int _Cost;
    public float _Damage;
    public Enum.Element _element;
    public Enum.RangeType _RangeType;
    public Enum.TargetType _TargetType;

}

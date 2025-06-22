using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public int CardID;
    public int _Cost;
    public float _Damage;
    public Enum.Element _element;
    public Enum.RangeType _RangeType;
    public Enum.TargetType _TargetType;

}

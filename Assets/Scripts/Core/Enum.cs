using UnityEngine;

public class Enum : MonoBehaviour
{
    //enum�� �����ϴ� Ŭ�����Դϴ�.
    
    //ī�� �Ӽ� ���� enum
    public enum Element
    {
        Normal = 0,
        Fire = 1,
        Ice = 2,
        Wind =3,
        Rock = 4,
        Elect =5,
    }
    //ī�� ���� enum
    public enum RangeType
    {
        Projectile	= 0,
        Line = 1,
        Close = 2,
        Cone = 3,
        Circle = 4,

    }
    //ī�� Ÿ�� Ÿ�� enum
    public enum TargetType
    {
        NoneTarget = 0,
        SelfTarget = 1,
        EnemyTarget = 2,
        AllTarget = 3,
    }
    //��Ʈ�ѷ� Ű�� ��ȣ �Ҵ�
    public enum SkillType
    {
        Q = 0, W = 1, E = 2, R = 3, T = 4
    }
}

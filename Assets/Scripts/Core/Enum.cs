using UnityEngine;

public class Enum : MonoBehaviour
{
    //enum을 관리하는 클래스입니다.
    
    //카드 속성 관리 enum
    public enum Element
    {
        Normal = 0,
        Fire = 1,
        Ice = 2,
        Wind =3,
        Rock = 4,
        Elect =5,
    }
    //카드 범위 enum
    public enum RangeType
    {
        Projectile	= 0,
        Line = 1,
        Close = 2,
        Cone = 3,
        Circle = 4,

    }
    //카드 타겟 타입 enum
    public enum TargetType
    {
        NoneTarget = 0,
        SelfTarget = 1,
        EnemyTarget = 2,
        AllTarget = 3,
    }
    //컨트롤러 키에 번호 할당
    public enum SkillType
    {
        Q = 0, W = 1, E = 2, R = 3, T = 4
    }
}

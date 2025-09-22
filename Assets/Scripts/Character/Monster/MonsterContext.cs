using UnityEngine;



public class MonsterContext
{
    public GameObject Owner { get; set; } //이 오브젝트
    public CapsuleCollider2D OwenrCollider { get; set; } //이 오브젝트의 콜라이더
    public GameObject Target { get; set; } //타겟 오브젝트

}
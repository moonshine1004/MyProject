using UnityEngine;

public class MonsterAttackState : MonsterStateMachine, MonsterBaseState.IState
{
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Attack;

    //몬스터의 공격 상태를 정의한 클래스입니다.
    [SerializeField]

    public void Enter()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

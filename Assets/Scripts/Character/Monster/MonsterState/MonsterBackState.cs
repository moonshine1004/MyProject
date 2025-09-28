using UnityEngine;

public class MonsterBackState : MonsterStateMachine, MonsterBaseState.IState
{
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Back;

    public void Enter()
    {
        monsterStateMachine = GetComponent<MonsterStateMachine>();
    }

    public void Exit()
    {
        monsterStateMachine.ChangeState(MonsterBaseState.MonsterState.Idle); //상태를 idle로 변경
    }

    public void Update()
    {
        
    }
}

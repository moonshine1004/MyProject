using UnityEngine;

public class MonsterDeathState : MonsterStateMachine, MonsterBaseState.IState
{
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Death;

    public void Enter()
    {
        monsterStateMachine = GetComponent<MonsterStateMachine>();
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
}

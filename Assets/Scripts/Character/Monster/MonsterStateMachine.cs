using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


public class MonsterStateMachine : MonoBehaviour
{

    protected MonsterBaseState.MonsterState _currentState;
    protected GameObject _targetObject = null; //몬스터가 좇는 오브젝트

    private Dictionary<MonsterBaseState.MonsterState, MonsterBaseState.IState> _state;


    private void Start()
    {
        var found = GetComponents<MonsterStateMachine>()
                    .OfType<MonsterBaseState.IState>();
        _state = new Dictionary<MonsterBaseState.MonsterState, MonsterBaseState.IState>();

        foreach (var st in found)
            _state[st.StateType] = st;

        _currentState = MonsterBaseState.MonsterState.Idle; //시작 상태는 idle
        _state[_currentState].Enter();
    }

    public void ChangeState(MonsterBaseState.MonsterState nextState)
    {
        _state[nextState].Enter();
    } 



    // private void Update()
    // {
    //     switch (_currentState)
    //     {
    //         case MonsterBaseState.MonsterState.Idle:
    //             _state[MonsterBaseState.MonsterState.Idle].Enter();
    //             break;
    //         case MonsterBaseState.MonsterState.Chase:
    //             _state[MonsterBaseState.MonsterState.Chase].Enter();
    //             break;
    //         case MonsterBaseState.MonsterState.Attack:
    //             _state[MonsterBaseState.MonsterState.Attack].Enter();
    //             break;
    //         case MonsterBaseState.MonsterState.Back:
    //             _state[MonsterBaseState.MonsterState.Back].Enter();
    //             break;
    //     }
    // }
}

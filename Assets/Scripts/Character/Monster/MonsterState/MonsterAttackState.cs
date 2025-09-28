using System.Collections;
using UnityEngine;

public class MonsterAttackState : MonsterStateMachine, MonsterBaseState.IState
{
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Attack;

    //몬스터의 공격 상태를 정의한 클래스입니다.
    #region 필드 변수
    [SerializeField]
    private GameObject _target; //타겟 오브젝트
    private float _attackCooldown = 5.0f;
    private float _attackRange = 10.0f;
    #endregion

    public void Enter()
    {
        monsterStateMachine = GetComponent<MonsterStateMachine>();
        _target = monsterContext.Target;
    }

    public void Exit()
    {
    }

    public void Update()
    {
    }
    private IEnumerator Attack()
    {
        while (_target.GetComponent<CharacterStat>().IsAlive)
        {
            yield return new WaitForSeconds(_attackCooldown);
        }
    }




    /*
    아이들-->체이스-->공격-->죽음(내가 죽으면)
                        -->아이들(상대가 죽으면)
                        -->원위치(상대가 도망치면)
                        
    
    */
}

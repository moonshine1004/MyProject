using System.Collections;
using UnityEngine;

public class MonsterChaseState : MonsterStateMachine, MonsterBaseState.IState
{
    //몬스터의 chase 상태를 정의한 클래스입니다.
    #region 필드 변수
    [SerializeField]
    private int _range = 5;  //몬스터의 적 감지 범위
    private Vector3 _targetPosition; //몬스터가 쫓을 오브젝트
    private MonsterMovement _monsterMovement; //몬스터의 움직임 오브젝트 복사
    #endregion
    public MonsterBaseState.MonsterState StateType => MonsterBaseState.MonsterState.Chase;

    public void Enter()
    {
        monsterStateMachine = GetComponent<MonsterStateMachine>();
        _monsterMovement = GetComponent<MonsterMovement>();

        _targetPosition = monsterStateMachine.monsterContext.Target.transform.position; //타겟 포지션을 타겟 오브젝트의 위치로 설정

        StartCoroutine(ChaseLoop());
    }

    public void Exit()
    {
        monsterStateMachine.ChangeState(MonsterBaseState.MonsterState.Attack); //상태를 Attack으로 변경
        #if DEBUG
        Debug.Log("어택 상태로 진입");
        #endif
    }

    public IEnumerator ChaseLoop()
    {
        while (true)
        {
            var target = monsterStateMachine.monsterContext.Target;
            float distance = Vector3.Distance(target.transform.position, this.gameObject.transform.position);
            if (distance < _range) //타겟이 사거리 안에 들어오면
            {
                Exit(); //상태변화
                yield break;
            }
            _monsterMovement.MonsterMoveToTarget(target.transform); //MonsterMovement를 이용하여 몬스터가 타겟에게 이동하도록 설정
            yield return null;
        }

    }
    
    public void Update()
    {
        
    }
}

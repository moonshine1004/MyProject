# Unity2D DunGeon Game
> 본 프로젝트는 2D 던전게임과 실시간 카드 게임을 합친다는 컨셉을 바탕으로, 플레이어가 덱을 구성하여 실시간 전투를 통해 몰입할 수 있도록 제작되었습니다.

## 프로젝트 개요
- **플랫폼**: Windows (PC)
- **엔진**: Unity 6 (URP)
- **개발 기간**: 2025.05 ~ 진행 중
- **개발 도구**: C#
- **버전 관리**: Git, GitHub
- **데이터 관리**: Unity ScriptableObject, CSV, Excel
- **개발 인원**: 개인 프로젝트

## 구현상세 
#### 1. **카드 시스템 구현**
- **아키텍처 패턴**: 
- **데이터 테이블 파싱**: 데이터 테이블은 게임 내 수치의 반복적인 수정과 방대한 양의 데이터를 다룸에 있어 이점을 가지므로, 기획자가 제작한 데이터 테이블을 CSV 데이터로 변환하여 스크립터블 오브젝트에 넣을 수 있도록 코드를 작성했습니다.
- **스크립터블 오브젝트**
 - 유니티의 스크립터블 오브젝트는 구조가 같은 대량의 서로 다른 오브젝트를 다루는데에 용이한 유니티의 **데이터 컨테이너**로, 복수의 데이터를 빠르게 에셋으로 만드어 저장하여 사용할 수 있다는 이점이 있습니다. 이에 본 프로젝트에서는 이를 이용하여 프로젝트의 핵심 시스템인 '카드 시스템'에 필요한 카드를 간편하고 빠르게 제작했습니다.
 - MonoBehavior 오브젝트와의 차이점
  -  MonoBehavior: MonoBehavior 클래스를 상속받는 스크립트는 기본적으로 유니티의 GameObject와 Transform 클래스를 상속받게 되어 단순 컨테이너를 구현할 때에는 메모리 공간 효율에 있어 단점을 갖습니다.
  -  C#:
  -  ScriptableObject: ScriptableObject는 유니티 내에서 **에셋**으로 저장되어 오브젝트 생성 없이도 작동하여 여러 씬에서 데이터에 접근할 수 있다는 이점을 가집니다.
- **카드 덱 순환 시스템**: 카드 덱은 사용 전, 후 카드 리스트와 각 키에 할당되는 사용 중 배열로 구성하여, 이를 유니티의 랜덤 매서드를 사용하여 구현하였습니다.
#### 2. 오브젝트 풀링을 통한 최적화
- **힙 메모리 효율성**: Instantiate는 호출과정에서 힙에 메모리를 할당하는데, 이때 오브젝트 풀링을 통해 필요한 메모리를 미리 할당함으로서 낭비되는 메모리 공간을 줄일 수 있도록 구현했습니다.
- **Garbage Collection 대상을 줄임**: Garbage Collection은 더이상 사용하지 않는 객체를 자동으로 수거하여 메모리 공간에서 삭제하는 시스템으로, 객체를 삭제하는 동안 게임은 멈추기 때문에 오브젝트 풀링을 통해서 가비지 컬렉터의 실행을 줄임으로서 런타임 중지를 방지했습니다.
#### 3. 캐릭터
- **유니티 이벤트 시스템**: 스킬 사용 키 입력 시 발생하는 동작들을 손쉽게 관리하기 위하여 유니티 이벤트를 사용하였습니다.
#### 4. 몬스터 인공지능
  -**FSM**: FSM은 어쩌구 저쩌구
   -구현: 'MonsterBaseState'를 베이스로 하여, 몬스터의 상태를 'MonsterIdleState', 'MonsterChaseState', 'MonsterAttackState', 'MonsterBackState'로 나누어 'MonsterStateMachine'을 통해 전이함수를 구현하였습니다.
```mermaid
classDiagram
direction TB
    class MonsterStateMachine {
	    # currentState: MonsterBaseState.MonsterState
	    # monsterContext: MonsterContext
	    - _state: Dictionary[*]
	    # ChangeState(MonsterBaseState.MonsterState) void
    }

    class MonsterIdle {
	    -OnCollisionEnter2D(CapsuleCollider2D) void
    }

    class MonsterChase {
	    - _range: int
	    - _targetPosition: Vector3
	    - _monsterMovement: MonsterMovement
    }

    class IState {
	    + Enter() void
	    + Update() void
	    + Exit() void
    }

    class MonsterMovement {
	    - _speed: float
	    - _isMoving: bool
	    - MonsterMoving(Transform) void
    }

	<<Interface>> IState

    IState <|.. MonsterIdle
    IState <|.. MonsterChase
    MonsterStateMachine  <|--  MonsterIdle
    MonsterStateMachine <|-- MonsterChase
    MonsterMovement <-- MonsterChase
```
## 기술적 포인트 & 문제해결
- 카드 덱 순환 시스템을 구현하는 과정에서 배열 초기화 순서에 의해 코드에서 오류가 발생하는 것을 통해서 하나의 스타트문을 중심으로 
## 향후 개발 로드맵
- 




## Contact Me
- eMail: moonshine1004@naver.com

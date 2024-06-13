using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Spine.Unity;
using UnityEngine;

public class PlayerSystem : Entity
{
    public static PlayerSystem playerSystem;

    //X값 오프셋
    const float OffSetX_value = 1.3f;

    //움직임 속도
    const float MiddleMoveSpeed = 100;

    public enum E_AniType
    {
        Running,
        Fly,
        Kick,
        Tail_Attack,
        Fire_Attack,
        Hold_Attack,
        Hit,
    }

    public enum E_AttackState
    {
        None,
        Attack,
        Hold,
        Attack_Re//다시 공격 가능 상태
    }

    //enum 순서를 Middle , UP으로 변경 
    public enum E_AttackPoint
    {
        None = -1,
        Down,
        Middle,
        Up,
    }

    [SerializeField] SkeletonAnimation M_SkeletonAnimation;

    //피격박스 사이즈
    List<Vector3> BoxSize = new List<Vector3>()
    {
        new Vector3(1, 1, 1),
        new Vector3(1.5f, 1.5f, 1),
        new Vector3(1,1,1),
        new Vector3(1,1,1)
    };

    //위치
    private List<Vector3> Tr_AttackVector = new List<Vector3>()
    {
        new Vector3(-7, -3.5f, 0),
        new Vector3(-7, 0, 0),
        new Vector3(-7, 3.5f, 0)
    };

    //공격 상태
    E_AttackState AttackState = E_AttackState.None;

    //애니메이션 리스트
    List<string> L_AniStr = new List<string>()
    {
        "Running",
        "fly",
        "Kick",
        "tail attack",
        "fire attack",
        "tail attack2",
        "retire",
    };

    //현재 공격 위치
    E_AttackPoint AttackPoint = E_AttackPoint.Down;

    //공격 카운트 수정
    float AttackDelay = 2f;

    //현재 공격 횟수
    int AttackCount = 0;

    //내려가기 딜레이
    float DownDelay = 1f;

    //홀딩 딜레이
    float HoldDelay = 0.2f;

    private void Awake()
    {
        playerSystem = this;
    }

    private void Start()
    {
        SetAni(E_AniType.Running);
        UI_Play.Instance.ActivatPanel(true);
    }

    private void Update()
    {
        AttackDelay -= Time.deltaTime;
        DownDelay -= Time.deltaTime;
        HoldDelay -= Time.deltaTime;
        if (Input.GetKey(KeyCode.F) && CheckAttackState())
        {
            var idx = SetAttack_Idx(E_AttackPoint.Up, E_AttackPoint.Down);
            AttackState = SetAttack(idx);

            if (HoldDelay <= 0)
            {
                SetAni(AttackState == E_AttackState.None ? E_AniType.Fly : E_AniType.Fire_Attack);
            }
        }

        if (Input.GetKey(KeyCode.J) && CheckAttackState())
        {
            var idx = SetAttack_Idx(E_AttackPoint.Down, E_AttackPoint.Up);
            AttackState = SetAttack(idx);

            if (HoldDelay <= 0)
            {
                var type = AttackCount > 1 ? E_AniType.Tail_Attack : E_AniType.Kick;
                SetAni(AttackState == E_AttackState.Hold ? E_AniType.Hold_Attack : type);
            }
        }
        if (AttackState == E_AttackState.Hold)
        {
            HoldDelay = 0.2f;
        }

        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.J))
        {
            Reset();
        }
        Move();
    }

    //상태 비교 체크
    public bool GetAttackState(E_AttackState state)
    {
        return AttackState == state;
    }

    //공격 가능 상태 체크
    bool CheckAttackState()
    {
        return AttackState == E_AttackState.Hold || AttackState == E_AttackState.Attack_Re; ;
    }

    //공격 상태 변경
    E_AttackPoint SetAttack_Idx(E_AttackPoint nextpoint, E_AttackPoint checkpoint)
    {
        AttackCount++;
        if (CheckAttackPoin(checkpoint) || AttackDelay <= 0)
        {
            AttackCount = 0;
        }
        AttackDelay = 2;
        DownDelay = 1;
        // 검사해야할 조건을추가하여 다음 위치로 가게 만듬 
        if (CheckAttackPoin(E_AttackPoint.Middle))
        {
            return E_AttackPoint.Middle;
        }
        SetDirectMoveIdx(nextpoint);
        return nextpoint;
    }


    //상태 비교 체크
    public bool CheckAttackPoin(E_AttackPoint state)
    {
        return AttackPoint == state;
    }

    //공격 위치 셋팅
    public void SetDirectMoveIdx(E_AttackPoint idx)
    {
        AttackPoint = idx;
    }

    //리셋
    void Reset()
    {
        DownDelay = 1;
        AttackState = E_AttackState.Attack_Re;
    }

    //움직임 함수
    void Move()
    {
        if (DownDelay <= 0)
        {
            DownDelay = 1;
            SetDirectMoveIdx(E_AttackPoint.Down);
            SetAni(E_AniType.Running);
        }

        // 목표 위치 가져오기
        var targetPos = Tr_AttackVector[(int)AttackPoint];
        var targetY = targetPos.y;
        var targetZ = targetPos.z;

        // 현재 위치 가져오기
        var currentPosition = transform.position;

        // 새로운 위치 계산 (x는 고정)
        var newPos = new Vector3(currentPosition.x, Mathf.Lerp(currentPosition.y, targetY, Time.deltaTime * MiddleMoveSpeed), Mathf.Lerp(currentPosition.z, targetZ, Time.deltaTime * MiddleMoveSpeed));

        // 이동
        transform.position = newPos;

        // 목표 위치에 거의 도달하면 루프 종료
        if (Mathf.Abs(currentPosition.y - targetY) < 0.01f && Mathf.Abs(currentPosition.z - targetZ) < 0.01f)
        {
            transform.position = new Vector3(currentPosition.x, targetY, targetZ);
        }
    }

    //공격 함수
    E_AttackState SetAttack(E_AttackPoint idx)
    {
        var result_hit = SetHit((int)idx);

        var col = result_hit.Item1;
        var perfect = result_hit.Item2;

        //허공에 공격   
        if (col == null)
        {
            return E_AttackState.None;
        }

        foreach (var item in col)
        {
            //몬스터 일때 처리
            var monster = item.GetComponent<Monster>();
            var result = SetMonster(monster, perfect);

            if (result)
            {
                return E_AttackState.Attack;
            }

            //롱 노트 일때 처리
            var longnote = item.GetComponent<LongNote>();
            result = SetLongNote(longnote, perfect);

            if (result)
            {
                return E_AttackState.Hold;
            }

            //보스일때 처리
            var boss = item.GetComponent<Boss>();
            result = SetBoss(boss, perfect);

            if (result)
            {
                return E_AttackState.Attack;
            }

            return E_AttackState.None;
        }

        return E_AttackState.None;
    }

    //히트 판정
    (Collider2D[], ScoreManager.E_ScoreState) SetHit(int idx)
    {
        //퍼펙트 체크
        var col = Physics2D.OverlapBoxAll(Tr_AttackVector[idx], BoxSize[(int)ScoreManager.E_ScoreState.Perfect], default);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Perfect);
        }

        //그레이트 체크
        col = Physics2D.OverlapBoxAll(Tr_AttackVector[idx], BoxSize[(int)ScoreManager.E_ScoreState.Great], default);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Great);
        }

        //얼리 체크
        var earlypos = Tr_AttackVector[idx];
        earlypos.x += OffSetX_value;
        col = Physics2D.OverlapBoxAll(earlypos, BoxSize[(int)ScoreManager.E_ScoreState.Early], default);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Early);
        }

        //늦음 체크
        var latepos = Tr_AttackVector[idx];
        latepos.x += -OffSetX_value;
        col = Physics2D.OverlapBoxAll(latepos, BoxSize[(int)ScoreManager.E_ScoreState.Late], default);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Late);
        }

        return (null, ScoreManager.E_ScoreState.Miss);
    }

    //그림
    private void OnDrawGizmos()
    {
        if (Tr_AttackVector == null || BoxSize == null) return;

        for (int i = 0; i < Tr_AttackVector.Count; i++)
        {
            if (Tr_AttackVector[i] == null)
            {
                return;
            }
            DrawOverlapBox(Tr_AttackVector[i], BoxSize[(int)ScoreManager.E_ScoreState.Perfect], Color.green);
            DrawOverlapBox(Tr_AttackVector[i], BoxSize[(int)ScoreManager.E_ScoreState.Great], Color.blue);

            var earlypos = Tr_AttackVector[i];
            earlypos.x += OffSetX_value;
            DrawOverlapBox(earlypos, BoxSize[(int)ScoreManager.E_ScoreState.Early], Color.yellow);

            var latepos = Tr_AttackVector[i];
            latepos.x += -OffSetX_value;
            DrawOverlapBox(latepos, BoxSize[(int)ScoreManager.E_ScoreState.Late], Color.red);
        }
    }
    void DrawOverlapBox(Vector2 position, Vector2 size, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(position, size);
    }

    //몬스터 공격 세팅
    bool SetMonster(Monster monster, ScoreManager.E_ScoreState perfect)
    {
        if (monster == null)
        {
            return false;
        }
        monster.SetHit(perfect);
        return true;
    }

    //보스 공격 셋팅
    bool SetBoss(Boss boss, ScoreManager.E_ScoreState perfect)
    {
        if (boss == null)
        {
            return false;
        }
        boss.SetHit(perfect);
        return true;
    }

    //롱노트 셋팅
    bool SetLongNote(LongNote longnote, ScoreManager.E_ScoreState perfect)
    {
        if (longnote == null)
        {
            return false;
        }
        longnote.SetAttack();
        return true;
    }

    //애니메이션 셋팅
    public void SetAni(E_AniType name)
    {
        M_SkeletonAnimation.SetAni(L_AniStr[(int)name], name == E_AniType.Fly || name == E_AniType.Running);
    }

    //공격 성공 시 애니메이션
    public static void SetPlayerAni(E_AniType name)
    {
        playerSystem.SetAni(name);
    }
}
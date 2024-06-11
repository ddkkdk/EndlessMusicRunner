using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;

public class PlayerSystem : Entity
{
    [SerializeField] SkeletonAnimation M_SkeletonAnimation;
    [SerializeField] Transform[] BoxCol;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3[] BoxSize;
    [SerializeField] Transform[] Tr_Pos;
    [SerializeField] float Speed;
    [SerializeField] float EarlyX;
    [SerializeField] float LateX;
    public static PlayerSystem playerSystem;
    const float OffSetX_value = 1.3f;

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

    public enum E_AttackPoint
    {
        Down,
        Up,
        Middle
    }

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

    int AttackCount = 0;
    public E_AttackState AttackState = E_AttackState.None;
    E_AttackPoint MoveIdx = 0;
    int CheckAttackF_J = 0;
    float Delay = 2f;
    float DownDelay = 1f;
    float HoldDelay = 0.2f;

    private void Awake()
    {
        playerSystem = this;
    }

    private void Start()
    {
        SetAni(E_AniType.Running);
        UIManager.Instance.ActivatPanel(true);
    }

    private void Update()
    {
        Delay -= Time.deltaTime;
        DownDelay -= Time.deltaTime;
        HoldDelay -= Time.deltaTime;
        if (Input.GetKey(KeyCode.F) && CheckAttackState())
        {
            SetAttack_Idx(1, 0);
            AttackState = SetAttack(1);

            if (HoldDelay <= 0)
            {
                SetAni(AttackState == E_AttackState.None ? E_AniType.Fly : E_AniType.Fire_Attack);
            }
        }
        if (Input.GetKey(KeyCode.J) && CheckAttackState())
        {
            SetAttack_Idx(0, 1);
            AttackState = SetAttack(0);

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

    bool CheckAttackState()
    {
        return AttackState == E_AttackState.Hold || AttackState == E_AttackState.Attack_Re; ;
    }

    void SetAttack_Idx(int idx, int checkidx)
    {
        AttackCount++;
        if (CheckAttackF_J == checkidx || Delay <= 0)
        {
            AttackCount = 0;
        }
        CheckAttackF_J = idx;
        Delay = 2;
        DownDelay = 1;
        if (MoveIdx != E_AttackPoint.Middle)
        {
            SetDirectMoveIdx((E_AttackPoint)idx);
        }
    }

    public void SetDirectMoveIdx(E_AttackPoint idx)
    {
        MoveIdx = idx;
    }

    void Reset()
    {
        DownDelay = 1;
        AttackState = E_AttackState.Attack_Re;
    }

    void Move()
    {
        if (DownDelay <= 0)
        {
            DownDelay = 1;
            SetDirectMoveIdx(E_AttackPoint.Down);
            SetAni(E_AniType.Running);
        }

        // 목표 위치 가져오기
        var targetPos = Tr_Pos[(int)MoveIdx];
        var targetY = targetPos.position.y;
        var targetZ = targetPos.position.z;

        // 현재 위치 가져오기
        var currentPosition = transform.position;

        // 새로운 위치 계산 (x는 고정)
        var newPos = new Vector3(currentPosition.x, Mathf.Lerp(currentPosition.y, targetY, Time.deltaTime * Speed), Mathf.Lerp(currentPosition.z, targetZ, Time.deltaTime * Speed));

        // 이동
        transform.position = newPos;

        // 목표 위치에 거의 도달하면 루프 종료
        if (Mathf.Abs(currentPosition.y - targetY) < 0.01f && Mathf.Abs(currentPosition.z - targetZ) < 0.01f)
        {
            transform.position = new Vector3(currentPosition.x, targetY, targetZ);
        }
    }

    //Attack
    E_AttackState SetAttack(int idx)
    {
        var result_hit = SetHit(idx);

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


    (Collider2D[], ScoreManager.E_ScoreState) SetHit(int idx)
    {
        var col = Physics2D.OverlapBoxAll(BoxCol[idx].position, BoxSize[(int)ScoreManager.E_ScoreState.Perfect], default, layerMask);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Perfect);
        }

        col = Physics2D.OverlapBoxAll(BoxCol[idx].position, BoxSize[(int)ScoreManager.E_ScoreState.Great], default, layerMask);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Great);
        }

        var earlypos = BoxCol[idx].position;
        earlypos.x += OffSetX_value;
        col = Physics2D.OverlapBoxAll(earlypos, BoxSize[(int)ScoreManager.E_ScoreState.Early], default, layerMask);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Early);
        }


        var latepos = BoxCol[idx].position;
        latepos.x += -OffSetX_value;
        col = Physics2D.OverlapBoxAll(latepos, BoxSize[(int)ScoreManager.E_ScoreState.Late], default, layerMask);

        if (col != null && col.Length > 0)
        {
            return (col, ScoreManager.E_ScoreState.Late);
        }

        return (null, ScoreManager.E_ScoreState.Miss);
    }

    private void OnDrawGizmos()
    {
        if (BoxCol == null || BoxSize == null) return;

        for (int i = 0; i < BoxCol.Length; i++)
        {
            DrawOverlapBox(BoxCol[i].position, BoxSize[(int)ScoreManager.E_ScoreState.Perfect], Color.green);
            DrawOverlapBox(BoxCol[i].position, BoxSize[(int)ScoreManager.E_ScoreState.Great], Color.blue);

            var earlypos = BoxCol[i].position;
            earlypos.x += OffSetX_value;
            DrawOverlapBox(earlypos, BoxSize[(int)ScoreManager.E_ScoreState.Early], Color.yellow);

            var latepos = BoxCol[i].position;
            latepos.x += -OffSetX_value;
            DrawOverlapBox(latepos, BoxSize[(int)ScoreManager.E_ScoreState.Late], Color.red);
        }
    }

    void DrawOverlapBox(Vector2 position, Vector2 size, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(position, size);
    }


    bool SetMonster(Monster monster, ScoreManager.E_ScoreState perfect)
    {
        if (monster == null)
        {
            return false;
        }
        monster.SetHit(perfect);
        return true;
    }

    bool SetBoss(Boss boss, ScoreManager.E_ScoreState perfect)
    {
        if (boss == null)
        {
            return false;
        }
        boss.SetHit(perfect);
        return true;
    }

    bool SetLongNote(LongNote longnote, ScoreManager.E_ScoreState perfect)
    {
        if (longnote == null)
        {
            return false;
        }
        longnote.SetAttack();
        return true;
    }

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
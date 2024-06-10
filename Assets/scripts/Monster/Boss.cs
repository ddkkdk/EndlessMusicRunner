using Spine.Unity;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int AttackIdx = 0;
    public int speed;
    public int Damage;
    public int monsterMaxHp;
    public GameObject[] G_Pattern;


    public enum E_State
    {
        Hit,
        Attack,
        Wait,
        PlayerMove,
    }

    E_State e_State = E_State.Wait;
    float Delay = 5;
    float Cur_Delay = 0;

    public static Vector3 CreatePos;


    public SkeletonAnimation bossAnimation;
    public static void Create()
    {
        var load = Resources.Load<GameObject>("Boss");
        var boss = Instantiate<GameObject>(load);
        boss.transform.position = CreatePos;

    }
    private void Start()
    {
        if (bossAnimation == null)
            bossAnimation = GetComponent<SkeletonAnimation>();

        // GameManager.instance.PlayMonsterAnimation(bossAnimation, "Hit");
    }
    private void Update()
    {
        switch (e_State)
        {
            case E_State.Hit:
                Hit();
                break;
            case E_State.Attack:
                Pattern();
                break;
            case E_State.Wait:
                Wait();
                break;
            case E_State.PlayerMove:
                CrushPlayer();
                break;
        }
    }

    //패턴 공격
    void Pattern()
    {
        //공격 패턴 실행
        /* for(int i =0;i<G_Pattern.Length;++i)
         {
             GameObject spawnPoint = GameObject.Find("SpawnPoint_3");
             var obj = Instantiate(G_Pattern[AttackIdx], spawnPoint.transform.position,Quaternion.identity);
             Destroy(obj, 10);
             AttackIdx++;
         }*/
        StartCoroutine(spawnObjects());
        e_State = E_State.Wait;
        AttackIdx = G_Pattern.Length;
        if (AttackIdx >= G_Pattern.Length)
        {
            //돌격 패턴
            AttackIdx = 0;
            e_State = E_State.PlayerMove;
            return;
        }
    }

    //대기 상태
    void Wait()
    {
        Cur_Delay += Time.deltaTime;

        //일정 시간 뒤 공격
        if (Cur_Delay >= Delay)
        {
            e_State = E_State.Attack;
            Cur_Delay = 0;
            return;
        }
    }

    //플레이어에게 돌격
    void CrushPlayer()
    {
        var direction = GameManager.instance.player.transform.position;
        var playerPosition = direction;
        direction.y =transform.position.y;
        playerPosition.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        var dis = Vector3.Distance(playerPosition, transform.position);
        //Debug.Log(dis);
        //만약 플레이어가 공격 상태라면?
        var hitObject = GameObject.Find("AttackPoint_Down");
        var col = GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>();
        var attackstate = col.enabled;

      
        
        //몬스터가 공격받는 상태
        if (attackstate)
        {
            var hitPosition = hitObject.transform.position;
            float distance = Vector3.Distance(hitPosition, transform.position);
            //Debug.Log(distance);
            if (distance <= 1.9f && distance >=1.6f)
            {
                Debug.Log(distance);
                Debug.Log("Perfect");
                var scripts = hitObject.GetComponent<HitCollisionDetection>();
                scripts.DrawEffect(gameObject, true);
                e_State = E_State.Hit;
                //보스가 공격 받는거 추가
                monsterMaxHp--;
                if (monsterMaxHp <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("몬스터 죽음");
                    SecenManager.LoadScene("UIScene");
                }
                UIManager.Instance.ComboScoreUpdater();
                UIManager.Instance.ScoreUpdater();
                e_State = E_State.Hit;
                return;
            }
            else if(distance>=1.91 && distance<=2.0f)
            {
                Debug.Log(distance);
                Debug.Log("Great");
                e_State = E_State.Hit;
                var scripts = hitObject.GetComponent<HitCollisionDetection>();
                scripts.DrawEffect(gameObject, false);
                //보스가 공격 받는거 추가
                monsterMaxHp--;
                if (monsterMaxHp <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("몬스터 죽음");
                    SecenManager.LoadScene("UIScene");
                }
                UIManager.Instance.ComboScoreUpdater();
                UIManager.Instance.ScoreUpdater();
                e_State = E_State.Hit;
                return;
            }
        }

        if (dis <= 1f)
        {
            
            //유저 공격 받는 코드 추가
            GameManager.instance.player.Damage(Damage);
            UIManager.Instance.ResetComboScoreUpdater();
            e_State = E_State.Hit;
        }
    }

    void Hit()
    {


        transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.bossWaitPosition.position, speed * Time.deltaTime);
        var dis = Vector3.Distance(transform.position, GameManager.instance.bossWaitPosition.position);

        if (dis <= 0.1f)
        {
            e_State = E_State.Wait;
            return;
        }
    }

    IEnumerator spawnObjects()
    {

        for (int i = 0; i < G_Pattern.Length; ++i)
        {
            yield return new WaitForSeconds(0.5f);


            GameObject spawnPoint = GameObject.Find("SpawnPoint_2");
            // GameObject sPoint = GameObject.Find("SpawnPoint_3");
            //sPoint.SetActive(false);
            var obj = Instantiate(G_Pattern[AttackIdx], spawnPoint.transform.position, Quaternion.identity);
            print("제거" + " / " + obj.name);
            Destroy(obj, 10);
            AttackIdx++;
        }


    }
    public void BossDamaged(int damage)
    {
        monsterMaxHp -= damage;
    }
}
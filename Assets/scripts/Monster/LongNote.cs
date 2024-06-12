using Spine.Unity;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class LongNote : MonoBehaviour
{
    [SerializeField] Transform Tr;
    [SerializeField] GameObject G_Effect;
    [SerializeField] GameObject G_End;
    [SerializeField] Transform start_1;
    [SerializeField] Transform start_2;
    [SerializeField] GameObject lowecollision;

    public SpriteRenderer[] myNoteSprite;
    public SpriteRenderer myLongSprrite;
    [SerializeField] bool Change;
    [SerializeField] Sprite[] noteSprites;
    [SerializeField] Sprite[] longSprites;
    [SerializeField] Vector3 BoxSize;
    [SerializeField] LayerMask layerMask;

    [SerializeField] float Star_X;
    [SerializeField] float Scale_X;

    GameObject Effect;
    float Dealy = 0f;

    private float GetScoreTime = 0f;

    public int AttackHold = 0;
    public static void Create(string folderName, string name, Transform CreatePos, int speed)
    {
        string path = $"{folderName}/{name}";
        var load = Resources.Load<GameObject>(path);
        var note = Instantiate<GameObject>(load);
        note.transform.position = CreatePos.position;

    }
    private void Start()
    {
        if (Change)
        {
            var type = UI_Lobby.Type == false ? 0 : 1;
            // 이미지 변경 추가
            for (int i = 0; i < myNoteSprite.Length; ++i)
            {
                var idx = (int)PlayerSkinType.Count;
                int spriteIndex = (int)UI_Lobby.playerSkinType % idx;
                if (type == 1)
                    spriteIndex += (int)PlayerSkinType.Count;
                
                myNoteSprite[i].sprite = noteSprites[spriteIndex];
                //사이즈가 1로나와서 0.5로 수동으로 조절
                myNoteSprite[i].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            myLongSprrite.sprite = longSprites[type];
        }
    }

    private void Update()
    {
        SetCheck();
    }

    void SetCheck()
    {
        if (AttackHold == 0 || AttackHold == 2)
        {
            // 판정 도중에 멈추거나 실패했을때 
            var targetPosition = GameManager.instance.player.transform.position;
            if (transform.position.x <= targetPosition.x)
            {
                GameManager.instance.player.SetHP(-5);
                ScoreManager.instance.SetBestCombo_Reset();
                Destroy(gameObject);
            }
            return;
        }

        var player = GameManager.instance.player;
        print(player.AttackState);
        if (player.AttackState == PlayerSystem.E_AttackState.Hold)
        {
            //점수추가
            if (GetScoreTime + 0.1f <= Time.time)
            {
                GetScoreTime = Time.time;
                var score = 1;
                Debug.Log(score);
                ScoreManager.instance.SetCurrentScore(score);
            }
            return;
        }
        AttackHold = 2;
        if (Effect)
        {
            Destroy(Effect);
        }
    }

    public void SetAttack()
    {
        if (AttackHold == 0)
        {
            AttackHold = 1;
            ScoreManager.instance.SetCombo_Add(); // 콤보추가
            return;
        }

        if (AttackHold == 2)
        {
            return;
        }

        if (Effect == null)
        {
            var createposr = GameManager.instance.lowerAttackPoint.transform.position;
            Effect = Instantiate(G_Effect, createposr, default, null);
        }

        var scale = Tr.localScale;
        scale.x -= Scale_X;

        var pos = myNoteSprite[1].transform.position;
        pos.x += Star_X;
        myNoteSprite[1].transform.position = pos;

        Tr.localScale = scale;

        Dealy -= Time.deltaTime;

        if (Dealy > 0)
        {
            AudioManager.instance.PlaySound();
            Dealy = 0.1f;
            AttackHold = 1;
        }



        if (Tr.localScale.x > 0)
        {
            return;
        }

        ScoreManager.instance.SetCombo_Add(); // 콤보추가
        Destroy(this.gameObject);
        Destroy(Effect);
        var createpos = GameManager.instance.skeleton.transform.position;
        createpos.x += 1;
        createpos.y = 0;
        var end = Instantiate(G_End, createpos, default, null);
        Destroy(end, 1f);

    }
}
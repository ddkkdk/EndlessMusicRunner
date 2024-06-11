using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDesign
{
    public int[] MID;
    public int[] MonsterInfo; // -> MonsterTable 코드로 변경해야함.
    public int[] MonsterSpwanCount;
    public float[] CoolTime;
    public int[] Spwan_Position;


    public Dictionary<int,List<C_LevelDesign>> Init()
    {
        Dictionary<int, List<C_LevelDesign>> datas = new Dictionary<int, List<C_LevelDesign>>();
        
        for(int i =0;i< MID.Length;++i)
        {
            var levelDesign = new C_LevelDesign(MonsterInfo[i], MonsterSpwanCount[i],
                CoolTime[i], Spwan_Position[i]);

            if (!datas.ContainsKey(MID[i]))
            {
                List<C_LevelDesign> C_LevelDesignList = new();
                C_LevelDesignList.Add(levelDesign);
                datas.Add(MID[i], C_LevelDesignList);
            }
            else
            {
                datas[MID[i]].Add(levelDesign);
            }
        }

        return datas;
    }
}
public class C_LevelDesign
{
    public int MonsterInfo;
    public int MonsterSpwanCount;
    public float CoolTime;
    public int Spwan_Position;

    public C_LevelDesign(int monsterInfo, int monsterSpwanCount, float coolTime, int spwan_Position)
    {
        MonsterInfo = monsterInfo;
        MonsterSpwanCount = monsterSpwanCount;
        CoolTime = coolTime;
        Spwan_Position = spwan_Position;
    }
}
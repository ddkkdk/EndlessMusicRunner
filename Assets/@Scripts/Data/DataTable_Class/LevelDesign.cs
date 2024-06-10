using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDesign
{
    public int[] M_ID;
    public int[] Stage_Info;
    public int[] MonsterInfo; // -> MonsterTable 코드로 변경해야함.
    public float Spwan_CoolTime;
    public float Spwan_PositionX;
    public float Spwan_PositionY;

    public Dictionary<int,C_LevelDesign> Init()
    {
        Dictionary<int,C_LevelDesign> datas = new Dictionary<int, C_LevelDesign>();
        
        for(int i =0;i<M_ID.Length;++i)
        {

        }

        return datas;
    }
}
public class C_LevelDesign
{

}
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public class Data : MonoBehaviour
    {

        #region Data / Dataset Scritpt
        public static DataSet M_DataSet
        {
            get
            {
                if (_DataSet == null)
                {
                    _DataSet = GameManager.instance.GetComponent<DataSet>();
                }

                return _DataSet;
            }
        }
        static DataSet _DataSet;
        #endregion


        #region 레벨디자인 테이블 (LevelDesigin Table)
        static Dictionary<int, List<C_LevelDesign>> _LevelDesign;
        public static Dictionary<int, List<C_LevelDesign>> LevelDesigin
        {
            get
            {
                if (_LevelDesign == null)
                {
                    var datas = ReadData_Sync<LevelDesign>("/LevelTable");
                    _LevelDesign = datas.Init();

                }
                return _LevelDesign;
            }
        }
        #endregion

        #region MonsterTable
        static Dictionary<int, C_MonsterTable> _MonsterTable;
        public static Dictionary<int, C_MonsterTable> MonsterTable
        {
            get
            {
                if(_MonsterTable == null)
                {
                    var datas = ReadData_Sync<MonsterTable>("/MonsterTable");
                    _MonsterTable = datas.Init();
                }
                return _MonsterTable;
            }
        }
        #endregion



        public static T ReadData_Sync<T>(string filename)
        {
            var data = M_DataSet.ReadData_Sync<T>(filename);
            return data;
        }


        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                var t = MonsterTable.ContainsKey(0);
                Debug.Log(t);
            }
        }
    }

}

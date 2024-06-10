using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public class Data : MonoBehaviour
    {
        #region 레벨디자인 테이블 (LevelDesigin Table)
        static Dictionary<int, LevelDesign> _LevelDesign;
        public static Dictionary<int, LevelDesign> LevelDesigin
        {
            get 
            { 
                return _LevelDesign; 
            }
        }
        #endregion
    }

}

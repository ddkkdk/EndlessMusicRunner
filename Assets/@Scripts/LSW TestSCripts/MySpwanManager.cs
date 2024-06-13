using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MySpwanManager : MonoBehaviour
{

    #region Change Addressables Code
    public List<string> AddressableGroups = new List<string>(); //MonsterTable�� ������ ����
    public List<GameObject> monsterOBjects=new List<GameObject>();
    public List<GameObject> bossObjects = new List<GameObject>();
    #endregion

    public Transform spwanUp;
    public Transform spwanDown;
    public void Update()
    {
        
    }
    private async void SpawnAddressablePrefab()
    {
        var t = GameData.Data.MonsterTable[0];
        string prefabAddress = monsterOBjects[t.PrefabName].name;
        var str = AddressableGroups[0] + prefabAddress;
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(prefabAddress);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefab = handle.Result;
            Instantiate(prefab, spwanDown.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"Failed to load prefab from Addressables: {prefabAddress}");
        }

        Addressables.Release(handle);
    }

    public void MonsterSpwan(int idx)
    {
        var t = GameData.Data.MonsterTable[idx];
        string prefab = string.Empty;
        if (t.monsterType == Monster_Type.Normal)
            prefab = monsterOBjects[t.PrefabName].name;
        else if (t.monsterType == Monster_Type.Boss)
            prefab = bossObjects[t.PrefabName].name;
        Monster.Create("Monster", prefab, spwanDown.position, t.MaxHp,t.Speed,t.Uniq_MonsterType);
    }

}

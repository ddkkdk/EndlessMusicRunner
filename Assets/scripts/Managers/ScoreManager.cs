using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int CurrentScore;
    public int BestScore;
    public int CurrentCombo;
    public int BestCombo;
    public Dictionary<E_ScoreState, int> D_SocreState = new Dictionary<E_ScoreState, int>();

    public enum E_ScoreState
    {
        Perfect,
        Great,
        Early,
        Late,
        Pass,
        Miss
    }
    private void Awake()
    {
        instance = this;
    }

    //최고 콤보 가져오기
    public int GetBestCombo()
    {
        return BestCombo;
    }

    //콤보 추가
    public void SetCombo_Add()
    {
        CurrentCombo++;
        UIManager.Instance.ComboScoreUpdater(CurrentCombo);

        //현재 콤보가 베스트 보다 큰지 확인
        if (CurrentCombo < BestCombo)
        {
            return;
        }
        BestCombo++;
    }

    //콤보 리셋
    public void SetBestCombo_Reset()
    {
        CurrentCombo = 0;
        UIManager.Instance.ResetComboScoreUpdater();
    }

    //점수 높이기
    public void SetCurrentScore()
    {
        CurrentScore++;
        UIManager.Instance.ScoreUpdater(CurrentScore);
    }

    //현재 점수 가져오기
    public int GetCurrentScore()
    {
        return CurrentScore;
    }

    //이전 최고 점수 가져오기
    public int GetBestScore()
    {
        return BestScore;
    }

    //최고 점수 바꾸기
    public void SetBestScore()
    {
        if (CurrentScore < BestScore)
        {
            return;
        }
        BestScore = CurrentScore;
    }

    //각 상태별 횟수 가져오기
    public int GetScoreState_Count(E_ScoreState state)
    {
        var check = D_SocreState.TryGetValue(state, out var data);
        return data;
    }

    //각 상태 카운트 추가
    public void SetScoreState(E_ScoreState state)
    {
        var check = D_SocreState.TryGetValue(state, out var data);

        if (!check)
        {
            D_SocreState.Add(state, 0);
        }

        D_SocreState[state]++;
    }

    //여태 나왔던 것들 총합
    public int GetMaxState()
    {
        var max = 0;

        foreach (var item in D_SocreState)
        {
            max += item.Value;
        }

        return max;
    }

    //미스 제외하고 총합
    public int GetAccuracy()
    {
        var count = 0;

        foreach (var item in D_SocreState)
        {
            if (item.Key == E_ScoreState.Miss)
            {
                continue;
            }
            count += item.Value;
        }

        return count;
    }
}
using UnityEngine;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] GameObject G_Pause;
    public void Btn_Pause()
    {
        G_Pause.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Btn_ReStart()
    {
        G_Pause.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Btn_Exit()
    {
        Time.timeScale = 1;
        SecenManager.LoadScene("UIScene");
    }

}


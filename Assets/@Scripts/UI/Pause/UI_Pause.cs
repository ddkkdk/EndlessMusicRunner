using UnityEngine;

public class UI_Pause : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Btn_ReStart()
    {
        Destroy(this.gameObject);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }

    public void Btn_Exit()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SecenManager.LoadScene("UIScene");
    }
}
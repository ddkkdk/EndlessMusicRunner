using TMPro;
using UnityEngine;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] GameObject G_Pause;
    [SerializeField] TextMeshProUGUI T_Type;
    public static bool Type;

    private void Start()
    {
        if (T_Type == null)
            return;
        T_Type.text = Type ? "B" : "A";
    }

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
        AudioListener.pause = false;
        SecenManager.LoadScene("UIScene");
    }

    public void Btn_A_And_B()
    {
        var text = T_Type.text;
        if (text == "A")
        {
            text = "B";
            Type = true;
        }
        else
        {
            text = "A";
            Type = false;
        }
        T_Type.text = text;
    }

}


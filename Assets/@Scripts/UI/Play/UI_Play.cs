using UnityEngine;

public class UI_Play : MonoBehaviour
{

    public async void Btn_Pause()
    {
        var name = "UI_Pause";
        await name.CreateOBJ<UI_Pause>();
    }
}
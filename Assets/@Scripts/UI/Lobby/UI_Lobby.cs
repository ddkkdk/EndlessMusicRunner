using TMPro;
using UnityEngine;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] TMP_Dropdown skinDropDown;
    [SerializeField] TextMeshProUGUI T_Type;
    public static bool Type;
    public static PlayerSkinType playerSkinType = PlayerSkinType.Skin0;

    private void Start()
    {
        if (T_Type == null)
            return;
        T_Type.text = Type ? "B" : "A";
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
    public void SetSkin()
    {
        playerSkinType = (PlayerSkinType)skinDropDown.value;
    }
}


public enum PlayerSkinType
{
    Skin0, Skin1, Skin2, Skin3,
    Skin4, Skin5, Skin6, skin7, Count
}

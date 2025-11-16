using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Stats health;
    public Stats attack;
    public Stats defense;
    public TextMeshProUGUI alltext;
    public static int Defense = 5;
    public static int Attack = 7;
    public static int Health = 100;

    void Update()
    {
        updateText();
    }
    private void updateText()
    {
        alltext.text = "Health: " + health.GetValue().ToString() + " Attack: " + attack.GetValue().ToString() + " Defense: " + defense.GetValue().ToString();

        Defense = defense.GetValue();
        Attack = attack.GetValue();
        Health = health.GetValue();
    }

}

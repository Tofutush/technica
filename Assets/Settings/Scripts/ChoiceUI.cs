using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    public static ChoiceUI Instance;
    public GameObject choicePanel;
    public Button killButton;
    public Button spareButton;

    private Enemy currentEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        choicePanel.SetActive(false);
        killButton.onClick.AddListener(() => makeChoice("kill"));
        spareButton.onClick.AddListener(() => makeChoice("spare"));
    }

    public void showChoice(Enemy enemy)
    {
        currentEnemy = enemy;
        choicePanel.SetActive(true);
    }

    public void makeChoice(string choice)
    {
        choicePanel.SetActive(false);
        if (choice == "kill")
        {
            MinigameManagerTrue.singleton.hasKilled = true;
        }
        Destroy(currentEnemy.gameObject);
    }
}

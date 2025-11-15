using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    static MinigameManager singleton;
    void Awake() { singleton = this; }

    private enum MinigameState
    {
        READY,
        PLAYING,
        SUCCESS,
        FAILURE
    }

    private MinigameState mstate = MinigameState.READY;
    private List<MinigameSubscriber> subscribers = new List<MinigameSubscriber>();

    public TextMeshPro instruct;
    
    [SerializeField]
    private TextMeshProUGUI statetext;

    [Header("You're welcome to change these fields:")]
    /*[SerializeField]
    [Tooltip("The length of the minigame timer, in seconds")]
    private float minigameLength = 65f; */

    [SerializeField]
    [Tooltip("Reference to the boss object")]
    private BossHealth boss;

    [SerializeField]
    [Tooltip("Reference to the player object")]
    private GameObject player;

    [SerializeField]
    private GameObject successPanel;

    [SerializeField]
    private GameObject failurePanel;

    public static void Subscribe(MinigameSubscriber subscriber)
    {
        singleton.subscribers.Add(subscriber);
    }

    void Start()
    {
        //CoreUI.Timer.maxValue = minigameLength;
        //CoreUI.Timer.value = minigameLength;
        mstate = MinigameState.READY;

        // Hide panels at start
        if (successPanel != null) successPanel.SetActive(false);
        if (failurePanel != null) failurePanel.SetActive(false);

        StartCoroutine(ReadyCountdown());
    }

    IEnumerator ReadyCountdown()
    {
        Time.timeScale = 0f;
        for (int i = 3; i > 0; i--)
        {
            CoreUI.CountdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        Time.timeScale = 1f;
        mstate = MinigameState.PLAYING;
        CoreUI.CountdownText.gameObject.SetActive(false);
        instruct.enabled = false;
        foreach (MinigameSubscriber s in subscribers)
            s.OnMinigameStart();
    }

    void Update()
    {
        if (mstate != MinigameState.PLAYING) return;

        // Check if player died
        if (player == null || !player.activeInHierarchy)
        {
            Debug.Log("PLAYER DIED! Setting failure state.");
            SetStateToFailure();
            EndGame();
            return; // Prevent further code from running
        }

        // Check if boss is defeated first (PRIORITY CHECK)
        if (boss != null)
        {
            Debug.Log($"Boss health check: {boss.CurrentHealth}");
            if (boss.CurrentHealth <= 0)
            {
                Debug.Log("BOSS DEFEATED! Setting success state.");
                SetStateToSuccess();
                EndGame();
                return; // Prevent further code from running
            }
        }
        else
        {
            Debug.LogWarning("Boss reference is null!");
        }

        // Decrease timer
        /*CoreUI.Timer.value -= Time.deltaTime;

        // Check for timer end
        if (CoreUI.Timer.value <= CoreUI.Timer.minValue)
        {
            Debug.Log("Timer ran out - setting failure state");
            foreach (MinigameSubscriber s in subscribers)
                s.OnTimerEnd();
            SetStateToFailure();
            EndGame();
        }*/
    }

    // Exposed functions
    public static bool IsReady()
    {
        return singleton.mstate != MinigameState.READY;
    }

    public static void SetStateToSuccess()
    {
        singleton.mstate = MinigameState.SUCCESS;
        //statetext.text = "All hail our new beloved royal highness!!! :)";
    }

    public static void SetStateToFailure()
    {
        singleton.mstate = MinigameState.FAILURE;
        //statetext.text = "Worthless";
    }

    public static void EndGame()
    {
        Debug.Log("EndGame called, state: " + singleton.mstate);

        // Start the delayed shutdown coroutine
        singleton.StartCoroutine(singleton.DelayedShutdown());
    }

    IEnumerator DelayedShutdown()
    {
        MinigameManager mgr = FindFirstObjectByType<MinigameManager>();
        // Wait 3 seconds in real time (unaffected by timeScale)
        yield return new WaitForSecondsRealtime(3f);

        // Show appropriate panel
        if (mstate == MinigameState.SUCCESS)
        {
            mgr.statetext.text = "All hail our new beloved royal highness!!! :)";
            if (successPanel != null)
            {
                successPanel.SetActive(true);
                Debug.Log("Success Panel activated!");
            }
        }
        else if (mstate == MinigameState.FAILURE)
        {
            mgr.statetext.text = "Worthless";
            if (failurePanel != null)
            {
                failurePanel.SetActive(true);
                Debug.Log("Failure Panel activated!");
            }
            else if (successPanel != null)
            {
                // Fallback to using success panel with modified text
                successPanel.SetActive(true);
                var textComponent = successPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = "FAILURE!";
                }
                Debug.Log("Failure Panel activated (using success panel)!");
            }
        }

        // Stop the game
        Time.timeScale = 0f;

#if UNITY_EDITOR
        // Delay editor stop to allow final frame to render
        EditorApplication.delayCall += () => { EditorApplication.isPlaying = false; };
#else
        // Handle non-editor exit
        Application.Quit();
#endif
    }
}
using System.Collections;
using UnityEngine;
using TMPro;

public class NarratorrrDialogue : MonoBehaviour
{
    [TextArea]
    public string[] lines;                  // Narrator lines
    public float typeSpeed = 0.03f;         // Speed of typewriter
    public TextMeshProUGUI textBox;

    private int index;
    private bool isTyping = false;
    private bool lineFinished = false;

    void Start()
    {
        textBox.text = "";
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        // Click to skip / continue
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Finish the line instantly
                StopAllCoroutines();
                textBox.text = lines[index];
                isTyping = false;
                lineFinished = true;
            }
            else if (lineFinished)
            {
                NextLine();
            }
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        lineFinished = false;
        textBox.text = "";

        foreach (char c in lines[index])
        {
            textBox.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        lineFinished = true;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            textBox.text = "";   // dialogue finished
        }
    }
}


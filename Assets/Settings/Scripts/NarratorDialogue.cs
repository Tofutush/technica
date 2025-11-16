using System.Collections;
using UnityEngine;
using TMPro;

public class NarratorDialogue : MonoBehaviour
{
    [TextArea]
    public string[] lines;                  
    public float typeSpeed = 0.03f;        
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
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
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
            textBox.text = "";
        }
    }
}


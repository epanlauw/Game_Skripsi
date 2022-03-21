using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public Image npcImg;
    public bool npcMove;
    public bool endOfDialogue = false;

    public string[] dialogLines;

    public int currentLine;

    bool justStarted;
    string checkName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialogLines[currentLine];
        npcMove = true;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    currentLine++;

                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        PlayerController.instance.canMove = true;
                        npcMove = true;
                        if (checkName == "Cyclope")
                            endOfDialogue = true;
                    }
                    else
                    {
                        CheckIfName();

                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        justStarted = true;

        PlayerController.instance.canMove = false;
        npcMove = false;
    }

    public void SetSprite(string npcName)
    {
        if (npcName.StartsWith("a-"))
        {
            string tempNameNPC = npcName.Replace("a-", "");
            npcImg.sprite = Resources.Load<Sprite>("Animals/" + tempNameNPC + "/Faceset");
        }
        else
        {
            if (npcName.Contains(" "))
            {
                string tempNameNPC = npcName.Replace(" ", "");
                npcImg.sprite = Resources.Load<Sprite>("Characters/" + tempNameNPC + "/Faceset");
            }
            else
            {
                npcImg.sprite = Resources.Load<Sprite>("Characters/" + npcName + "/Faceset");
            }
        }
    }

    public void CheckIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            string tempName = dialogLines[currentLine].Replace("n-", "");
            checkName = tempName;
            nameText.text = tempName + ":";
            SetSprite(tempName);
            currentLine++;
        }

        if (dialogLines[currentLine].StartsWith("a-"))
        {
            string tempName = dialogLines[currentLine].Replace("a-", "");
            nameText.text = tempName + ":";
            SetSprite(dialogLines[currentLine]);
            currentLine++;
        }
    }
}

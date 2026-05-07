using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    // Four stage UI panels
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

    // Confirmation dialog
    public GameObject five;
    public Button nextBtn;

    // Text component inside the dialog
    private Text dialogTextUI;

    // Current stage index 0~4
    public int index = 0;

    // Tip message shown at each stage
    public string[] stepTip =
    {
        "Washing machine is running",
        "Laundry basket ready for wringing",
        "Wringing in progress",
        "Ready to hang dry",
        "Drying complete"
    };

    // Dialog message shown at each stage
    public string[] dialogText =
    {
        "Transfer laundry from washing machine to basket?",
        "Move laundry to the wringer?",
        "Stop wringing and hang to dry?",
        "Hang the laundry to dry?"
    };

    public HandleRotator handle;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Get the Text component from the first child of the dialog panel
        if (five != null)
        {
            dialogTextUI = five.transform.GetChild(0).GetComponent<Text>();
        }

        five.SetActive(false);
        nextBtn.onClick.AddListener(OnNextClick);

        Tips.AddNotice(stepTip[0]);
        UpdatePanelShow();
    }

    // Opens the dialog and sets its text to the current stage message
    public void OpenDialog()
    {
        if (index >= dialogText.Length) return;

        if (dialogTextUI != null)
        {
            dialogTextUI.text = dialogText[index];
        }

        five.SetActive(true);
    }

    // Advances to the next stage when the dialog confirm button is clicked
    void OnNextClick()
    {
        five.SetActive(false);

        index++;
        index = Mathf.Clamp(index, 0, 4);

        Tips.AddNotice(stepTip[index]);
        UpdatePanelShow();
    }

    // Shows the panel corresponding to the current stage
    void UpdatePanelShow()
    {
        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false);
        four.SetActive(false);

        switch (index)
        {
            case 0: one.SetActive(true); break;
            case 1: two.SetActive(true); break;
            case 2: three.SetActive(true); Invoke(nameof(StartRollerForward), 3f); break;
            case 3: break;
            case 4: four.SetActive(true); break;
        }
    }

    public void StartRollerForward()
    {
        handle.OnForwardBtn();
    }

    // Returns true only if the given step matches the current stage
    public bool CanClickStep(int needStep)
    {
        return index == needStep;
    }
}
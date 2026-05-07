using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

    public GameObject five;
    public Button nextBtn;

    private Text dialogTextUI;

    public int index = 0;

    private string[] stepTip =
    {
        "Washing machine is running",
        "Laundry basket ready for wringing",
        "Wringing in progress",
        "Ready to hang dry",
        "Drying complete"
    };

    private string[] dialogText =
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
        if (five != null)
        {
            dialogTextUI = five.transform.GetChild(0).GetComponent<Text>();
            five.SetActive(false);
        }

        if (nextBtn != null)
            nextBtn.onClick.AddListener(OnNextClick);

        Tips.AddNotice(stepTip[0]);
        UpdatePanelShow();
    }

    public void OpenDialog()
    {
        if (index >= dialogText.Length) return;

        if (dialogTextUI != null)
            dialogTextUI.text = dialogText[index];

        if (five != null)
            five.SetActive(true);
    }

    void OnNextClick()
    {
        if (five != null)
            five.SetActive(false);

        index++;
        index = Mathf.Clamp(index, 0, 4);

        Tips.AddNotice(stepTip[index]);
        UpdatePanelShow();
    }

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

    public bool CanClickStep(int needStep)
    {
        return index == needStep;
    }
}
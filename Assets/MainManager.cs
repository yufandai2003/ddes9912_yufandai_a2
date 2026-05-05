using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    // 四个进度界面
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

    // 对话弹窗
    public GameObject five;
    public Button nextBtn;

    // 对话框文字组件
    private Text dialogTextUI;

    // 当前进度 0~4
    public int index = 0;

    // 每步Tips提示文案 严格对应流程
    public string[] stepTip =
    {
        "洗衣机正在洗衣服",
        "洗衣篮衣物待挤压",
        "衣物正在挤压",
        "衣物待晾晒",
        "晾晒已完成"
    };

    // 每步对话框文字
    public string[] dialogText =
    {
        "是否从洗衣机取出放入洗衣篮",
        "是否取出衣物进行挤压",
        "是否停止挤压进行晾晒",
        "是否晾晒衣服"
    };
    public HandleRotator handle;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 获取对话框的文本（第一个子物体的Text）
        if (five != null)
        {
            dialogTextUI = five.transform.GetChild(0).GetComponent<Text>();
        }

        five.SetActive(false);
        nextBtn.onClick.AddListener(OnNextClick);

        // 开局第一步提示
        Tips.AddNotice(stepTip[0]);

        // 初始化界面显隐
        UpdatePanelShow();
    }

    // 打开对话框 自动赋值文字
    public void OpenDialog()
    {
        if (index >= dialogText.Length) return;

        // 设置对话框文字
        if (dialogTextUI != null)
        {
            dialogTextUI.text = dialogText[index];
        }

        five.SetActive(true);
    }

    // 对话框下一步
    void OnNextClick()
    {
        five.SetActive(false);

        index++;
        index = Mathf.Clamp(index, 0, 4);

        // 弹出当前步骤提示
        Tips.AddNotice(stepTip[index]);

        // 更新界面显隐
        UpdatePanelShow();
    }

    // 进度界面显隐控制
    void UpdatePanelShow()
    {
        one.SetActive(false);
        two.SetActive(false);
        three.SetActive(false );
        four.SetActive(false);

        switch (index)
        {
            case 0: one.SetActive(true); break;
            case 1: two.SetActive(true);  break;
            case 2: three.SetActive(true);Invoke(nameof(qqq), 3f ); break;
            case 3:  break;
            case 4: four.SetActive(true); break;
        }
    }
    public void qqq ()
    {
        handle.OnForwardBtn();
    }
    // 给外部物体调用：判断当前能不能点击
    public bool CanClickStep(int needStep)
    {
        // 只有当前index等于需要步骤 才能点，做完就不能再点
        return index == needStep;
    }
}
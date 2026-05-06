using UnityEngine;
using System.Collections.Generic;

public class ClickObject : MonoBehaviour
{
    [Header("当前物体对应步骤 0=洗衣机 1=洗衣篮 2=挤压机 3=晾衣架")]
    public int stepNum;

    // 自动找到的所有带渲染器的子物体
    private List<OutlineEffect> _outlineList = new List<OutlineEffect>();

    void Awake()
    {
        // 遍历自身 + 所有子物体、孙物体，自动找带Renderer的物体
        FindAllRenderersInChildren(transform);
    }

    // 递归遍历所有子物体，自动加轮廓
    void FindAllRenderersInChildren(Transform parent)
    {
        // 判断当前物体有没有渲染器
        Renderer render = parent.GetComponent<Renderer>();
        if (render != null)
        {
            // 有渲染器 → 自动添加/获取轮廓脚本
            OutlineEffect outline = parent.GetComponent<OutlineEffect>();
            if (outline == null)
                outline = parent.gameObject.AddComponent<OutlineEffect>();

            _outlineList.Add(outline);
        }

        // 继续遍历子物体（孙物体也会遍历）
        foreach (Transform child in parent)
        {
            FindAllRenderersInChildren(child);
        }
    }

    private void OnMouseEnter()
    {
        // 只有当前步骤可点击 → 全部高亮
        if (MainManager.instance.CanClickStep(stepNum))
        {
            foreach (var ol in _outlineList)
                ol.ShowOutline();
        }
    }

    private void OnMouseExit()
    {
        // 全部取消高亮
        foreach (var ol in _outlineList)
            ol.HideOutline();
    }

    private void OnMouseDown()
    {
        // 步骤锁定，防止重复点击
        if (!MainManager.instance.CanClickStep(stepNum))
            return;

        // 弹出对话框
        MainManager.instance.OpenDialog();
    }
}
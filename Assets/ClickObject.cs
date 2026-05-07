using UnityEngine;
using System.Collections.Generic;

public class ClickObject : MonoBehaviour
{
    [Header("Step index for this object: 0=Washing Machine 1=Laundry Basket 2=Wringer 3=Drying Rack")]
    public int stepNum;

    // All child renderers that will receive the outline effect
    private List<OutlineEffect> _outlineList = new List<OutlineEffect>();

    void Awake()
    {
        // Recursively search self and all children for Renderer components
        FindAllRenderersInChildren(transform);
    }

    // Recursively adds OutlineEffect to all objects with a Renderer
    void FindAllRenderersInChildren(Transform parent)
    {
        Renderer render = parent.GetComponent<Renderer>();
        if (render != null)
        {
            OutlineEffect outline = parent.GetComponent<OutlineEffect>();
            if (outline == null)
                outline = parent.gameObject.AddComponent<OutlineEffect>();

            _outlineList.Add(outline);
        }

        foreach (Transform child in parent)
        {
            FindAllRenderersInChildren(child);
        }
    }

    private void OnMouseEnter()
    {
        // Only highlight if this step is currently active
        if (MainManager.instance.CanClickStep(stepNum))
        {
            foreach (var ol in _outlineList)
                ol.ShowOutline();
        }
    }

    private void OnMouseExit()
    {
        foreach (var ol in _outlineList)
            ol.HideOutline();
    }

    private void OnMouseDown()
    {
        // Ignore click if this step is not active
        if (!MainManager.instance.CanClickStep(stepNum))
            return;

        MainManager.instance.OpenDialog();
    }
}
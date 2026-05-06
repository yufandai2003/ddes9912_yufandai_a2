using UnityEngine;

public class OutlineEffect : MonoBehaviour
{
    private Renderer _renderer;
    private Material _originalMaterial;
    private Material _emitMaterial;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;

        // 创建一个自发光材质（外圈柔和高亮，不覆盖模型）
        _emitMaterial = new Material(_originalMaterial);
        _emitMaterial.EnableKeyword("_EMISSION");
        _emitMaterial.SetColor("_EmissionColor", Color.cyan * 0.4f);
    }

    public void ShowOutline()
    {
        _renderer.material = _emitMaterial;
    }

    public void HideOutline()
    {
        _renderer.material = _originalMaterial;
    }
}
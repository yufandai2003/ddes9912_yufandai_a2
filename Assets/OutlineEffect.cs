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

        // Create an emissive material variant for the highlight effect
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
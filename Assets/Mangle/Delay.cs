using UnityEngine;

public class DelayedShow : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        if (target != null)
            target.SetActive(false);
        Invoke(nameof(Show), 3f);
    }

    public void Show()
    {
        if (target != null)
            target.SetActive(true);
    }
}
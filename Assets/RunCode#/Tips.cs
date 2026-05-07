using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public static Tips instance;
    public float noticeShowDuration = 3f;

    private static Queue<string> noticeQueue = new Queue<string>();
    private bool isShowingNotice = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (noticeQueue.Count > 0 && !isShowingNotice)
        {
            string noticeText = noticeQueue.Dequeue();
            StartCoroutine(ShowNotice(noticeText));
        }
    }

    public static void AddNotice(string noticeText)
    {
        noticeQueue.Enqueue(noticeText);
    }

    IEnumerator ShowNotice(string noticeText)
    {
        isShowingNotice = true;
        GameObject noticeObj = Instantiate(Resources.Load<GameObject>("Tip"), this.transform);
        Text noticeTextObj = noticeObj.transform.GetChild(0).GetComponentInChildren<Text>();
        noticeTextObj.text = noticeText;

        yield return new WaitForSeconds(noticeShowDuration);
        Destroy(noticeObj);
        isShowingNotice = false;
    }
}
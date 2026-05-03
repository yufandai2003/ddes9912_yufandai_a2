/****************************************************
    文件：Tips.cs
    作者：
    邮箱: 
    日期：2023/6/14 22:42:14
    功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    public static Tips instance;
    public float noticeShowDuration = 1f;

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
       // noticePrefab.gameObject.SetActive(true);
        //GameObject noticeObj = Instantiate(noticePrefab, noticeParent);
        GameObject noticeObj = Instantiate(Resources.Load<GameObject>("Tiop"), this.transform);
         Text noticeTextObj = noticeObj.transform.GetChild(0).GetComponentInChildren<Text>();
        noticeTextObj.text = noticeText;

        yield return new WaitForSeconds(noticeShowDuration);
        Destroy(noticeObj);
       // noticePrefab.gameObject.SetActive(false);
        isShowingNotice = false;
    }
}



///********************************************************************
///	   作者：铎铎高
///    类名称：BaseWindow
///	   类功能：UI窗口基类
///	   创建时间：2023/5/27   21:15 		
///*********************************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseWindow : MonoBehaviour
{

    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }

    protected void OnClick(GameObject go, Action<PointerEventData, object[]> clickCallBack, params object[] args)
    {
        UIListener listener = GetOrAddComponent<UIListener>(go);
        listener.onClick = clickCallBack;
        if (args != null)
        {
            listener.args = args;
        }
    }

    protected void OnClickDown(GameObject go, Action<PointerEventData, object[]> clickDownCallBack, params object[] args)
    {
        UIListener listener = GetOrAddComponent<UIListener>(go);
        listener.onClickDown = clickDownCallBack;
        if (args != null)
        {
            listener.args = args;
        }
    }

    protected void OnClickUp(GameObject go, Action<PointerEventData, object[]> clickUpCallBack, params object[] args)
    {
        UIListener listener = GetOrAddComponent<UIListener>(go);
        listener.onClickUp = clickUpCallBack;
        if (args != null)
        {
            listener.args = args;
        }
    }

    protected void OnDrag(GameObject go, Action<PointerEventData, object[]> dragCallBack, params object[] args)
    {
        UIListener listener = GetOrAddComponent<UIListener>(go);
        listener.onDrag = dragCallBack;
        if (args != null)
        {
            listener.args = args;
        }
    }
}

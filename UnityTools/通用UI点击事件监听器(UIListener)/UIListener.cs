///********************************************************************
///	   作者：DDG
///    类名称：WindowListener
///	   类功能：UI时间监听
///	   创建时间：2023/5/27   21:08 		
///*********************************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action<PointerEventData, object[]> onClick;//点击事件
    public Action<PointerEventData, object[]> onClickDown;//按下事件
    public Action<PointerEventData, object[]> onClickUp;//抬起事件
    public Action<PointerEventData, object[]> onDrag;//拖拽事件

    public object[] args = null;

    /// <summary>
    /// 当点击UI的时候会调用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData, args);
    }

    /// <summary>
    /// 当按下的时候会调用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        onClickDown?.Invoke(eventData, args);
    }

    /// <summary>
    /// 当抬起的时候会调用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        onClickUp?.Invoke(eventData, args);
    }

    /// <summary>
    /// 当拖拽的时候会调用
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData, args);
    }




}

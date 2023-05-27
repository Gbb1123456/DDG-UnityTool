///********************************************************************
///	   ���ߣ�DDG
///    �����ƣ�WindowListener
///	   �๦�ܣ�UIʱ�����
///	   ����ʱ�䣺2023/5/27   21:08 		
///*********************************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action<PointerEventData, object[]> onClick;//����¼�
    public Action<PointerEventData, object[]> onClickDown;//�����¼�
    public Action<PointerEventData, object[]> onClickUp;//̧���¼�
    public Action<PointerEventData, object[]> onDrag;//��ק�¼�

    public object[] args = null;

    /// <summary>
    /// �����UI��ʱ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData, args);
    }

    /// <summary>
    /// �����µ�ʱ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        onClickDown?.Invoke(eventData, args);
    }

    /// <summary>
    /// ��̧���ʱ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        onClickUp?.Invoke(eventData, args);
    }

    /// <summary>
    /// ����ק��ʱ������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData, args);
    }




}

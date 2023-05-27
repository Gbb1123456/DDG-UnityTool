
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartWnd : BaseWindow
{
    [SerializeField]
    Image img;

    private void Start()
    {
        OnClick(img.gameObject, ImgOnClick, img);
        OnClickDown(img.gameObject, ImgOnClickDown);
        OnClickUp(img.gameObject, ImgOnClickUp, img);
        OnDrag(img.gameObject, ImgOnDrag, img);
    }

    void ImgOnClick(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"Click点击了UI：{img.name}");
    }
    void ImgOnClickDown(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"按下点击了UI：{img.name}");
    }
    void ImgOnClickUp(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"抬起点击了UI：{img.name}");
    }

    void ImgOnDrag(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"拖拽点击了UI：{img.name}");
    }
}

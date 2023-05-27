
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
        Debug.LogError($"Click�����UI��{img.name}");
    }
    void ImgOnClickDown(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"���µ����UI��{img.name}");
    }
    void ImgOnClickUp(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"̧������UI��{img.name}");
    }

    void ImgOnDrag(PointerEventData data, object[] args)
    {
        Image img = args[0] as Image;
        Debug.LogError($"��ק�����UI��{img.name}");
    }
}

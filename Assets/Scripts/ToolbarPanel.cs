using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolbarPanel : BaseItemPanel
{
    [SerializeField] PlayerToolBarController toolBarController;

    private void Start()
    {
        Init();
        toolBarController.onChange += Highlight;
        Highlight(0);
    }

    public override void OnClick(int id, PointerEventData eventData)
    {
        toolBarController.Set(id);
        Highlight(id);
    }

    int currIndex;

    public void Highlight(int id)
    {
        slots[currIndex].Highlight(false);
        currIndex = id;
        slots[currIndex].Highlight(true);
    }
}

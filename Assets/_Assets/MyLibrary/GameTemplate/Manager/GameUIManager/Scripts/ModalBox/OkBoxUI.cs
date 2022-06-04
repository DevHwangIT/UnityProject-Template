using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OkBoxUI : ModalBoxUI
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button okButton;

    new void Awake()
    {
        base.Awake();
    } 
    
    public void Initialize(string title, string info, Action onOkButtonClick = null)
    {
        base.Initialize();
        titleText.text = title;
        infoText.text = info;
        
        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(new UnityAction(Hide));
        if (onOkButtonClick != null)
        {
            okButton.onClick.AddListener(new UnityAction(onOkButtonClick));
        }
    }

    public void AddListener(UnityAction okAction)
    {
        if (okAction != null)
            okButton.onClick.AddListener(okAction);
    }
}

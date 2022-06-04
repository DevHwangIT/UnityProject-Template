using UnityEngine;
using UnityEngine.UI;

public class NotifyBoxUI : ModalBoxUI
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    private float showDuration = 1f;
    
    new void Awake()
    {
        base.Awake();
    }
    
    public void Initialize(string title, string info, float duration)
    {
        base.Initialize();
        titleText.text = title;
        infoText.text = info;
        showDuration = duration;
    }
    
    public new void Show()
    {
        base.Show();
        Invoke(nameof(Hide), showDuration);
    }
}

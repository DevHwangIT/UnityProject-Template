using UnityEngine;
using UnityEngine.UI;

public class WaitingUI : ModalBoxUI
{
    [Header("- UI ")]
    [SerializeField] private RawImage waitImageUI;
    [SerializeField] private Text infoTextUI;

    [Header("- Variable ")]
    [SerializeField] private float waitingInterval = 0.05f;
    [SerializeField] private float waitingRotSpeed = 2f;
    [SerializeField] private int MaxDotCount = 3;
    
    private float time = 0f;
    private int DotCount = 1;
    private string infoText;

    new void Awake()
    {
        base.Awake();
        transform.SetAsLastSibling();
    }
    
    public void Initialize(string info)
    {
        base.Initialize();
        time = 0f;
        DotCount = 1;
        SetText(info);
    }
    
    public void SetText(string info)
    {
        infoText = info;
        infoTextUI.text = infoText;
    }
    
    private void Update()
    {
        waitImageUI.transform.localRotation *= Quaternion.Euler(Vector3.forward * waitingRotSpeed * Time.deltaTime);

        if (time < waitingInterval)
        {
            time += Time.deltaTime;
            return;
        }
        time = 0f;
        
        DotCount++;
        if (DotCount > MaxDotCount)
            DotCount = 1;
        infoTextUI.text = infoText;
        for (int i = 0; i < DotCount; i++)
        {
            infoTextUI.text += '.';
        }
    }
}

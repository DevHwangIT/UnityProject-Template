using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary.Utility.Sample
{
    public class DrawUI : MonoBehaviour
    {
        //UpdateUI Script
        [SerializeField] private bool isAutomatic = false;
        [SerializeField] private Text hpText;
        [SerializeField] private Text mpText;

        private void OnEnable()
        {
            if (isAutomatic)
            {
                Debug.Log("Event Enable");
                DataCenter.Instance.hp.Subscribe(UpdateUI);
                DataCenter.Instance.mp.Subscribe(UpdateUI);
            }
        }

        private void OnDisable()
        {
            if (isAutomatic)
            {
                Debug.Log("Event Disable");
                DataCenter.Instance.hp.UnSubscribe(UpdateUI);
                DataCenter.Instance.mp.UnSubscribe(UpdateUI);
            }
        }

        private void Update()
        {
            if (isAutomatic == false)
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            hpText.text = DataCenter.Instance.hp.Value.ToString();
            mpText.text = DataCenter.Instance.mp.Value.ToString();
        }
    }
}
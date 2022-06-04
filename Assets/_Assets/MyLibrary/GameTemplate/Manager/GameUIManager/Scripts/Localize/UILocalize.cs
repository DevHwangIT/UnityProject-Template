using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text)), DisallowMultipleComponent]
public class UILocalize : MonoBehaviour
{
    private Text _text;
    [SerializeField] private string _key = "";
    [SerializeField] private UnityEngine.TextAsset textData;

    private static int _keyIndex = 0;
    private static Dictionary<string, int> _keyDictionary = new Dictionary<string, int>();
    private static List<Dictionary<string, string>> _localizeData;
    private static Action OnChange;
    
    private void Awake()
    {
        LoadData();
        _text = this.GetComponent<Text>();
        OnChange += SetText;
    }
    
    public static bool SetCountry(string Country)
    {
        if (_keyDictionary.TryGetValue(Country, out _keyIndex))
        {
            OnChange?.Invoke();
            return true;
        }
        else
            return false;
    }
    public static bool SetCountry(TranslationCountries Country)
    {
        if (_keyDictionary.TryGetValue(Country.ToString(), out _keyIndex))
        {
            OnChange?.Invoke();
            return true;
        }
        else
            return false;
    }
    
    public static string Get(string key)
    {
        string value;
        if (_localizeData[_keyIndex].TryGetValue(key, out value))
            return value;
        else
            return "";
    }

    private void SetText()
    {
        _text.text = Get(_key);
    }

    private void LoadData()
    {
        if (_localizeData == null) 
            _localizeData = CSVRead();
    }
    
    private List<Dictionary<string, string>> CSVRead()
    {
        var localizeDictionaryList = new List<Dictionary<string, string>>();

        string firstLine = textData.text.Split('\n')[0];
        string[] KeyList = firstLine.Split(',');
        
        _keyDictionary.Clear();
        for (int i = 1; i < KeyList.Length; i++)
        {
            localizeDictionaryList.Add(new Dictionary<string, string>());
            _keyDictionary.Add(KeyList[i], i - 1);
        }

        string[] lineText = textData.text.Split('\n');
        for (int index = 1; index < lineText.Length; index++)
        {
            string[] localizeText = lineText[index].Split(',');
            for (int keyIndex = 1; keyIndex < localizeText.Length; keyIndex++)
            {
                localizeDictionaryList[keyIndex - 1].Add(localizeText[0], localizeText[keyIndex]);
            }
        }
        return localizeDictionaryList;
    }
}

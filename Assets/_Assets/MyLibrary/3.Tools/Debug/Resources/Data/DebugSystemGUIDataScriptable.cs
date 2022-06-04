using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SerializableClass

[System.Serializable]
public class DebugSystemImagesResources
{
    public Texture2D clearImage;
    public Texture2D collapseImage;
    public Texture2D clearOnNewSceneImage;
    public Texture2D showTimeImage;
    public Texture2D showSceneImage;
    public Texture2D userImage;
    public Texture2D showMemoryImage;
    public Texture2D softwareImage;
    public Texture2D dateImage;
    public Texture2D showFpsImage;
    public Texture2D infoImage;
    public Texture2D saveLogsImage;
    public Texture2D searchImage;
    public Texture2D copyImage;
    public Texture2D closeImage;

    public Texture2D buildFromImage;
    public Texture2D systemInfoImage;
    public Texture2D graphicsInfoImage;
    public Texture2D backImage;

    public Texture2D logImage;
    public Texture2D warningImage;
    public Texture2D errorImage;
    
    public Texture2D even_logImage;
    public Texture2D odd_logImage;
    public Texture2D selectedImage;
}

#endregion

[CreateAssetMenu(fileName = "DebugSystemGUIData", menuName = "ScriptableObjects/MyLibrary/Tools/Debug/DebugSystemGUIData", order = 2)]
public class DebugSystemGUIDataScriptable : ScriptableObject
{
    [Header("GUI Skin")]
    public GUISkin debugSystemGUISkin;

    [Header("Font Size")]
    public int FontSize = 10;
    
    [Header("ICon Image")]
    public DebugSystemImagesResources imagesResources;

    [Header("ICon Size")]
    public Vector2 IconSize = new Vector2(20, 20);
}

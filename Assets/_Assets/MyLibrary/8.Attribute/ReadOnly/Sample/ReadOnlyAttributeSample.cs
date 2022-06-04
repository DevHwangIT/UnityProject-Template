using UnityEngine;

namespace MyLibrary.Attribute.Sample
{
    public class ReadOnlyAttributeSample : MonoBehaviour
    {
        //Always you can't Modify
        [ReadOnlyInEditorAttribute(false), SerializeField]
        private string testName;

        [ReadOnlyInEditorAttribute(false), SerializeField]
        private string testInfo;

        //if Unity is Playing GameMode. you can't Modify
        [ReadOnlyInEditorAttribute(true), SerializeField]
        private int testLv;
    }
}
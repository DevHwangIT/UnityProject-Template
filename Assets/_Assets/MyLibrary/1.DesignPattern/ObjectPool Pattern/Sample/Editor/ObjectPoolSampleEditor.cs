using UnityEngine;
using UnityEditor;

namespace MyLibrary.DesignPattern.Sample
{
    [CustomEditor(typeof(ObjectPool_Sample))]
    public class ObjectPoolSampleEditor : Editor
    {
        ObjectPool_Sample sample = null;
        private TestObjectEnum LastSelected;

        void OnEnable()
        {
            sample = (ObjectPool_Sample) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(20);

            GUILayout.Space(5);
            var greenStyle = new GUIStyle(GUI.skin.button);
            greenStyle.normal.textColor = Color.green;
            if (GUILayout.Button("Show Selected Object Total Count Log", greenStyle))
            {
                sample.ShowObjectDetailCount();
            }

            GUILayout.Space(10);
            if (EditorApplication.isPlaying)
            {
                GUILayout.Space(10);
                if (GUILayout.Button("Batching Object Array Button"))
                {
                    sample.BatchingObjectArray();
                }

                GUILayout.Space(10);
                if (GUILayout.Button("Recycle From Spawned Object"))
                {
                    sample.RecycleFromSpawnedObject();
                }

                GUILayout.Space(10);
                if (GUILayout.Button("Destroy Object In Pool"))
                {
                    sample.DestroyObjectInObjectPool();
                }

                GUILayout.Space(10);
                if (GUILayout.Button("Destroy Object Out Pool"))
                {
                    sample.DestroyObjectOutObjectPool();
                }

                GUILayout.Space(10);
                if (GUILayout.Button("If same Object In Pool, Destroy All"))
                {
                    sample.DestroyAllObjectInObjectPool();
                }
            }
        }
    }
}

using UnityEngine;

namespace MyLibrary.Utility.Sample
{
    public class MyJsonContainer
    {
        public string Name;
        public int Level;
        public float Exp;

        public void Logging()
        {
            Debug.Log("- Result \nName : " + Name + "\nLevel : " + Level + "\nExp : " + Exp);
        }
    }

    public class JsonSample : MonoBehaviour
    {
        MyJsonContainer jsonContainer;

        void Start()
        {
            jsonContainer = new MyJsonContainer();
            jsonContainer.Name = "Kim Test";
            jsonContainer.Level = 1;
            jsonContainer.Exp = 100.0f;

            JsonSaveLoader.SetDefaultPath = Application.persistentDataPath;
            
            if(JsonSaveLoader.Save(jsonContainer, "Test.txt"))
                Debug.Log("Save Success");
            else
                Debug.Log("Save Failed");
            
            jsonContainer.Name = "Failed Load Test";
            jsonContainer.Level = 2;
            jsonContainer.Exp = 50.0f;
            
            if(JsonSaveLoader.Load<MyJsonContainer>("Test.txt", out jsonContainer))
                Debug.Log("Load Success");
            else
                Debug.Log("Load Failed");
            
            jsonContainer.Logging();
        }
    }
}

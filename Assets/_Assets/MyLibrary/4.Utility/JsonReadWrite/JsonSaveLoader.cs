using System.IO;
using UnityEngine;

namespace MyLibrary.Utility
{
    public class JsonSaveLoader : MonoBehaviour
    {
        private static string defaultPath = "";

        public static string SetDefaultPath
        {
            get { return defaultPath; }
            set { defaultPath = value; }
        }
        
        public static bool Save<T>(T data, string _fileName)
        {
            if (defaultPath.Equals(""))
                defaultPath = Application.persistentDataPath;
            
            try
            {
                string json = JsonUtility.ToJson(data, true);
                
                if (json.Equals("{}"))
                {
                    Debug.Log("json null");
                    return false;
                }

                string path = defaultPath + _fileName;
                File.WriteAllText(path, json);
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("The file was not found:" + e.Message);
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.Log("The directory was not found: " + e.Message);
                return false;
            }
            catch (IOException e)
            {
                Debug.Log("The file could not be opened:" + e.Message);
                return false;
            }
            return true;
        }

        public static bool Load<T>(string _fileName, out T data)
        {
            data = default;
            if (defaultPath.Equals(""))
                defaultPath = Application.persistentDataPath;
            
            try
            {
                string path = defaultPath + _fileName;
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    data = JsonUtility.FromJson<T>(json);
                    return true;
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("The file was not found:" + e.Message);
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.Log("The directory was not found: " + e.Message);
                return false;
            }
            catch (IOException e)
            {
                Debug.Log("The file could not be opened:" + e.Message);
                return false;
            }
            return false;
        }
    }
}

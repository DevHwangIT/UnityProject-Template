using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyLibrary.Tools
{
	public class DebugSystemEditor : Editor
	{
		[MenuItem("MyLibrary/Tools/Debug/Create Debug Instance")]
		public static void CreateInstanceBinder()
		{
			DebugSystem _instance = (DebugSystem) FindObjectOfType(typeof(DebugSystem));
			if (_instance == null)
			{
				GameObject singletonObject = new GameObject($"Debug System (Singleton)");
				_instance = singletonObject.AddComponent<DebugSystem>();
			}
			_instance.GetComponent<DebugSystemGUI>().Initialize();
		}

		public class DebugSystemModificationProcessor : UnityEditor.AssetModificationProcessor
		{
			[InitializeOnLoad]
			public class BuildInfo
			{
				static BuildInfo()
				{
					EditorApplication.update += Update;
				}

				static bool isCompiling = true;

				static void Update()
				{
					if (!EditorApplication.isCompiling && isCompiling)
					{
						//Debug.Log("Finish Compile");
						if (!Directory.Exists(Application.dataPath + "/StreamingAssets"))
						{
							Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
						}

						string info_path = Application.dataPath + "/StreamingAssets/build_info";
						StreamWriter build_info = new StreamWriter(info_path);
						build_info.Write(
							"Build from " + SystemInfo.deviceName + " at " + System.DateTime.Now.ToString());
						build_info.Close();
					}

					isCompiling = EditorApplication.isCompiling;
				}
			}
		}
	}
}


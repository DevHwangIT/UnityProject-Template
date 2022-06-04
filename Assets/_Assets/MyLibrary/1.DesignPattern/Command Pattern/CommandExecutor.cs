using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.DesignPattern
{
    [ExecuteInEditMode, DisallowMultipleComponent]
    public class CommandExecutor : MonoBehaviour
    {
        #region Singgleton
        private static CommandExecutor _instance;
        public static CommandExecutor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (CommandExecutor) FindObjectOfType(typeof(CommandExecutor));
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(CommandExecutor)} (Singleton)");
                        _instance = singletonObject.AddComponent<CommandExecutor>();
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }
        #endregion
        
        private static Stack<ICommand> completeCommands = new Stack<ICommand>();
        private static Queue<ICommand> runningCommands = new Queue<ICommand>();
        private static bool isUndo = false;
        
        private void Awake()
        {
            StartCoroutine(CommandsRoutine());
        }

        private IEnumerator CommandsRoutine()
        {
            while (true)
            {
                if (runningCommands.Count > 0)
                {
                    ICommand command = runningCommands.Dequeue();
                    yield return StartCoroutine(command.Execute());
                    completeCommands.Push(command);
                }

                if (isUndo) 
                {
                    if (completeCommands.Count > 0)
                    {
                        ICommand undoCommand = completeCommands.Pop();
                        yield return StartCoroutine(undoCommand.Undo());
                    }
                    isUndo = false;
                }
                yield return null;
            }
        }

        public static void Run(ICommand command)
        {
            CommandExecutor executor = Instance;
            runningCommands.Enqueue(command);
        }

        public static void Undo()
        {
            CommandExecutor executor = Instance;
            isUndo = true;
        }
    }
}

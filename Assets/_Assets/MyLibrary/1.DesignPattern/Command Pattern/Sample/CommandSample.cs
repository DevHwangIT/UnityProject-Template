using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public class CommandSample : MonoBehaviour
    {
        private void Awake()
        {
            CommandExecutor.Run(new MoveCommand(this.gameObject));
            Invoke("UndoCall", 2f);
        }

        void UndoCall()
        {
            CommandExecutor.Undo();
        }

        public class MoveCommand : ICommand
        {
            private const float speed = 5.0f;
            private GameObject mover;

            public MoveCommand(GameObject obj)
            {
                mover = obj;
            }

            public IEnumerator Undo()
            {
                float time = 0f;
                while (time < 1f)
                {
                    mover.transform.Translate(Vector3.left * Time.deltaTime * speed);
                    time += Time.deltaTime;
                    yield return null;
                }
            }

            public IEnumerator Execute()
            {
                float time = 0f;
                while (time < 1f)
                {
                    mover.transform.Translate(Vector3.right * Time.deltaTime * speed);
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}

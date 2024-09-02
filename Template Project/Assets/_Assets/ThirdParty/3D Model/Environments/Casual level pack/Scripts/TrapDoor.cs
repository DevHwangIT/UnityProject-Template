using System.Collections;
using UnityEngine;

namespace CasualLevelsDemo
{
	public interface ICharacterFloorTrigger
	{
		void OnCharacterTrigger(Character character);
	}

	public class TrapDoor : MonoBehaviour, ICharacterFloorTrigger
	{
		[SerializeField] private float openDelay = 0.15f;
		[SerializeField] private float openPosition = -30.0f;
		[SerializeField] private float openedDuration = 1.0f;
		[SerializeField] private float openDuration;
		[SerializeField] private Vector3 rotationAxis = new Vector3(1, 0, 0);
		[SerializeField] private Transform rotationRoot;

		private Coroutine currentOpenCoroutine = null;
		
		public void OnCharacterTrigger(Character character)
		{
			if (currentOpenCoroutine != null)
				return;

			currentOpenCoroutine = StartCoroutine(Open());
		}

		private IEnumerator Open()
		{
			yield return new WaitForSeconds(openDelay);

			var rb = rotationRoot.GetComponent<Rigidbody>();
			
			var halfDuration = openDuration / 2.0f;
			var timeLeft = halfDuration;
			var closedRotation = rotationRoot.rotation;
			var openedRotation = closedRotation * Quaternion.AngleAxis(openPosition, rotationAxis);
			var waitForFixedUpdate = new WaitForFixedUpdate();
			yield return waitForFixedUpdate;

			while (timeLeft >= 0.0f)
			{
				rb.MoveRotation(Quaternion.Slerp(openedRotation, closedRotation, timeLeft / halfDuration));
				
				timeLeft -= Time.fixedDeltaTime;
				yield return waitForFixedUpdate;
			}

			rb.MoveRotation(openedRotation);
			
			yield return new WaitForSeconds(openedDuration);

			timeLeft = halfDuration;
			while (timeLeft >= 0.0f)
			{
				rb.MoveRotation(Quaternion.Slerp(closedRotation, openedRotation, timeLeft / halfDuration));
				
				timeLeft -= Time.fixedDeltaTime;
				yield return waitForFixedUpdate;
			}

			rb.MoveRotation(closedRotation);

			currentOpenCoroutine = null;
		}
	}
}
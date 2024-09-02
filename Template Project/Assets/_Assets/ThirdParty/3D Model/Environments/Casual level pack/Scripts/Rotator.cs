using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasualLevelsDemo
{
	public class Rotator : MonoBehaviour, IVelocityProvider
	{
		[SerializeField] private float rotationSpeed = 30.0f;
		
		public Vector3 GetVelocity(Vector3 worldPosition)
		{
			var difference = transform.position - worldPosition;
			return Vector3.Cross(transform.up, difference) * rotationSpeed;
		}
	}
}
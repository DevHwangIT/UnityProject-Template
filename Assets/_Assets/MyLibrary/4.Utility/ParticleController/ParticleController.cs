using UnityEngine;
using UnityEngine.Events;

namespace MyLibrary.Utility
{
	public enum PlayCondition
	{
		PlayOnAwake,
		PlayCall
	}
	
	public enum HideType
	{
		Destroy,
		Scaling,
		Enable,
		ObjectPooling
	}

	public enum DestroyCondition
	{
		Distance,
		Time,
		DeleteCall
	}
	
	public class ParticleController : MonoBehaviour
	{
		public HideType hideType = HideType.Enable;
		public DestroyCondition destroyType = DestroyCondition.DeleteCall;
		public PlayCondition playConditionType = PlayCondition.PlayCall;

		public Transform distanceCompareTarget = null;
		public float distanceValue = 0;
		private float destructionTimer = 0f;
		public float durationTime = 0;

		private ParticleSystem[] particleSystems;
		public ParticleSystem[] ParticleSystems
		{
			get
			{
				if (particleSystems == null)
					particleSystems = this.GetComponentsInChildren<ParticleSystem>(true);
				return particleSystems;
			}
		}
		
		private Vector3 InitPos;
		private bool isPlaying = false;

		public UnityEvent onGetToPool;
		public UnityEvent onReturnToPool;

		private void Awake()
		{
			Initialize();

			if (playConditionType == PlayCondition.PlayOnAwake)
				Play();
		}

		void FixedUpdate()
		{
			if (isPlaying)
			{
				destructionTimer += Time.fixedDeltaTime;
				switch (destroyType)
				{
					case DestroyCondition.Distance:
						if (Vector3.Distance(InitPos, distanceCompareTarget.position) > distanceValue)
							Stop();
						break;

					case DestroyCondition.Time:
						if (destructionTimer > durationTime)
							Stop();
						break;
				}
			}
		}

		private void Initialize()
		{
			InitPos = transform.position;
			destructionTimer = 0f;
			foreach (var particle in ParticleSystems)
			{
				ParticleSystem.MainModule particleMain = particle.main;
				particleMain.scalingMode = ParticleSystemScalingMode.Local;

				if (playConditionType == PlayCondition.PlayCall)
					particleMain.playOnAwake = false;
				else
					particleMain.playOnAwake = true;

				particle.gameObject.transform.localScale = Vector3.one;
				if (particle.isPlaying)
					particle.Stop();
			}

			this.gameObject.SetActive(true);
			isPlaying = false;

			switch (hideType)
			{
				case HideType.Destroy:
					Destroy(this.gameObject);
					break;

				case HideType.Scaling:
					for (int i = 0; i < ParticleSystems.Length; i++)
					{
						ParticleSystem.MainModule particle = ParticleSystems[i].main;
						particle.scalingMode = ParticleSystemScalingMode.Hierarchy;
					}

					break;

				case HideType.Enable:
					this.gameObject.SetActive(false);
					break;

				case HideType.ObjectPooling:
					onGetToPool?.Invoke();
					break;
			}
		}

		public void Play()
		{
			Initialize();
			foreach (var particle in ParticleSystems)
			{
				if (!particle.isPlaying) 
					particle.Play();
			}
			isPlaying = true;
		}
		
		public void Pause()
		{
			foreach (var particle in ParticleSystems)
			{
				if (particle.isPlaying)
					particle.Pause();
			}
			isPlaying = false;
		}

		public void Stop()
		{
			foreach (var particle in ParticleSystems)
			{
				if (particle.isPlaying)
					particle.Stop();
			}
			
			switch (hideType)
			{
				case HideType.Destroy:
					Destroy(this.gameObject);
					break;
				
				case HideType.Scaling:
					for (int i = 0; i < ParticleSystems.Length; i++)
						ParticleSystems[i].gameObject.transform.localScale = Vector3.zero;
					break;
				
				case HideType.Enable:
					this.gameObject.SetActive(false);
					break;
				
				case HideType.ObjectPooling:
					onReturnToPool?.Invoke();
					break;
			}
			isPlaying = false;
		}
	}
}

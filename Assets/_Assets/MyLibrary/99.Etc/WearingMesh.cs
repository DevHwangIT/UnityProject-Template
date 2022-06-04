using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메쉬 커스텀마이징 구현

public class WearingMesh : MonoBehaviour
{
	public GameObject WearObj;

	// 이렇게 비어있는 게임 오브젝트를 선언 해준다.
	private GameObject WearObjOnceWorn;

	private bool isWorn = false;

	void Start()
	{
		AddEquipment();
	}

	// 착용
	public void AddEquipment()
	{
		isWorn = true;

		// Here, boneObj must be instatiated and active (at least the one with the renderer),
		// or else GetComponentsInChildren won't work.
		//// 머 일단 이렇게 아랫쪽에 있을 SkinnedMeshRenderer를 받아둔다.
		SkinnedMeshRenderer[] BonedObjects = WearObj.GetComponentsInChildren<SkinnedMeshRenderer>();

		// 배열 속에 들어있을 SkinnedMeshRenderer에 대해 뭔가를 해 준다.
		foreach (SkinnedMeshRenderer smr in BonedObjects)
			ProcessBonedObject(smr);

		// We don't need the old obj, we make it disappear.
		WearObj.SetActive(false);
	}

	public void RemoveEquipment()
	{
		isWorn = false;

		// 오브젝트 제건
		if (WearObjOnceWorn != null)
			Destroy(WearObjOnceWorn);

		// 다시 착용 안된 바지..를 나타나게 한다.
		WearObj.SetActive(true);
	}

	private void ProcessBonedObject(SkinnedMeshRenderer ThisRenderer)
	{
		// Create the SubObject
		// 캐릭터에게 입힐 바지 오브젝트를 새로 만들자.
		WearObjOnceWorn = new GameObject(ThisRenderer.gameObject.name);

		// 이 위치에 새로운 버자(팬티)가 하위 객체로 생성되었다.
		WearObjOnceWorn.transform.parent = transform;

		// Add the renderer
		SkinnedMeshRenderer NewRenderer = WearObjOnceWorn.AddComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;

		// Assemble Bone Structure
		// 본도 받아야 한다. 일단 크기만큼 할당을 하자. ※왠지 SkinnedMeshRenderer에 본이 있다.
		Transform[] MyBones = new Transform[ThisRenderer.bones.Length];

		// As clips are using bones by their names, we find them that way.
		// 이 함수는 아래에 있다.
		for (int i = 0; i < ThisRenderer.bones.Length; i++)
			MyBones[i] = FindChildByName(ThisRenderer.bones[i].name, transform); // 하위 본을 전부 맞춤

		// 랜더러를 할당.
		NewRenderer.bones = MyBones;
		NewRenderer.sharedMesh = ThisRenderer.sharedMesh;
		NewRenderer.materials = ThisRenderer.materials;
	}

	// Recursive search of the child by name.
	private Transform FindChildByName(string ThisName, Transform ThisGObj)
	{
		// 리턴용 임시 Transform.
		Transform ReturnObj;

		// 검색 조건에 맞으면 리턴한다.
		if (ThisGObj.name == ThisName)
			return ThisGObj.transform;

		// Else, we go continue the search horizontaly and verticaly		
		foreach (Transform child in ThisGObj)
		{
			// 재귀함수
			ReturnObj = FindChildByName(ThisName, child);

			if (ReturnObj != null)
				return ReturnObj;
		}

		return null;
	}
}

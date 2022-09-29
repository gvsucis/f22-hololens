using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CrossComm.Framework.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Collection/Runtime GameObject Collection")]

	public class GameObjectCollectionSO : BaseRuntimeCollectionSO<GameObject>
    {
		public GameObject GetGameObjectInCollectionByName(string i_name)
		{
			return GetCollection().Find(item => item.name == i_name);
		}
	}
}

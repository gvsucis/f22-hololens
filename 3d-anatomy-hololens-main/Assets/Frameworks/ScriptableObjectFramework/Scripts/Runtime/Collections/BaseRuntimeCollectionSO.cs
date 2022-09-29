using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
    public abstract class BaseRuntimeCollectionSO<T> : BaseCollectionSO<T>
    {
		#region Monobehavior Methods

		protected override void OnEnable()
		{
			base.OnEnable();
            _collection = new List<T>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _collection = new List<T>();
        }

        #endregion Monobehavior Methods    
    }
}

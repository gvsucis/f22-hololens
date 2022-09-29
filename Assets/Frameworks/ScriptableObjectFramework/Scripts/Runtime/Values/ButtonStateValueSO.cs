using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
	[CreateAssetMenu(menuName = "Runtime Values/Button State")]
	public class ButtonStateValueSO : BaseRuntimeValueSO<ButtonState>
	{

	}

	[Serializable]
	public class ButtonState
	{
		public bool Down;
		public bool Held;
		public bool Up;
		public object Params;
	}
}
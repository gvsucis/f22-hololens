﻿using Deform;
using UnityEditor;
using UnityEngine;

namespace DeformEditor
{
	public class DeformEditorSettingsAsset : ScriptableObject
	{
		
		
		[CollapsibleSection("Scene")]
		[DisplayName("Solid Color")] public Color solidHandleColor = new Color (1f, 0.4f, 0f, 1f);
		[DisplayName("Light Color")] public Color lightHandleColor = new Color (1f, 0.4f, 0f, 0.25f);
		public float dottedLineSize = 5f;
		[DisplayName("Handle Size")] public float screenspaceHandleCapSize = 0.0275f;
		[DisplayName("Angle Handle Size")] public float screenspaceAngleHandleSize = 1.25f;
		[DisplayName("Lattice Handle Size")] public float screenspaceLatticeCapSize = 0.035f;

		[CollapsibleSection("Importer")]
		public bool modelsReadableByDefault = false;
		
		[CollapsibleSection("Builds")]
		public BuildStrippingMode buildStripping = BuildStrippingMode.NoStripping;
	}
}
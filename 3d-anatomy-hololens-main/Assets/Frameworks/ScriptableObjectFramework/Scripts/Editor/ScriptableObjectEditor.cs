using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System;

namespace CrossComm.Framework.ScriptableObjects
{
    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true)]
    public class ScriptableObjectEditor : Editor
    {
        private string _objectGuid = string.Empty; //GUID of this SO 
        private Dictionary<UnityEngine.Object, List<string>> _referenceObjects = new Dictionary<UnityEngine.Object, List<string>>(); // Key-Value pair is the SO or the prefab parent with a list of scripts that it is referenced on
        private Vector2 _scrollPosition = new Vector2();
        private Stopwatch _searchTimer = new Stopwatch();

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            //Get GUID
            if (GUILayout.Button("Update References"))
            {                
                _objectGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(target));
                UpdateReferences();
            }

            if (_objectGuid != string.Empty)
            {
                EditorGUILayout.HelpBox($"GUID: {_objectGuid}", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox($"Click on 'Update References' to view referneces in project", MessageType.Info);
                return;
            }

            if (_referenceObjects != null && _referenceObjects.Count > 0)
            {
                DisplayReferenceObjectList();
            }
        }

        private void UpdateReferences()
        {
            _searchTimer.Start();

            _referenceObjects = new Dictionary<UnityEngine.Object, List<string>>();

            // Get all prefabs and assets to check referece in
            var allPathToAssetsList = new List<string>();

            var allPrefabs = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories);
            allPathToAssetsList.AddRange(allPrefabs);

            //SOs are .asset files
            var allAssets = Directory.GetFiles(Application.dataPath, "*.asset", SearchOption.AllDirectories);
            allPathToAssetsList.AddRange(allAssets);

            // Iterate through meta files to find match for GUID
            string assetPath;
            string scriptGUID = string.Empty;

            for (int i = 0; i < allPathToAssetsList.Count; i++)
            {
                assetPath = allPathToAssetsList[i];
                var text = File.ReadAllText(assetPath);
                var lines = text.Split('\n');
                for (int j = 0; j < lines.Length; j++)
                {
                    var line = lines[j];

                    // Save the GUID of the any component found while iterating through lines of .prefab or .asset file
                    if (line.Contains("m_Script"))
					{
                        //strip all text excpet for the GUID of the script
                        //eg. m_Script: {fileID: 11500000, guid: 90b3b2a6317e4704eb13ed08d19f04c7, type: 3} becomes 90b3b2a6317e4704eb13ed08d19f04c7
                        scriptGUID = line.ToString();
						scriptGUID = scriptGUID.Substring(scriptGUID.IndexOf("guid:"));
						scriptGUID = scriptGUID.Replace("guid: ", string.Empty);
						int indexOfType = scriptGUID.IndexOf(", type:");
						scriptGUID = scriptGUID.Substring(0, indexOfType);
					}

					if (line.Contains("guid:"))
                    {
                        if (line.Contains(_objectGuid))
                        {
							string scriptPath = AssetDatabase.GUIDToAssetPath(scriptGUID);
							string scriptName = Path.GetFileName(scriptPath);
							scriptName = scriptName.Replace(".cs", string.Empty);

							var pathToReferenceAsset = assetPath.Replace(Application.dataPath, string.Empty);
                            pathToReferenceAsset = pathToReferenceAsset.Replace(".meta", string.Empty);
                            var path = "Assets" + pathToReferenceAsset;
                            path = path.Replace(@"\", "/"); // fix OSX/Windows path
                            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                            if (asset != null)
                            {
                                if (!_referenceObjects.ContainsKey(asset))
                                {
                                    _referenceObjects.Add(asset, new List<string>());
                                }
                                _referenceObjects[asset].Add(scriptName);                                
                            }
                            else
                            {
                                //Debug.LogError(path + " could not be loaded");
                            }
                        }
                    }
                }
            }

            _searchTimer.Stop();
            EditorGUILayout.HelpBox($"Search took: {_searchTimer.Elapsed}", MessageType.Info);
            _searchTimer.Reset();
        }

        private void DisplayReferenceObjectList()
        {
            GUILayout.Label("Reference by: " + _referenceObjects.Count + " assets. (Last search duration: " + _searchTimer.Elapsed + ")");
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            foreach (var referenceObject in _referenceObjects)
            {
                var referencingObject = referenceObject.Key;
                var referenceCount = referenceObject.Value.Count;
                EditorGUILayout.ObjectField(referencingObject.name + " (" + referenceCount + ")", referencingObject, typeof(UnityEngine.Object), false);
                foreach(string referenceScript in referenceObject.Value)
				{
                    EditorGUILayout.SelectableLabel(referenceScript, EditorStyles.miniTextField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}


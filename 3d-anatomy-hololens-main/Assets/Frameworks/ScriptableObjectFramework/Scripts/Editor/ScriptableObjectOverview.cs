using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace CrossComm.Framework.ScriptableObjects
{
    public class ScriptableObjectOverview : EditorWindow
    {

        #region Private Fields
        //Serialized
        //Non-Serialized
        private Dictionary<string, Dictionary<UnityEngine.Object, List<string>>> _scriptableObjectGUIDtoReferences = new Dictionary<string, Dictionary<UnityEngine.Object, List<string>>>(); // Key-Value pair is the GUID of the SO and Key-Value Pair of referencing objects and the components they are referenced in.
        private Vector2 _scrollPosition = new Vector2();
        private Stopwatch _searchTimer = new Stopwatch();
        private List<bool> _foldoutBools = new List<bool>();
        private bool _expandAll = false;
        #endregion Private Fields

        #region Public Fields

        [MenuItem("SO Framework/Tools/SO Reference Overview")]
        private static void Init()
        {
            // Get existing open window or if none, make a new one:
            EditorWindow.GetWindow(typeof(ScriptableObjectOverview));
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Update Overview"))
            {
                UpdateOverview();
            }

            if (_scriptableObjectGUIDtoReferences != null && _scriptableObjectGUIDtoReferences.Count > 0)
            {
                if (GUILayout.Button("Expand/Collapse All"))
                {
                    _expandAll = !_expandAll;
                    UpdateOverview();
                }

                DisplayReferenceObjectList();
            }
        }

        #endregion Public Fields

        #region Private Methods

        /// <summary>
        /// Updates _scriptableObjectGUIDReferences to be used in displaying overview
        /// </summary>
        private void UpdateOverview()
        {
            //Used to toggle referencing object foldouts
            _foldoutBools.Clear();

            _searchTimer.Reset();
            _searchTimer.Start();

            _scriptableObjectGUIDtoReferences = new Dictionary<string, Dictionary<UnityEngine.Object, List<string>>>();

            // Get all Scriptable Objects in project by GUID
            List<string> scriptableObjectGUIDs = new List<string>(AssetDatabase.FindAssets("t: ScriptableObject"));

            //Exclude referencing objects located in External folders, add remaining GUIDs to _GUIDtoReferencesList
            foreach (string scriptableObjectGUID in scriptableObjectGUIDs)
            {
                if (AssetDatabase.GUIDToAssetPath(scriptableObjectGUID).Contains("\\External\\"))
                {
                    continue;
                }
                if (!_scriptableObjectGUIDtoReferences.ContainsKey(scriptableObjectGUID))
                {
                    _scriptableObjectGUIDtoReferences.Add(scriptableObjectGUID, new Dictionary<Object, List<string>>());
                }
            }

            // Get all prefabs and assets to check refereces
            List<string> allPathToAssetsList = new List<string>();

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

                string text = File.ReadAllText(assetPath);
                string[] lines = text.Split('\n');
                for (int j = 0; j < lines.Length; j++)
                {
                    string line = lines[j];

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
                        //Iterate through the GUIDs for all Scriptable Objects in the project to see if they are referenced by this object
                        foreach (var GUID in _scriptableObjectGUIDtoReferences)
                        {
                            if (line.Contains(GUID.Key))
                            {
                                string scriptPath = AssetDatabase.GUIDToAssetPath(scriptGUID);
                                string scriptName = Path.GetFileName(scriptPath);
                                scriptName = scriptName.Replace(".cs", string.Empty);

                                string pathToReferenceAsset = assetPath.Replace(Application.dataPath, string.Empty);
                                pathToReferenceAsset = pathToReferenceAsset.Replace(".meta", string.Empty);
                                string path = "Assets" + pathToReferenceAsset;
                                path = path.Replace(@"\", "/"); // fix OSX/Windows path
                                Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                                if (asset != null)
                                {
                                    if (!GUID.Value.ContainsKey(asset))
                                    {
                                        GUID.Value.Add(asset, new List<string>());
                                    }
                                    GUID.Value[asset].Add(scriptName);
                                }
                                else
                                {
                                    //Debug.LogError(path + " could not be loaded");
                                }
                            }
                        }

                    }
                }
            }
            _searchTimer.Stop();

            //Remove SOs that are not referenced by anything
            List<string> scriptableObjectGUIDsToRemove = new List<string>();

            foreach(var kvp in _scriptableObjectGUIDtoReferences)
            {
                if (kvp.Value.Count == 0)
                {
                    scriptableObjectGUIDsToRemove.Add(kvp.Key);
                }
            }

            foreach(string GUIDToRemove in scriptableObjectGUIDsToRemove)
            {
                _scriptableObjectGUIDtoReferences.Remove(GUIDToRemove);
            }
        }

        /// <summary>
        /// Uses _scriptableObjectGUIDReferences to display overview
        /// </summary>
        private void DisplayReferenceObjectList()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            //Foldout bools are rerquired to keep track of foldout states, we know the foldout bools need to be initialized if the list is empty, as this is cleared at the top of UpdateOverview()
            bool initializeBools = (_foldoutBools.Count == 0);

            //Counter for tracking 
            int scriptableObjectGUIDCounter = 0;

            List<string> scriptableObjectTypes = new List<string>();

            foreach (string scriptableObjectGUID in _scriptableObjectGUIDtoReferences.Keys)
            {
                string path = AssetDatabase.GUIDToAssetPath(scriptableObjectGUID);
                var type = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path).GetType();
                scriptableObjectTypes.Add(type.ToString());
            }

            //Sort Types Alphabetically
            scriptableObjectTypes.Sort();

            // Key-Value pair is the Scriptable Object type and a list of GUIDs of that type
            Dictionary<string, List<string>> scriptableObjectTypetoGUIDList = new Dictionary<string, List<string>>();

            //Add Keys
            foreach (string scriptableObjectType in scriptableObjectTypes)
            {
                if (!scriptableObjectTypetoGUIDList.ContainsKey(scriptableObjectType))
                {
                    scriptableObjectTypetoGUIDList.Add(scriptableObjectType, new List<string>());
                }
            }

            //Add Values
            foreach (string RefGUID in _scriptableObjectGUIDtoReferences.Keys)
            {
                string path = AssetDatabase.GUIDToAssetPath(RefGUID);
                string type = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path).GetType().ToString();
                scriptableObjectTypetoGUIDList[type].Add(RefGUID);
            }

            //Render Overview
            foreach (string type in scriptableObjectTypetoGUIDList.Keys)
            {
                //Label - Scriptable Object Type
                EditorGUILayout.LabelField(type);
                GUILayout.Space(5);

                foreach (string GUID in scriptableObjectTypetoGUIDList[type])
                {
                    //Initialize bool for each foldout
                    if (initializeBools)
                    {
                        _foldoutBools.Add(_expandAll);
                    }

                    string path = AssetDatabase.GUIDToAssetPath(GUID);
                    Object so = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                    int numberOfReferences = _scriptableObjectGUIDtoReferences[GUID].Count;

                    //Horizontal Element - Scriptable Object Field and Foldout
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField("", so, typeof(UnityEngine.Object), false);
                    bool foldOut = EditorGUILayout.Foldout(_foldoutBools[scriptableObjectGUIDCounter], numberOfReferences.ToString(), true);
                    GUILayout.EndHorizontal();
                    //EditorGUILayout.LabelField(AssetDatabase.GUIDToAssetPath(GUID));

                    //Foldout content is only visible if foldout is clicked
                    if (foldOut)
                    {
                        GUILayout.Space(5);
                        GuiLine(1, Color.white);
                        GUILayout.Space(10);

                        foreach (var referencingObject in _scriptableObjectGUIDtoReferences[GUID])
                        {
                            //Object Field - Objects referencing the current Scriptable Object
                            EditorGUILayout.ObjectField("", referencingObject.Key, typeof(UnityEngine.Object), false);
                            foreach (string referenceScript in referencingObject.Value)
                            {
                                //Label Field - Names of components referencing Scriptable Object within referencing Object
                                EditorGUILayout.SelectableLabel(referenceScript, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                            }
                            GUILayout.Space(10);
                        }

                        GuiLine(1, Color.white);
                        GUILayout.Space(5);
                    }

                    //Store foldOut state and iterate counter
                    _foldoutBools[scriptableObjectGUIDCounter] = foldOut;
                    scriptableObjectGUIDCounter++;
                }

                GUILayout.Space(10);
            }

            GUILayout.Label(_scriptableObjectGUIDtoReferences.Count + " SOs with references, (Last search duration: " + _searchTimer.Elapsed + ")");
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Tool for drawing GUILines in editor
        /// </summary>
        /// <param name="i_height"></param>
        /// <param name="i_color"></param>
        private void GuiLine(int i_height, Color i_color)

        {
            Rect rect = EditorGUILayout.GetControlRect(false, i_height);
            rect.height = i_height;
            EditorGUI.DrawRect(rect, i_color);
        }

        #endregion Private Methods

        #region Public Methods
        #endregion Public Methods

        #region Coroutines
        #endregion Coroutines

        #region Events
        #endregion Events

    }
}

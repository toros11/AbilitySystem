﻿using UnityEngine;
using UnityEditor;
using AbilitySystem;
using System.IO;

namespace EntitySystemApp.Editor {
    public static class ScriptableObjectUtility {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>


        //[MenuItem("Assets/Requirement Prototype")]
        //public static void CreateAbilityRequirement() {
        //    CreateAsset<RequirementPrototype>("Ability Requirement");
        //}

        //[MenuItem("Assets/Ability")]
        //public static void CreateAbility() {
        //    //CreateAsset<Ability>("Ability");
        //}

        [MenuItem("Assets/Database")]
        public static void CreateDatabase() {
            CreateAsset<Persistence.Database>();
        }

        public static void CreateAsset<T>(string assetName = null) where T : ScriptableObject {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "") {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "") {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            assetName = assetName ?? typeof(T).ToString();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + assetName + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}

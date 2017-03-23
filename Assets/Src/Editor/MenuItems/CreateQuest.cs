using UnityEngine;
using UnityEditor;
using EntitySystem;
using System.IO;

public static class QuestMenuItem {
    [MenuItem("Assets/Quests")]
    public static QuestCreator CreateScriptableObject() {
        QuestCreator asset = ScriptableObject.CreateInstance<QuestCreator>();
        string assetPath = "Assets/Quests/quest.asset";
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(assetPath);
        Quest quest = new Quest();
        quest.Id = Path.GetFileNameWithoutExtension(assetPathAndName);
        AssetSerializer serializer = new AssetSerializer();
        serializer.AddItem(quest);
        asset.source = serializer.WriteToString();
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return asset;
    }
}
using UnityEngine;

namespace TacticsCore.Save
{
    [System.Serializable]
    public abstract class SaveData
    {
        
    }
    
    public static class SaveManager
    {
        private static readonly string SaveFilePath = Application.persistentDataPath + "/save.json";

        public static bool Save(SaveData saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData);
            Debug.Log($"Saving data: {jsonData} to file: {SaveFilePath}");
            System.IO.File.WriteAllText(SaveFilePath, jsonData);
            return true;
        }
        
        public static T Load<T>() where T : SaveData
        {
            string jsonData = System.IO.File.ReadAllText(SaveFilePath);
            Debug.Log($"Loading data: {jsonData}");
            return JsonUtility.FromJson<T>(jsonData);
        }
    }
}
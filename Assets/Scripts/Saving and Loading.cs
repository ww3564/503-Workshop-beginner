using UnityEngine;

public class Saving_and_Loading : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPos;
    }

    public static void SaveCurrentData()
    {
        if (Kratos_movement.i == null)
        {
            return;
        }

        SaveData newSaveData = new SaveData();
        newSaveData.playerPos = Kratos_movement.i.transform.position;

        string convertedData = JsonUtility.ToJson(newSaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/SaveData.json", convertedData);
    }

    public static SaveData LoadData()
    {
        string dataToLoad = System.IO.File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        return JsonUtility.FromJson<SaveData>(dataToLoad);
    }

    public static bool IsThereDataToLoad()
    {
        return System.IO.File.Exists(Application.persistentDataPath + "/SaveData.json");
    }

    public static void DeleteAllData()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/SaveData.json");
    }
}

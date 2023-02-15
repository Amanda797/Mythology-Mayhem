using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 

{
    //Save folder for all save paths
    public static string savePath = Application.persistentDataPath + "/Save Data";

    /* Player Data
    Saves and loads player stats. 
        TODO: Multiplayer support
    
        SavePlayer()
    Takes in a Player game object. Opens a new binary formatter and file stream to save processed player data to a persistent data path. 
    Encrypted (Binary). Custom file path is .MMS (for mythology mayhem save).

        LoadPlayer()
    Returns stored player data, if the save data exists. Opens a new binary formatter and file stream and deserializes the data saved in the path by opening the file.
        
    */
    public static void SavePlayer(PlayerStats player) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "/player.mms";
        FileStream stream = new FileStream(savePath + path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }//end Save Player

    public static PlayerData LoadPlayer() {
        string path = savePath + "/player.mms";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath + "/player.mms", FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        } else {
            Debug.LogError("Save file not found at path: " + savePath + "/player.mms");
            return null;
        }
    }//end Load Player



    /* World Data
    Saves scene stats. World data includes objects in the scene that can be interacted with by the player, thus changing its state of being, as well as enemies.
    TODO: What objects should be reset and which ones should be restored?
    
    */
    



}

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	//writes binary file with player data in a directory
	//public static void SavePlayer (Player player)
	//{
	//	BinaryFormatter formatter = new BinaryFormatter();
	//	string path = Application.persistentDataPath + "/player.skk";
	//	FileStream stream = new FileStream(path, FileMode.Create);

	//	PlayerData data = new PlayerData(player);

	//	formatter.Serialize(stream, data);
	//	stream.Close();
	//}
	public static void SavePlayer()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.skk";
		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(Player.bestScore, Player.balance, Player.activeSkinID, Player.activeLevelID, Player.boughtSkinsID, Player.boughtLevelsID);

		formatter.Serialize(stream, data);
		stream.Close();
	}

	//DELETES THE SAVE FILE, USE WITH CAUTION
	public static void DeleteSave()
	{
		string path = Application.persistentDataPath + "/player.skk";
		File.Delete(path);
	}

	//reads binary file with player data in a directory and loads the data into the player
	public static PlayerData LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.skk";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;

			stream.Close();
			return data;
		}
		else
		{
			Debug.Log("Save file not found in " + path);
			return null;
		}
	}
    /**
     * Automatically loads and saves the loaded values to the player
     * 
     */
    public static void LoadAndAssign()
    {
        PlayerData p = LoadPlayer();
        Player.activeSkinID = p.activeSkinID;
        Player.activeLevelID = p.activeLevelID;
        Player.balance = p.balance;
        Player.bestScore = p.bestScore;
        Player.boughtSkinsID = p.boughtSkinsID;
        Player.boughtLevelsID = p.boughtLevelsID;
    }
}

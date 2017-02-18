using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string newGame;
    public string selectGame;

    public int startPlayerLife;
    public int startPlayerGold;

    PlayerLifeSystem lifeSystem;
    PlayerData playerData;

    void Start()
    {
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        lifeSystem = FindObjectOfType<PlayerLifeSystem>();
        playerData = new PlayerData();

      

    }

    public void NewGame()
    {
        Application.LoadLevel(newGame);
        PlayerPrefs.SetInt("CurrentPlayerLife", startPlayerLife);
        PlayerPrefs.SetInt("CurrentPlayerGold", startPlayerGold);

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            playerData = (PlayerData)bf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("CurrentPlayerLife", playerData.life);
            PlayerPrefs.SetInt("CurrentPlayerGold", playerData.gold);

            //int x = PlayerPrefs.GetInt("CurrentPlayerLife", playerData.life);

            //Debug.Log("currentlife : " + x);
        }
    }

    public void SelectLevel()
    {
        Application.LoadLevel(selectGame);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        int x = PlayerPrefs.GetInt("CurrentPlayerLife");
        int y =  PlayerPrefs.GetInt("CurrentPlayerGold");
        playerData.life = x;
        playerData.gold = y;

        bf.Serialize(file, playerData);
        file.Close();
    }

    //public void Load()
    //{
    //    if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

    //        playerData = (PlayerData)bf.Deserialize(file);
    //        file.Close();

    //        PlayerPrefs.SetInt("CurrentPlayerLife", playerData.life); 

    //    }

        
    //}
}

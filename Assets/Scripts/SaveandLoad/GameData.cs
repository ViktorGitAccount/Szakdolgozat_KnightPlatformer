using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int currency;

    public Serializeable_Dictionary<string, bool> chestStates;
    public Serializeable_Dictionary<string, bool> skillTree;
    public Dictionary<string, int> skillUsage = new Dictionary<string, int>();
    public Serializeable_Dictionary<string, int> inventory;
    public List<string> equipmentId;
    public Serializeable_Dictionary<string, bool> checkpoints;
    public string closestCheckpointId;
    public string sceneOfCheckpoint;


    public float lostCurrencyX;
    public float lostCurrencyY;
    public int lostCurrencyAmount;

    public GameData()
    {
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrencyAmount = 0;

        sceneOfCheckpoint = "MainScene";
        chestStates = new Serializeable_Dictionary<string, bool>();
        skillTree = new Serializeable_Dictionary<string, bool>();
        inventory = new Serializeable_Dictionary<string, int>();
        equipmentId = new List<string>();

        closestCheckpointId = string.Empty;
        checkpoints= new Serializeable_Dictionary<string, bool>();
    }
}

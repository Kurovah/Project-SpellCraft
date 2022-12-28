using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData saveData;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        saveData = new GameData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Modifiers> GetModifiers()
    {
        return saveData.equippedModifiers;
    }
}

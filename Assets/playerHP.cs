using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHP : HP
{
    public override void OnDeath()
    {
        SaveData.deaths++;
        Debug.Log(SaveData.deaths);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

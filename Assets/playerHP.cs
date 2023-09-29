using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHP : HP
{
    public override void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

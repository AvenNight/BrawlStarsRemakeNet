using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;

    private bool levelComplete = false;
    public bool LevelComplite => levelComplete;

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Player" && !levelComplete)
        {
            levelComplete = true;
            StartCoroutine("GameOver");
            Debug.Log("FINISH");
        }
    }

    private IEnumerator GameOver()
    {
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(10f);
        gameOverText.SetActive(false);
        Destroy(this.gameObject);
    }
}

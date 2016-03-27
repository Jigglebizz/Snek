using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public Sn8ke snakePrefab;
    public Pup pupperPrefab;
    private int score;

    private Sn8ke snake;

    // Use this for initialization
    void Start () {
        score = 0;
        snake = (Sn8ke)GameObject.Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
        newPupper();
    }

    public void incrementScore()
    {
        score++;
    }

    public void newPupper()
    {
        Instantiate(pupperPrefab,
                    Vector3.zero,
                    Quaternion.identity);
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}

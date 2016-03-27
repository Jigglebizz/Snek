using UnityEngine;
using System.Collections;

public class Pup : MonoBehaviour {

    // Use this for initialization
    void Start () {
        Vector3 position = new Vector3(
                                        Random.Range(-Camera.main.orthographicSize * Camera.main.aspect + 1f,
                                                      Camera.main.orthographicSize * Camera.main.aspect - 1f),
                                        Random.Range(-Camera.main.orthographicSize + 1f,
                                                      Camera.main.orthographicSize - 1f),
                                        
                                        0);
        gameObject.transform.position = position;
    }
}

using UnityEngine;
using System.Collections;

public class Sn8keHead : MonoBehaviour {
    GameObject parent;

    public void setParent(GameObject snek)
    {
        this.parent = snek;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Body")
        {
            ((Sn8ke)parent.GetComponent<Sn8ke>()).die();
        }
        else if (coll.gameObject.tag == "Food")
        {
            Destroy(coll.gameObject);
            ((Game)Camera.main.GetComponent<Game>()).newPupper();
            ((Game)Camera.main.GetComponent<Game>()).incrementScore();
            ((Sn8ke)parent.GetComponent<Sn8ke>()).addSegment();
        }
    }
}

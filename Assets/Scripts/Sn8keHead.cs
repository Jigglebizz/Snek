using UnityEngine;
using System.Collections;

public class Sn8keHead : MonoBehaviour {
    GameObject parent;

    public Sprite sprUp;
    public Sprite sprLeft;
    public Sprite sprDown;
    public Sprite sprRight;

    public void setParent(GameObject snek)
    {
        this.parent = snek;
    }

    void Update()
    {
        // Calculate sprite
        SpriteRenderer sprRenderer = gameObject.GetComponent<SpriteRenderer>();

        float angle;
        Vector3 axis = Vector3.zero;
        gameObject.transform.rotation.ToAngleAxis(out angle, out axis);

        if (axis.z < 0)
            angle = 180 - angle + 180;

        if (angle < 45 || angle > 315)
            sprRenderer.sprite = sprUp;
        else if (angle > 45 && angle < 135)
            sprRenderer.sprite = sprLeft;
        else if (angle > 135 && angle < 225)
            sprRenderer.sprite = sprDown;
        else if (angle > 225 && angle < 315)
            sprRenderer.sprite = sprRight;
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
            ((Sn8ke)parent.GetComponent<Sn8ke>()).increaseSpeed();
        }
    }
}

using UnityEngine;
using System.Collections;

public class Sn8ke : MonoBehaviour {
    public GameObject headPrefab;
    public GameObject bodyPrefab;
    public GameObject tailPrefab;

    public float sensitivity;           // We can turn this fast
    public float velocity;              // Snek speed
    public float headRotationOffset;    // Where the head rotates
    public float bodyRotationOffset;    // Where each body piece rotates
    public float tailRotationOffset;    // Where we rotate on the tail piece
    public int spacing;                 // Spacing between body parts
    public float speedIncrement;        // How much we increment the speed by

    private GameObject head;            // The head
    private ArrayList body;             // All of the bodies
    private GameObject tail;            // Tail

    private bool dead;                  // Have we dead?

    /// <summary>
    /// Initialize Snek
    /// </summary>
    void Start () {
        // Initial vars
        dead = false;

        // Create head
        head =  Instantiate(headPrefab, 
                            Vector3.zero, 
                            Quaternion.identity) 
                as GameObject;
        ((Sn8keHead)head.GetComponent<Sn8keHead>()).setParent(gameObject);

        // Create tail
        tail =  Instantiate(tailPrefab, 
                            Vector3.zero, 
                            Quaternion.identity) 
                as GameObject;
        ((Sn8kePart)tail.GetComponent<Sn8kePart>()).setSpacing(spacing, Vector3.zero, Quaternion.identity);
        ((Sn8kePart)tail.GetComponent<Sn8kePart>()).setRotationOffset(tailRotationOffset);

        // Get that body started
        body = new ArrayList();
        GameObject bodySegment =    Instantiate(bodyPrefab,
                                                Vector3.zero,
                                                Quaternion.identity)
                                    as GameObject;
        ((Sn8kePart)bodySegment.GetComponent<Sn8kePart>()).setSpacing(spacing, Vector3.zero, Quaternion.identity);
        ((Sn8kePart)bodySegment.GetComponent<Sn8kePart>()).setRotationOffset(bodyRotationOffset);
        body.Add(bodySegment);
    }
    
    /// <summary>
    /// Propogates movement throughout body using routines in Sn8kePart.
    /// Also handles control of snake
    /// </summary>
    void Update () {
        // Propogate back-to-front movement
        // Add the last body segment's transform to the tail's transform queue
        Vector3 position; Quaternion rotation;
        ((Sn8kePart)((GameObject)body[body.Count - 1])
                                 .GetComponent<Sn8kePart>())
                                 .getSpacedTransform(out position, out rotation);
        ((Sn8kePart)tail
                    .GetComponent<Sn8kePart>())
                    .addTransformToList(position, rotation);

        // Iterate through the body, back to front, and add the parent segment's transform to the transform queue
        for (int i = body.Count - 1; i > 0; i--)
        {
            ((Sn8kePart)((GameObject)body[i - 1])
                                     .GetComponent<Sn8kePart>())
                                     .getSpacedTransform(out position, out rotation);
            ((Sn8kePart)((GameObject)body[i]).GetComponent<Sn8kePart>())
                                             .addTransformToList(position, rotation);
        }

        // For the first body piece, add the head's transform to its queue
        ((Sn8kePart)((GameObject)body[0])
                                 .GetComponent<Sn8kePart>())
                                 .addTransformToList(   head.transform.position, 
                                                        head.transform.rotation);

        // Update head control
        if (!dead)
        {
            float rotation_amt = -Input.GetAxis("Horizontal") * sensitivity * Time.deltaTime;
            head.transform.Translate(new Vector3(0, -headRotationOffset, 0));
            head.transform.Rotate(Vector3.forward, rotation_amt);
            head.transform.Translate(new Vector3(0, headRotationOffset, 0));
            head.transform.position += head.transform.up * velocity * Time.deltaTime;
        }

        // Debug control - add a segment
        //if (Input.GetKeyDown("space"))
        //{
        //    addSegment();
        //}
    }

    public void die()
    {
        dead = true;
        Debug.Log("You have died");
    }

    /// <summary>
    /// Adds a segment to our snake
    /// </summary>
    public void addSegment()
    {
        GameObject bodySegment = Instantiate(   bodyPrefab,
                                                tail.transform.position,
                                                Quaternion.identity)
                                    as GameObject;
        Vector3 position; Quaternion rotation;
        ((Sn8kePart)((GameObject)tail)
                                .GetComponent<Sn8kePart>())
                                .getSpacedTransform(out position, out rotation);
        ((Sn8kePart)bodySegment.GetComponent<Sn8kePart>()).setSpacing(spacing, position, rotation);
        ((Sn8kePart)bodySegment.GetComponent<Sn8kePart>()).setRotationOffset(bodyRotationOffset);
        bodySegment.tag = "Body";
        body.Add(bodySegment);
    }

    public void increaseSpeed()
    {
        velocity += speedIncrement;
    }
}

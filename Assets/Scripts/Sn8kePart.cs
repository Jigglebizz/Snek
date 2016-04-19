using UnityEngine;
using System.Collections;

public class Sn8kePart : MonoBehaviour {
    public int spacing;
    public Sprite sprUp;
    public Sprite sprLeft;
    public Sprite sprDown;
    public Sprite sprRight;

    // The transform list will be a queue of fixed length
    // so it will be implemented as a circular array
    private Vector3[] transformPositionList;
    private Quaternion[] transformRotationList;
    private int transformListIndex;

    // This is so we rotate the pieces at the correct place
    private float rotationOffset;

    /// <summary>
    /// Set the spacing between snake parts. Sets up our queue
    /// </summary>
    /// <param name="value">spacing amount</param>
    /// <param name="position">position to initialize queue to</param>
    /// <param name="rotation">rotation to initialize queue to</param>
    public void setSpacing(int value, Vector3 position, Quaternion rotation)
    {
        spacing = value;
        transformPositionList = new Vector3[spacing];
        transformRotationList = new Quaternion[spacing];
        transformListIndex = 0;
        for (int i = 0; i < spacing; i++)
        {
            transformPositionList[i] = position;
            transformRotationList[i] = rotation;
        }
    }

    /// <summary>
    /// Set the offset of rotation
    /// </summary>
    /// <param name="value">y value in relative coordinates from center</param>
    public void setRotationOffset(float value)
    {
        rotationOffset = value;
    }

    /// <summary>
    /// Called every frame. Updates position and transform;
    /// </summary>
    void Update()
    {
        if (transformPositionList != null)
        {
            Vector3 position;
            Quaternion rotation;
            getSpacedTransform(out position, out rotation);
            gameObject.transform.Translate(new Vector3(0, rotationOffset, 0));
            gameObject.transform.rotation = rotation;
            gameObject.transform.Translate(new Vector3(0, -rotationOffset, 0));

            // Calculate sprite
            SpriteRenderer sprRenderer = gameObject.GetComponent<SpriteRenderer>();

            float angle;
            Vector3 axis = Vector3.zero;
            rotation.ToAngleAxis( out angle,  out axis);

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

            gameObject.transform.position = position;
        }
    }

    /// <summary>
    /// Add a transform to the queue of transforms
    /// </summary>
    /// <param name="transformPosition">Position to add</param>
    /// <param name="transformRotation">Rotation to add</param>
    public void addTransformToList(Vector3 transformPosition, Quaternion transformRotation)
    {
        if (transformListIndex == spacing - 1)
            transformListIndex = 0;
        else
            transformListIndex++;
        
        transformPositionList[transformListIndex] = transformPosition;
        transformRotationList[transformListIndex] = transformRotation;
    }

    /// <summary>
    /// Get the last transform in the queue
    /// </summary>
    /// <param name="position">The position</param>
    /// <param name="rotation">The rotation</param>
    public void getSpacedTransform(out Vector3 position, out Quaternion rotation)
    {
        if (transformPositionList != null)
        {
            position = transformPositionList[(transformListIndex + 1) % spacing];
            rotation = transformRotationList[(transformListIndex + 1) % spacing];
        }
        else
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }
    }
}

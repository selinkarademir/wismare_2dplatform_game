using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform firstPos, secondPos;

    public float speed;

    Vector3 nextPos;

    private void Start()

    {

        nextPos = firstPos.position;

    }

    private void Update()

    {

        if (transform.position == firstPos.position)

            nextPos = secondPos.position;

        if (transform.position == secondPos.position)

            nextPos = firstPos.position;

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }
}

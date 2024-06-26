using UnityEngine;
using System.Collections.Generic;

public class BallManager : MonoBehaviour
{
    public List<GameObject> balls; // List to hold the ball GameObjects
    public float gap = 2f; // Gap between balls

    private void Start()
    {
        ArrangeBalls();
    }

    public void RemoveBall(GameObject ball)
    {
        if (balls.Contains(ball))
        {
            balls.Remove(ball);
            Destroy(ball);
            ArrangeBalls();
        }
    }

    private void ArrangeBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            balls[i].transform.position = new Vector3(i * gap, balls[i].transform.position.y, balls[i].transform.position.z);
        }
    }
}

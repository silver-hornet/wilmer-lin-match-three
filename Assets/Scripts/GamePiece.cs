using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;

    Board m_board;

    bool m_isMoving = false; // to prevent any buggy movement between frames, particuarly if you try to double tap

    public InterpType interpolation = InterpType.SmootherStep;

    public enum InterpType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    };

    public MatchValue matchValue;

    public enum MatchValue
    {
        Yellow,
        Blue,
        Magenta,
        Indigo,
        Green,
        Teal,
        Red,
        Cyan,
        Wild
    }

    void Start()
    {

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    Move((int)transform.position.x + 2, (int)transform.position.y, 0.5f);
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    Move((int)transform.position.x - 2, (int)transform.position.y, 0.5f);
        //}
    }

    public void Init(Board board)
    {
        m_board = board;
    }

    public void SetCoord(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        if (!m_isMoving)
        {
            StartCoroutine(MoveRoutine(new Vector3(destX, destY, 0), timeToMove));
        }
    }

    IEnumerator MoveRoutine(Vector3 destination, float timeToMove)
    {
        Vector3 startPosition = transform.position;

        bool reachedDestination = false;

        float elapsedTime = 0f;

        m_isMoving = true;

        while (!reachedDestination)
        {
            // if we are close enough to destination
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                reachedDestination = true;

                if (m_board != null)
                {
                    m_board.PlaceGamePiece(this, (int)destination.x, (int)destination.y);
                }
                break;
            }

            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f); // although don't really need to use Mathf.Clamp, since Lerp already has a clamp built-in

            switch (interpolation)
            {
                case InterpType.Linear:
                    break;
                case InterpType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f); // Adds subtle deceleration towards the end of the movement
                    break;
                case InterpType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f); // Adds subtle acceleration towards the start of the movement
                    break;
                case InterpType.SmoothStep:
                    t = t * t * (3 - 2 * t); // Smoothstep formula eases in and eases out
                    break;
                case InterpType.SmootherStep:
                    t = t * t * t * (t * (t * 6 - 15) + 10); // Smootherstep formula is even smoother than the above Smoothstep formula
                    break;
            }

            transform.position = Vector3.Lerp(startPosition, destination, t);

            // wait until next frame
            yield return null;
        }

        m_isMoving = false;
    }
}

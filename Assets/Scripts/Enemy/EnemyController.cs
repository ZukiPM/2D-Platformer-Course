using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSmoothness = 1f;
    [SerializeField] float waitingTime = 1f;
    float actualTime = 0;

    bool waiting = false;
    bool toPos01 = false;
    bool toPos02 = false;

    Vector2 pos01;
    Vector2 pos02;

    Vector2 _velocity;

    private void Awake() 
    {
        pos01 = transform.GetChild(0).position;
        pos02 = transform.GetChild(1).position;
    }

    private void Update() 
    {
        if(!GameManager.singleton.isPaused)
        {
            if(!toPos01 && !toPos02) toPos01 = true;

            if(toPos01 && !waiting)
            {
                transform.position = Vector2.SmoothDamp(transform.position, pos01, ref _velocity, moveSmoothness);
            }
            else if(toPos02 && !waiting)
            {
                transform.position = Vector2.SmoothDamp(transform.position, pos02, ref _velocity, moveSmoothness);
            }

            if(toPos01 && checkDistanceToPoint(pos01) < 0.1f)
            {
                WaitOnPoint();
            }

            if(toPos02 && checkDistanceToPoint(pos02) < 0.1f)
            {
                WaitOnPoint();
            }

            if(waiting)
            {
                actualTime += Time.deltaTime;
            }
        }
    }

    void WaitOnPoint()
    {
        waiting = true;

        if(actualTime >= waitingTime)
        {
            actualTime = 0;
            waiting = false;

            if(toPos01)
            {
                toPos01 = false;
                toPos02 = true;
            }
            else if(toPos02)
            {
                toPos01 = true;
                toPos02 = false;
            }
        }
    }

    float checkDistanceToPoint(Vector2 pos)
    {
        float dist = Vector2.Distance(transform.position, pos);

        return dist;
    }
}

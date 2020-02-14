using UnityEngine;
using System.Collections;
using System.Deployment.Internal;

public class MoveLabirinto : MonoBehaviour
{
    public GameObject wayPointsT;
     
    public bool _useCoroutine;
    
    [Range(0.001f, 0.5f)][SerializeField] private float _nextWayPointWaitTime;


    // Use this for initialization
    void Start()
    {
        //if (_useCoroutine)
        //    StartCoroutine(MoveAlongWaipointsCoroutine());
    }


    // Update is called once per frame
    void Update()
    {
        wayPointsT = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log(wayPointsT);
        //if (_useCoroutine)
        //    return;

        //for (int i = 0; i < wayPointsT.childCount; i++)
        //{
        //    Vector3 wayPointPosition = wayPointsT.GetChild(i).position;
        //    transform.position = new Vector3(wayPointPosition.x, transform.position.y, wayPointPosition.z);
        //}
    }

    public IEnumerator MoveAlongWaipointsCoroutine()
    {
        while (true)
        {
            //for (int i = 0; i < wayPointsT.childCount; i++)
            //{
            //    Vector3 wayPointPosition = wayPointsT.GetChild(i).position;
            //    transform.position = new Vector3(wayPointPosition.x, transform.position.y, wayPointPosition.z);
       
            //    yield return new WaitForSeconds(_nextWayPointWaitTime);
            //}
        }
    }
}

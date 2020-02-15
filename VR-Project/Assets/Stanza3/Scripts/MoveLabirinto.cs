using UnityEngine;
using System.Collections;
using System.Deployment.Internal;
using System.Collections.Generic;

public class MoveLabirinto : MonoBehaviour
{
    public GameObject wayPoints;
    private List<Vector3> positions;
    public bool arrivato= false;
    private float _nextWayPointWaitTime=0.25f;   //velocità (più è alto più va lento)
    private Vector3[] initPos = new Vector3[10];

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void tornaIndietro()
    {
        int levaN;
        int.TryParse(name, out levaN);
        transform.position = initPos[levaN-1];
    }

    public IEnumerator MoveAlongWaipointsCoroutine()
    {
        int levaN;
        int.TryParse(name, out levaN);
        initPos[levaN-1]= transform.position;
        wayPoints = this.gameObject.transform.GetChild(0).gameObject;

        while (arrivato==false)
        {
            positions = new List<Vector3>();

            for (int i = 0; i < wayPoints.transform.childCount; i++)
            {
                positions.Add(wayPoints.transform.GetChild(i).position);
            }
            for(int i = 0; i < positions.Count; i++)
            {
                Vector3 wayPointPosition = positions[i];
                transform.position = new Vector3(wayPointPosition.x, transform.position.y, wayPointPosition.z);
                yield return new WaitForSeconds(_nextWayPointWaitTime);
            }
            arrivato = true;
        }
        arrivato = false;
    }
}

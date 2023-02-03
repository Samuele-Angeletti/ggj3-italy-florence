using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<GameObject> PointList;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(PointList != null)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < PointList.Count; i++)
            {
                if(i < PointList.Count - 1)
                Gizmos.DrawLine(PointList[i].transform.position, PointList[i + 1].transform.position);
                Gizmos.DrawSphere(PointList[i].transform.position, 0.1f);
            }
        }
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;

    public int GetSortingOrder(GameObject obj)
    {
        float objDist = Mathf.Abs(top.position.y - obj.transform.position.y);
        float totalDist = Mathf.Abs(top.position.y - bottom.position.y);

        return (int)(Mathf.Lerp(System.Int16.MinValue, System.Int16.MaxValue, objDist / totalDist));
    }

}

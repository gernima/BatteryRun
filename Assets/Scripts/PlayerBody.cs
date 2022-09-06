using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private int gap = 2;
    [SerializeField] private float bodySpeed = 12f;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<int> bodyPartsIndex = new List<int>();
    private List<Vector3> positionHistory = new List<Vector3>();

    public void Grow()
    {
        GameObject body = Instantiate(bodyPrefab, transform.position, transform.rotation);
        bodyParts.Add(body);
        int index = 0;
        index++;
        bodyPartsIndex.Add(index);
    }
    private void FixedUpdate()
    {
        positionHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var body in bodyParts)
        {
            Vector3 point = positionHistory[Mathf.Min(index * gap, positionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.fixedDeltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }
}

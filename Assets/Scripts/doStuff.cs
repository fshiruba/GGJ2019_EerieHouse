using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doStuff : MonoBehaviour
{
    public Vector3 startpos;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var randx = Random.Range(25, 50);
        var randy = Random.Range(25, 50);

        var dest = transform.position + new Vector3(randx, randy, 0);

        transform.position = Vector3.Lerp(startpos, dest, Time.deltaTime * 5);

    }
}

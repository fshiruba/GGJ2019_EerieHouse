using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Camera cam;
    public float PanSpeed = 15f;
    public float panborderthickness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        //var delta = Input.mouseScrollDelta;

        //if (delta.y != 0)
        //{
        //    cam.orthographicSize += -1 * delta.y;

        //    if (cam.orthographicSize > 5)
        //    {
        //        cam.orthographicSize = 5;
        //    }

        //    if (cam.orthographicSize < 5)
        //    {
        //        cam.orthographicSize = 5;
        //    }
        //}

        if (Input.mousePosition.y >= Screen.height - panborderthickness)
        {
            pos.z += PanSpeed * Time.deltaTime;
            //pos.x += PanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panborderthickness)
        {
            pos.z -= PanSpeed * Time.deltaTime;
            //pos.x -= PanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - panborderthickness)
        {
            pos.x += PanSpeed * Time.deltaTime;
            //pos.z -= PanSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panborderthickness)
        {
            pos.x -= PanSpeed * Time.deltaTime;
            //pos.z += PanSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -15f, 0f);
        pos.z = Mathf.Clamp(pos.z, -15f, 0f);

        transform.position = pos;
    }
}

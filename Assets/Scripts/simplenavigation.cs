using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class simplenavigation : MonoBehaviour
{
    NavMeshAgent agent;
    public Camera Cam;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hit.point, .1f);        
        Gizmos.DrawRay(ray);
        Gizmos.DrawLine(ray.origin, hit.point);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Cam.ScreenPointToRay(Input.mousePosition);

            var ask = 1 << 9;
            var mask = ~ask;

            var r = Physics.RaycastAll(ray, 100f, mask);

            foreach (var hit2 in r)
            {
                if (hit2.collider.CompareTag("interactive"))
                {
                    hit2.collider.gameObject.GetComponent<IActionObject>().StopHaunt();
                }
            }
            
            //if (Physics.Raycast(ray, out hit))
            //{
            //    if (hit.collider.CompareTag("interactive"))
            //    {
            //        hit.collider.gameObject.GetComponent<IActionObject>().StopHaunt();
            //    }
            //}
        }
    }
}

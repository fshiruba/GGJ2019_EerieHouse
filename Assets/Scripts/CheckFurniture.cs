using System.Linq;
using UnityEngine;

public class CheckFurniture : MonoBehaviour
{
    public GameObject[] TodasMobilias = new GameObject[0];
    public GameObject[] TodasMobiliasForaDeVisao = new GameObject[0];
    public GameObject[] TodasMobiliasAmaldicoadas = new GameObject[0];
    public bool isHaunting;
    public GameObject PersonNavagent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TodasMobilias = GameObject.FindGameObjectsWithTag("interactive");

        //foreach (var item1 in TodasMobilias)
        //{
        //var parent = item1.GetComponentInParent<MeshRenderer>();
        //parent.material.color = Color.black;
        //}

        var visao = PersonNavagent.GetComponent<SphereCollider>().bounds;

        TodasMobiliasForaDeVisao = TodasMobilias.ToList().Where(a => !visao.Intersects(a.GetComponent<BoxCollider>().bounds)).ToArray();
        TodasMobiliasAmaldicoadas = TodasMobilias.ToList().Where(b => b.GetComponent<IActionObject>().IsHaunted).ToArray();

        isHaunting = TodasMobiliasAmaldicoadas.Length > 0;

        //foreach (var item2 in TodasMobiliasForaDeVisao)
        //{
        //var parent = item2.GetComponentInParent<MeshRenderer>();
        //parent.material.color = Color.white;
        //}
    }
}

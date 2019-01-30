using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersonCoordinator : MonoBehaviour
{
    public GameObject PersonSprite;
    public GameObject PersonNavagent;
    public CheckFurniture CF;
    public Bounds visao;
    //public Image hp;
    public GameObject hpbar;
    public Sprite BackupSprite;
    public Color cor;

    public float timeUntilChaos;
    public float Insanidade; //ou desconforto, sei lah
    public float batata = 100f;

    public Sprite[] carinha;
    public Image carinhaRef;
    public Image barrinha;
    public TextMesh Tm;
    public int pontos;

    public audioFiles afiles;
    public AudioSource[] audios;

    public float Difficulty = 1f;


    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();
        afiles = GetComponent<audioFiles>();

        audios[0].clip = afiles.arrayAudios[3];
        audios[0].Play();

        timeUntilChaos = Random.Range(2f, 5f) * 60;
    }

    // Update is called once per frame
    void Update()
    {
        visao = PersonNavagent.GetComponent<SphereCollider>().bounds;
        //bool recover = true;

        foreach (var item0 in CF.TodasMobiliasAmaldicoadas)
        {
            var pos = item0.GetComponent<BoxCollider>().bounds; //   .transform.position;

            if (visao.Intersects(pos))
            {
                //recover = false;
                Insanidade += .25f;
            }
        }

        Insanidade = Mathf.Clamp(Insanidade, 0f, 100f);

        if (Insanidade > 33)
        {
            carinhaRef.sprite = carinha[1];
        }

        if (Insanidade > 66)
        {
            carinhaRef.sprite = carinha[2];
        }

        if (Insanidade >= 100)
        {
            SceneManager.LoadScene(3);
        }

        hpbar.transform.localScale = new Vector3((100 - Insanidade) * 0.092f, 1, 1);

        float r = Insanidade > 50 ? Insanidade / (batata / 4) : Insanidade / (batata / 2);
        float g = 1f - r;

        r = Mathf.Clamp(r, 0f, 1f);
        g = Mathf.Clamp(g, 0f, 1f);

        Debug.Log("R " + r + " - G " + g);

        cor = new Color(r, g, 0f);

        hpbar.GetComponentInChildren<MeshRenderer>().material.color = cor;

        //foreach (var item1 in CF.TodasMobilias)
        //{
        //    var parent = item1.GetComponent<MeshRenderer>();
        //    parent.material.color = Color.black;
        //}

        //foreach (var item2 in CF.TodasMobiliasForaDeVisao)
        //{
        //    if (item2.GetComponent<IActionObject>().IsHaunted)
        //    {
        //        continue;
        //    }
        //
        //    var parent = item2.GetComponent<MeshRenderer>();
        //    parent.material.color = Color.white;
        //}

        if (CF.isHaunting)
        {
            return;
        }

        timeUntilChaos--;

        if (timeUntilChaos <= 0)
        {
            timeUntilChaos = Random.Range(3f, 5f) * (60 - Difficulty);

            if (CF.TodasMobiliasForaDeVisao.Length <= 0)
            {
                return;
            }

            var movelAleatorio = Random.Range(0, CF.TodasMobiliasForaDeVisao.Length);

            //Debug.Log(todasMobiliasForadaVisao.Count + " candidatos - escolhido index " + movelAleatorio);

            var toHaunt = CF.TodasMobiliasForaDeVisao[movelAleatorio];

            Difficulty += 3;

            if (Insanidade > 50)
            {
                audios[1].clip = afiles.arrayAudios[2];
            }
            else if (Insanidade > 30)
            {
                audios[1].clip = afiles.arrayAudios[1];
            }
            else
            {
                audios[1].clip = afiles.arrayAudios[0];
            }

            audios[1].Play();

            toHaunt.GetComponent<IActionObject>().Haunt();

            //StartCoroutine(Haunt(toHaunt));
        }
    }

    private IEnumerator Haunt(GameObject gameObject)
    {
        var comp = gameObject.GetComponent<IActionObject>();
        comp.IsHaunted = true;
        Difficulty--;

        for (int i = 0; i < 5000; i++)
        {
            if (comp.IsHaunted == false)
            {
                //if (CF.TodasMobiliasAmaldicoadas.Length == 0)
                //{
                //    isHaunting = false;
                //}

                break;
            }

            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            gameObject.GetComponent<MeshRenderer>().material.color = new Color(r, g, b);

            yield return new WaitForSeconds(.01f);
        }

        comp.IsHaunted = false;
        //isHaunting = false;

        yield return null;
    }
}

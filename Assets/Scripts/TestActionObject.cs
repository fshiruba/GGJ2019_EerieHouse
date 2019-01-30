using UnityEngine;

public class TestActionObject : MonoBehaviour, IActionObject
{
    MeshRenderer render;

    public bool IsHaunted { get; set; }
    public Color DefaultColor { get; set; }
    public Animator Anim { get; set; }
    public placar pl;

    public void Haunt()
    {
        IsHaunted = true;
        Anim.Play("HAUNT");

        if (gameObject.name.ToUpper() == "TV")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (gameObject.name.ToUpper().Contains("CADEIRA"))
        {
            var audios = gameObject.GetComponentInChildren<AudioSource>();
            var file = gameObject.GetComponentInChildren<audioFiles>();

            audios.clip = file.arrayAudios[0];
            audios.Play();
        }

    }

    public void StopHaunt()
    {
        if (!IsHaunted)
        {
            return;
        }

        IsHaunted = false;
        Anim.Play("IDLE");

        if (gameObject.name.ToUpper() == "TV")
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }

        if (gameObject.name.ToUpper().Contains("CADEIRA"))
        {
            var audios = gameObject.GetComponentInChildren<AudioSource>();
            var file = gameObject.GetComponentInChildren<audioFiles>();

            audios.clip = file.arrayAudios[1];
            audios.Play();
        }

        pl.pontos = pl.pontos + 1;
    }

    public void Test()
    {
        if (IsHaunted)
        {
            render.material.color = DefaultColor;
            IsHaunted = false;
        }
        else
        {
            render.material.color = new Color(1f, 0f, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pl = FindObjectOfType<placar>();

        render = GetComponent<MeshRenderer>();
        DefaultColor = render.material.color;

        var tryGetAnim = GetComponent<Animator>();

        if (tryGetAnim == null)
        {
            var tryTwoorMore = GetComponentsInChildren<Animator>();

            if (tryTwoorMore.Length > 1)
            {
                var rnd = Random.Range(0, tryTwoorMore.Length);
                Anim = tryTwoorMore[rnd];
            }
            else if (tryTwoorMore.Length == 1)
            {
                Anim = tryTwoorMore[0];
            }

        }
        else
        {
            Anim = tryGetAnim;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}

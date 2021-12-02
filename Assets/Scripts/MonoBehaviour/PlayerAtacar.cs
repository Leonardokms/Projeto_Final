using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtacar : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Inimigo")
            {
                Vector2 PosInimigo = hit.transform.position;
                Vector2 PosPlayer = transform.position;
                float dis = Vector2.Distance(PosInimigo, PosPlayer);

                if (Input.GetKey("A"))
                {
                    if (dis < 0.5f)
                    {
                        hit.transform.GetComponent<InimigoBase>().ReceberDano(1);
                    }
                }
            }
        }
    }


}

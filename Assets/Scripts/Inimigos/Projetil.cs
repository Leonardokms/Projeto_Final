using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
	public float duracaoProjetil;
	
	
    void Start()
	{
		Destroy(this.gameObject, duracaoProjetil);
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}

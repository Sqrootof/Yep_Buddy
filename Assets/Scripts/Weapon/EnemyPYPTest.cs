using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPYPTest : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <=0)
        {
            Destroy(gameObject);
        }
    }
}

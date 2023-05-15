using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Enemy enemy;

    public RectTransform group;
    public RectTransform bar;
    // Start is called before the first frame update
    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * 3f;
        transform.rotation = Camera.main.transform.rotation;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(enemy.curHealth >= 0)
        { 
        bar.localScale = new Vector3((float)enemy.curHealth / enemy.maxHealth, 1, 1);
        }
        else
        {
            enemy.curHealth = 0;
        }
    }
}

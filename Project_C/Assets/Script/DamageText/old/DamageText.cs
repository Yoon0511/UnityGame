using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public TextMesh textMesh;
    [SerializeField]
    float speed = 1.5f;
    [SerializeField]
    float alpha = 0.5f;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = textMesh.color;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        color.a -= alpha * Time.deltaTime;
        textMesh.color = color;
        if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Init(Transform _postion, Color _color,float _damage)
    {
        textMesh.text = _damage.ToString();
        transform.position = _postion.position;
        textMesh.color = _color;
    }
}

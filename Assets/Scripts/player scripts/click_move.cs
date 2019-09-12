using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click_move : MonoBehaviour
{
    [SerializeField] float speed;
    [HideInInspector] Vector3 target;
    //[SerializeField] Transform FollowCamera;
    //[SerializeField] float tolerance = .25f;

    private void Start()
    {
        target = gameObject.transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                target = hit.point;
            }
        }
    }
    void FixedUpdate()
    {
        target = new Vector3(target.x, transform.position.y, target.z);
        Vector3 mov = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.position = mov;
        transform.LookAt(target);
    }
}

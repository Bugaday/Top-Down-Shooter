using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Transform bullet;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask enemiesMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public List<Transform> invisibleTargets = new List<Transform>();

    private float bulletVelocity = 5000f;
    private float speed = 10f;
    private float rotSpeed = 200f;

    private LineRenderer leftLine;
    private LineRenderer rightLine;
    private Quaternion rightLineAngle;
    private Quaternion leftLineAngle;
    private Vector3[] linePositionsLeft = new Vector3[2];
    private Vector3[] linePositionsRight = new Vector3[2];

    void Start()
    {
        rightLine = transform.GetChild(1).GetComponent<LineRenderer>();
        leftLine = transform.GetChild(2).GetComponent<LineRenderer>();
        linePositionsRight[0] = transform.position;
        linePositionsLeft[0] = transform.position;

        Quaternion lineAngle = Quaternion.AngleAxis(viewAngle / 2, transform.up);
        Debug.Log(Quaternion.Angle(transform.rotation,lineAngle));

        StartCoroutine("FindTargetsWithDelay", .1f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // Update is called once per frame
    void Update () {

        rightLineAngle = Quaternion.AngleAxis(viewAngle / 2, transform.up);
        leftLineAngle = Quaternion.AngleAxis(360 - (viewAngle / 2), transform.up);

        linePositionsRight[0] = transform.position;
        linePositionsRight[1] = transform.position + (rightLineAngle * (transform.forward * viewRadius));

        linePositionsLeft[0] = transform.position;
        linePositionsLeft[1] = transform.position + (leftLineAngle * (transform.forward * viewRadius));

        rightLine.SetPositions(linePositionsRight);
        leftLine.SetPositions(linePositionsLeft);

        float distFromCam = Camera.main.transform.position.y - transform.position.y;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,distFromCam));

        Vector3 mouseDir = mousePos - transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        transform.rotation = Quaternion.LookRotation(mouseDir);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        invisibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemiesMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
                else
                {
                    invisibleTargets.Add(target);
                }
            }
            else
            {
                invisibleTargets.Add(target);
            }
        }

        foreach (Transform visibleTarget in visibleTargets)
        {
            GameObject visTargetChild = visibleTarget.GetChild(0).gameObject;
            visTargetChild.layer = 9;

            foreach (Transform child in visTargetChild.transform)
            {
                child.gameObject.layer = 9;
            }
        }

        foreach (Transform invisibleTarget in invisibleTargets)
        {
            GameObject invisTargetChild = invisibleTarget.GetChild(0).gameObject;
            invisTargetChild.layer = 10;

            foreach (Transform child in invisTargetChild.transform)
            {
                child.gameObject.layer = 10;
            }
        }

    }

    void Shoot()
    {
        GameObject firedBullet = Instantiate(bullet.gameObject,transform.position,Quaternion.identity);
        Rigidbody firedBulletRB = firedBullet.GetComponent<Rigidbody>();

        firedBulletRB.AddForce(transform.forward * bulletVelocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamProject;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 velocity;
    public CharacterController ccComponent;
    public GameObject bulletPrefab;
    public Transform modelTransform, aimTransform;

    float cooldown;
    Plane floorPlane;
    Vector3 worldMousePos;
    public float cooldownSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ccComponent = GetComponent<CharacterController>();
        floorPlane = new Plane(Vector3.up, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseWorldPos();
        Vector3 right = Vector3.Cross(-Camera.main.transform.forward, Vector3.up);
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        Vector3 facingVector = worldMousePos - transform.position;

        aimTransform.forward = facingVector;
        velocity = (Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward) * moveSpeed * Time.deltaTime;
        ccComponent.Move(velocity);

        modelTransform.forward = Input.GetMouseButton(0)? Vector3.Slerp(modelTransform.forward, facingVector, 0.1f) : Vector3.Slerp(modelTransform.forward, velocity, 0.1f);

        if(Input.GetMouseButton(0) && cooldown <= 0)
        {
            Instantiate(bulletPrefab, transform.position + modelTransform.forward, modelTransform.rotation);
            StartCoroutine(CoolDownTimer());
        }
    }


    void GetMouseWorldPos()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (floorPlane.Raycast(ray, out distance))
        {
            worldMousePos = ray.GetPoint(distance);
        }
    }

    IEnumerator CoolDownTimer()
    {
        cooldown = 1;
        while(cooldown > 0)
        {
            cooldown -= cooldownSpeed;
            yield return null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(worldMousePos, Vector3.one);
    }
}

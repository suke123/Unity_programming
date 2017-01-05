using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject SparkPrefab;
    public Transform muzzle;
    public float gun_speed = 50;
    public int bulletSpeed = 2;

    public GameObject trajectory;
    public GameObject targetMarker;
    public Material material;
    //public int limit = 10;

    public float targetDistance = 0.0f;
    public float firingAngle = 45.0f;
    public float baseRotationAngle = 0.0f;
    public float gravity = 9.8f;

    //private GameObject bullet;

    private Vector3 force;
    private List<GameObject> trajectorys = new List<GameObject>();
    private float flightDuration;

    // Use this for initialization
    void Start () {
        //transform.parent = GameObject.Find("Base").transform;
        //Transform tar = GameObject.Find("SparkLoc").transform;
    }
	
	// Update is called once per frame
	void Update () {
        firingAngle = 90.0f - muzzle.transform.rotation.eulerAngles.x + 2.0f;
        baseRotationAngle = transform.parent.rotation.eulerAngles.y;

        // Calculate distance to target
        targetDistance = Mathf.Pow(gun_speed, 2) * Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity;
        force = CalcForce();

        Transform Base = transform.parent;
        if(Base != null){
            Base.Rotate(
                new Vector3(0, Input.GetAxis("Horizontal") * 60.0f * Time.deltaTime, 0),
                Space.World
            );
        }
        else
        {
            Debug.LogWarning("Set Base!");
        }
      
        transform.Rotate(
            new Vector3(Input.GetAxis("Vertical") * 60.0f * Time.deltaTime, 0, 0),
            Space.Self
        );

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Fire(force);
        }

        CalcTrajectory(force);

    }

    public void Fire(Vector3 force) {
        //Transform tar = GameObject.Find("SparkLoc").transform;
        Transform tar = transform.Find("SparkLoc");
        if (tar != null)
        {
            Instantiate(
                SparkPrefab,
                tar.position,
                transform.rotation
            );
        }
        else
        {
            Debug.LogWarning("Set tar!");
        }

        GameObject bullet = (GameObject)Instantiate(
            BulletPrefab, 
            transform.position, 
            transform.rotation
        );
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * gun_speed, ForceMode.VelocityChange);
    }

    Vector3 CalcForce()
    {
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = gun_speed;//Mathf.Sqrt(targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity));

        // Extract the X  Y componenent of the velocity
        float Vx = projectile_Velocity * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = projectile_Velocity * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        flightDuration = targetDistance / Vx;
        Debug.Log(flightDuration);

        return new Vector3(Vx * Mathf.Sin(baseRotationAngle * Mathf.Deg2Rad), Vy, Vx * Mathf.Cos(baseRotationAngle * Mathf.Deg2Rad));
    }

    void CalcTrajectory(Vector3 force)
    {
        ClearTrajectory();

        float time = 0.5f;
        //int limit = (int)flightDuration;
        for (int i = 1; ; i++)
        {
            Vector3 pos = CalcPositionFromForce(time * i, BulletPrefab.GetComponent<Rigidbody>().mass, muzzle.position, force, Physics.gravity);
            if (pos.y < 0)
            {
                Vector3 prePos = CalcPositionFromForce(time * (i - 1), BulletPrefab.GetComponent<Rigidbody>().mass, muzzle.position, force, Physics.gravity);
                Vector3 impactPos = CalcImpactPos(pos, prePos);
                targetMarker.transform.position = impactPos;
                //trajectorys.Add(targetMarker);
                break;
            }
            /*
            GameObject sp = (GameObject)Instantiate(trajectory, pos, transform.rotation);
            sp.transform.parent = transform;
            Destroy(sp.GetComponent<SphereCollider>());
            //sp.GetComponent<Renderer>().material = material;
            trajectorys.Add(sp);
            /**/
        }

    }

    void ClearTrajectory()
    {
        foreach (GameObject obj in trajectorys)
        {

            Destroy(obj);

        }

        trajectorys.Clear();
    }

    Vector3 CalcPositionFromForce(float time, float mass, Vector3 startPosition, Vector3 force, Vector3 gravity)
    {
        Vector3 speed = (force / mass) /* * Time.fixedDeltaTime*/;
        Vector3 position = (speed * time) + (gravity * 0.5f * Mathf.Pow(time, 2));

        return startPosition + position;
    }

    Vector3 CalcImpactPos(Vector3 pos, Vector3 prePos)
    {
        float x, y = 0, z;
        if ((pos.x - prePos.x) == 0) x = pos.x;
        else x = -prePos.y / (pos.y - prePos.y) * (pos.x - prePos.x) + prePos.x;
        //if ((pos.y - prePos.y) == 0) y = pos.y;
        if ((pos.z - prePos.z) == 0) z = pos.z;
        else z = -prePos.y / (pos.y - prePos.y) * (pos.z - prePos.z) + prePos.z;

        return new Vector3(x, 0.5f, z);
    }

    IEnumerator SimulateProjectile()
    {
        // Short delay added before Projectile is thrown
        //yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        //bullet.transform.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = gun_speed;//Mathf.Sqrt(targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity));

        // Extract the X  Y componenent of the velocity
        float Vx = projectile_Velocity * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = projectile_Velocity * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = targetDistance / Vx;
        Debug.Log(flightDuration);

        // Rotate projectile to face the target.
        //bullet.transform.rotation = Quaternion.LookRotation(Target.position - bullet.transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            //bullet.transform.Translate(0, (Vy - (gravity * elapse_time * bulletSpeed)) * Time.deltaTime, Vx * Time.deltaTime * bulletSpeed);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }
}

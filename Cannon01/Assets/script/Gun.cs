using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject SparkPrefab;
    public Transform muzzle;
    public float gun_speed = 50;
    public int life = 6;
    public LifePanel lifePanel;     //自分HP

    //public EnemyBehavior enemyForce;    //敵の攻撃力
    //public int enemy_force;

    public GameObject trajectory;
    public GameObject targetMarker;
    public Material material;
      
    public float targetDistance = 0.0f;
    public float firingAngle = 45.0f;
    public float baseRotationAngle = 0.0f;
    public float gravity = 9.8f;

    public float maxMuzzleAngle = 90.0f;
    public float minMuzzleAngle = 45.0f;
    public float maxBaseAngle = 60.0f;
    public float minBaseAngle = -60.0f;
    public float moveSpeed = 60.0f;

    private Vector3 force;
    private List<GameObject> trajectorys = new List<GameObject>();

    // Use this for initialization
    void Start () {
        //enemy_force = enemyForce.EnemyForce();
    }
	
	// Update is called once per frame
	void Update () {

        firingAngle = 90.0f - muzzle.transform.rotation.eulerAngles.x + 2.0f;
        baseRotationAngle = transform.parent.rotation.eulerAngles.y;
        
        force = CalcForce();
        
        if (Input.GetKeyDown(KeyCode.Space)){
            Fire(force);
        }

        Transform Base = transform.parent;
        float turnV = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotateX = transform.eulerAngles.x;
        // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminMuzzleAngleからMaxMuzzleAngle内に収まるようにする
        float angleX = Mathf.Clamp(rotateX + turnV, minMuzzleAngle, maxMuzzleAngle);
        transform.rotation = Quaternion.Euler(angleX, Base.eulerAngles.y, 0);

        float turnH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //現在の回転角度を0～360から-180～180に変換
        float rotateY = (Base.eulerAngles.y > 180) ? Base.eulerAngles.y - 360 : Base.eulerAngles.y;
        // 現在の回転角度に入力(turn)を加味した回転角度をMathf.Clamp()を使いminBaseAngleからMaxBaseAngle内に収まるようにする
        float angleY = Mathf.Clamp(rotateY + turnH, minBaseAngle, maxBaseAngle);
        // 回転角度を-180～180から0～360に変換
        angleY = (angleY < 0) ? angleY + 360 : angleY;
        Base.rotation = Quaternion.Euler(0, angleY, 0);

        CalcTrajectory(force);

    }

    void Fire(Vector3 force) {
        Transform tar = transform.Find("SparkLoc");
        Instantiate(
             SparkPrefab,
             tar.position,
             transform.rotation
         );

        GameObject bullet = (GameObject)Instantiate(
         BulletPrefab,
         muzzle.transform.position,
         transform.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
        GetComponent<AudioSource>().Play();

    }

    Vector3 CalcForce()
    {
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = gun_speed;//Mathf.Sqrt(targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity));

        // Extract the X  Y componenent of the velocity
        float Vx = projectile_Velocity * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = projectile_Velocity * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate distance to target
        targetDistance = Mathf.Pow(gun_speed, 2) * Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity;

        //flightDuration = targetDistance / Vx;
        //Debug.Log(flightDuration);

        return new Vector3(Vx * Mathf.Sin(baseRotationAngle * Mathf.Deg2Rad), Vy, Vx * Mathf.Cos(baseRotationAngle * Mathf.Deg2Rad));
    }

    void CalcTrajectory(Vector3 force)
    {
        ClearTrajectory();

        float time = 0.5f;
        
        for(int i = 1;; i++){
            Vector3 pos = CalcPositionFromForce(time * i, BulletPrefab.GetComponent<Rigidbody>().mass, muzzle.position, force, Physics.gravity);
            if (pos.y < 0)
            {
                Vector3 prePos = CalcPositionFromForce(time * (i-1), BulletPrefab.GetComponent<Rigidbody>().mass, muzzle.position, force, Physics.gravity);
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
        Vector3 speed = (force / mass);
        Vector3 position = (speed * time) + (gravity * 0.5f * Mathf.Pow(time, 2));

        return startPosition + position;
    }

    Vector3 CalcImpactPos(Vector3 pos, Vector3 prePos)
    {
        float x, z;
        if ((pos.x - prePos.x) == 0) x = pos.x;
        else x = -prePos.y / (pos.y - prePos.y) * (pos.x - prePos.x) + prePos.x;
        
        if ((pos.z - prePos.z) == 0) z = pos.z;
        else z = -prePos.y / (pos.y - prePos.y) * (pos.z - prePos.z) + prePos.z;

        return new Vector3(x, 0.5f, z);
    }

    public int Life()
    {
        return life;
    }

    public void isDamaged(int enemy_force)
    {
        life -= enemy_force;
        Debug.Log(life);
        lifePanel.UpdateLife(life);
    }

}

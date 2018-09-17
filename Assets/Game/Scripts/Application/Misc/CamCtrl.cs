using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    private Vector2[] oPosition = new Vector2[2];
    private float moveSpeed = 5;
    private float scaleSpeed = 20;

    private const float xMin = -190;
    private const float xMax = 180;
    private const float yMin = 100;
    private const float yMax = 420;
    private const float zMin = -278;
    private const float zMax = -70;

    private Touch oldTouch1;
    private Touch oldTouch2;

    void Start()
    {
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        //ClampPos();
    }

    void FixedUpdate()
    {
        //moveSpeed = 10 + (420 - transform.position.y) / 2;

        //FingerCommand1();

    }

    private void FingerCommand()
    {
#if UNITY_STANDALONE
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += Quaternion.AngleAxis(45, Vector3.up) * new Vector3(x, 0, z);
#endif


#if UNITY_ANDROID
        switch (Input.touchCount)
        {
            case 0:
                break;
            case 1:
                //if (Input.touches[0].phase == TouchPhase.Began)
                //{
                //    oPosition[0] = Input.touches[0].position;
                //}
                //if (Input.touches[0].phase == TouchPhase.Moved)
                //{
                //    transform.Translate(Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(45, Vector3.up) * new Vector3(Input.touches[0].deltaPosition.x * Time.deltaTime, 0, Input.touches[0].deltaPosition.y * Time.deltaTime), Space.World);
                //}
                break;
            case 2:
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    oPosition[0] = Input.touches[0].position;
                }
                if (Input.touches[1].phase == TouchPhase.Began)
                {
                    oPosition[1] = Input.touches[1].position;
                }

                if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
                {
                    if ((Input.touches[0].deltaPosition - Input.touches[1].deltaPosition).magnitude >= 10)//缩放
                    {
                        float deltaMag = (oPosition[0] - oPosition[1]).magnitude - (Input.touches[0].position - Input.touches[1].position).magnitude;
                        if (deltaMag > 0)//缩小
                        {
                            transform.Translate(-transform.forward * scaleSpeed * Time.deltaTime, Space.World);
                            Debug.Log("GO BACK");
                        }
                        else if (deltaMag < 0)//放大
                        {
                            transform.Translate(transform.forward * scaleSpeed * Time.deltaTime, Space.World);
                            Debug.Log("GO FORWARD");
                        }

                    }
                    else//平移
                    {
                        Vector3 tarPos = Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(45, Vector3.up) * new Vector3(Input.touches[0].deltaPosition.x, 0, Input.touches[0].deltaPosition.y);
                        Vector3 midPos = Vector3.Lerp(Vector3.zero, tarPos, 100);
                        transform.Translate(midPos * moveSpeed * Time.deltaTime, Space.World);

                    }
                }

                break;
            default:
                break;
        }
#endif
    }

    private void FingerCommand1()
    {
        if (2 != Input.touchCount)
            return;

        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch2 = newTouch2;
            oldTouch1 = newTouch1;
            return;
        }

        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

        float offset = newDistance - oldDistance;

        if (offset <= 50)
        {
            Vector3 tarPos1 = Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(45, Vector3.up) * new Vector3(Input.touches[0].deltaPosition.x, 0, Input.touches[0].deltaPosition.y);
            Vector3 midPos1 = Vector3.Lerp(Vector3.zero, tarPos1, 100);
            transform.Translate(midPos1 * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            //float scaleFactor = offset / 10f;
            //Vector3 tarPos2 = transform.forward * offset;
            //Vector3 midPos2 = Vector3.Lerp(Vector3.zero, tarPos2, 100f);
            //transform.Translate(midPos2 * Time.deltaTime, Space.World);
            transform.Translate(transform.forward * offset * Time.deltaTime, Space.World);
        }

    }

    private void ClampPos()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -190, 160), Mathf.Clamp(transform.position.y, 100, 420), Mathf.Clamp(transform.position.z, -278, -80));
    }

    public void Translate(Vector2 delPos)
    {
        Vector3 v = transform.position + Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(45, Vector3.up) * new Vector3(delPos.x, 0, delPos.y) * moveSpeed * Time.deltaTime;
        v = new Vector3(Mathf.Clamp(v.x, xMin, xMax), Mathf.Clamp(v.y, yMin, yMax), Mathf.Clamp(v.z, zMin, zMax));
        transform.position = v;
        //Debug.Log("111");
        //transform.Translate(Quaternion.AngleAxis(180, Vector3.up) * Quaternion.AngleAxis(45, Vector3.up) * new Vector3(delPos.x, 0, delPos.y) * moveSpeed * Time.deltaTime, Space.World);
    }

    public void ZoomIn(float delPin)
    {
        Vector3 v = transform.position + transform.forward * delPin * scaleSpeed * Time.deltaTime;
        v = new Vector3(Mathf.Clamp(v.x, xMin, xMax), Mathf.Clamp(v.y, yMin, yMax), Mathf.Clamp(v.z, zMin, zMax));
        transform.position = v;
        //Debug.Log("222");
        //transform.Translate(transform.forward * delPin * scaleSpeed * Time.deltaTime, Space.World);
    }

    public void ZoomOut(float delPin)
    {
        Vector3 v = transform.position - transform.forward * delPin * scaleSpeed * Time.deltaTime;
        v = new Vector3(Mathf.Clamp(v.x, xMin, xMax), Mathf.Clamp(v.y, yMin, yMax), Mathf.Clamp(v.z, zMin, zMax));
        transform.position = v;
        //Debug.Log("333");
        //transform.Translate(-transform.forward * delPin * scaleSpeed * Time.deltaTime, Space.World);
    }

    public void SpawnSoldierOnTouchDown(Vector2 tapPos)
    {
        Arm currentSpawnType = MVC.GetModel<GameModel>().CurrentSpawnType;
        if (currentSpawnType == Arm.NULL)
            return;

        if (Game.Instance.StaticData.GetSoldierInfo(currentSpawnType).Cost > MVC.GetModel<GameModel>().Energy)
            return;

        Vector3 spawnPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(tapPos), out hit))
        {
            if (hit.transform.tag != Tags.GROUND)
                return;

            spawnPos = hit.point;
        }

        if (spawnPos != null)
        {
            SpawnSoldierArgs e1 = new SpawnSoldierArgs() { arm = currentSpawnType, camp = Camp.YELLOW, pos = spawnPos };
            MVC.SendEvent(Consts.E_SpawnSoldier, e1);
        }
    }

    public void SpawnSkillOnTouchDown(Vector2 tapPos)
    {
        GameModel gm = MVC.GetModel<GameModel>();
        Debug.Log(gm.CurrentSkillType);
        if (gm.CurrentSkillType == SkillType.NULL)
            return;

        if (gm.GetSkillState(gm.CurrentSkillType))
            return;

        if (gm.GetSkillCount(gm.CurrentSkillType) <= 0)
            return;

        Vector3 spawnPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(tapPos), out hit))
        {
            //if (hit.transform.tag != Tags.GROUND)
            //    return;

            spawnPos = hit.point;
        }

        if (spawnPos != null)
        {
            SpawnSkillArgs e1 = new SpawnSkillArgs() { skillType = gm.CurrentSkillType, camp = Camp.YELLOW, pos = spawnPos };
            MVC.SendEvent(Consts.E_SpawnSkill, e1);
        }
    }

}

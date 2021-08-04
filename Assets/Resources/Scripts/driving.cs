using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class driving : MonoBehaviour
{

    AudioSource engineSound;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    public GameObject speedometer;
    public GameObject wheel;

    float wheelRotation = 0f;
    float lastVelocity = 0f;

    public static float mileDistance = 1760f;

    float mile_fraction = 0f;
    int miles = 12281;
    public GameObject o_odometer;

    public float carSpeedMult = 900f;
    public GameObject lakitu;

    public bool stall = false;
    public int stallStage = 0;

    float yJogTime = 0f;

    Vector3 lastPosition;

    // Update is called once per frame
    void Update()
    {
        #if UNITY_STANDALONE
        if (Input.GetAxis("Escape") != 0)
        {
            Application.Quit();
        }
        #endif

        if(triggers == 0)
        {
            if(stallSound != null)
            {
                Destroy(stallSound);
            }
            stallTime = 0f;
        }

        if(stallStage == 1)
        {

            GetComponent<AudioSource>().enabled = false;

            // Spawn lakitu at last good point and move towards player.

            int lastStopInMiles = GetComponent<mapper>().magnitude / 6688 * 380;
            float currentPositionInMiles = (GetComponent<mapper>().magnitude * GetComponent<mapper>().magnitudeDistance + transform.position.z) / mileDistance;

            float lastGoodZ = (currentPositionInMiles - lastStopInMiles) * mileDistance;
            lakitu.SetActive(true);
            lakitu.transform.localPosition = new Vector3(0, 0.868f, -lastGoodZ + -0.6f);

            stallStage += 1;

            // Start blinkers
            transform.GetChild(18).GetComponent<blinker>().setBlinkerOn();
            transform.GetChild(19).GetComponent<blinker>().setBlinkerOn();
        }
        else if (stallStage == 2)
        {
            lakitu.transform.localPosition += new Vector3(0, 0, Time.deltaTime * mileDistance * 65f / 3600f);
            if(lakitu.transform.localPosition.z >= -0.6f)
            {
                lakitu.transform.localPosition = new Vector3(0, 0.868f, -0.6f);
                stallStage += 1;
            }
        }


        Rigidbody carBody = this.GetComponent<Rigidbody>();

        //Progress - originally * 200
        float zForce = Input.GetAxis("Vertical") * Time.deltaTime * carSpeedMult;
        float cZForce = (Input.GetAxis("XboxRightTrigger") - Input.GetAxis("XboxLeftTrigger")) * Time.deltaTime * carSpeedMult;
        zForce = Mathf.Abs(zForce) > Mathf.Abs(cZForce) ? zForce : cZForce;

        float pull = -0.15f;
        float xForce = zForce != 0 ? Input.GetAxis("Horizontal") * Time.deltaTime * 100f + pull : 0f;

        float axisHorizontal = Input.GetAxis("Horizontal");
        if (axisHorizontal != 0f)
        {
            if (axisHorizontal > 0f)
            {
                if(wheelRotation < 15f)
                {
                    wheelRotation += 1;
                    wheel.transform.Rotate(new Vector3(0f, 1f, 0f));
                }
            }
            else
            {
                if(wheelRotation > -15f)
                {
                    wheelRotation -= 1;
                    wheel.transform.Rotate(new Vector3(0f, -1f, 0f));
                }
            }
        }
        else
        {
            if(wheelRotation > 0f)
            {
                wheelRotation -= 1;
                wheel.transform.Rotate(new Vector3(0f, -1f, 0f));
            }
            else if(wheelRotation < 0f)
            {
                wheelRotation += 1;
                wheel.transform.Rotate(new Vector3(0f, 1f, 0f));
            }
        }

        if(GetComponent<mapper>().magnitude == 0 && stall)
        {
            setStall(false);
            carBody.velocity = Vector3.zero;
        }

        if (!stall)
        {
            carBody.AddRelativeForce(new Vector3(xForce, 0, zForce));
        }
        else
        {
            if(stallStage == 3)
            {
                float xDiff = -2.75f - transform.position.x;
                if (GetComponent<mapper>().magnitude <= 0)
                {
                    xDiff = 2.75f - transform.position.x;
                }

                Vector3 newXForce = Vector3.RotateTowards(new Vector3(0, 0, Time.deltaTime * 100f), new Vector3(xDiff, 0, 0), 100, 100);
                Vector3 newZForce = new Vector3(0f, 0f, Time.deltaTime * carSpeedMult);
                if (GetComponent<mapper>().magnitude <= 0)
                {
                    carBody.AddRelativeForce(newXForce + newZForce);
                }
                else
                {
                    carBody.AddRelativeForce(newXForce - newZForce);
                }
            }
        }

        if(carBody.velocity.z - lastVelocity < 0.5 && carBody.velocity.z - lastVelocity > -0.5)
        {

        }
        else
        {
            float speedRatio = Mathf.Round(carBody.velocity.z) / 30f;

            speedometer.transform.localRotation = Quaternion.Euler(90f + 110f * speedRatio, 90f, -90f);
            lastVelocity = carBody.velocity.z;
        }
        if(stallStage == 0)
        {
            mile_fraction += Vector3.Distance(transform.position, lastPosition);
        }
        if(mile_fraction >= mileDistance)
        {
            miles += 1;
            mile_fraction = 0;
            Text odometer_text = o_odometer.GetComponent<Text>();
            odometer_text.text = miles + "";
        }

        if(transform.position.x != lastPosition.x || transform.position.z != lastPosition.z)
        {
            yJogTime += Time.deltaTime;

            transform.position = new Vector3(transform.position.x, 0.25f * Mathf.Sin(yJogTime) + 1.7f, transform.position.z);

            if (yJogTime > 2 * Mathf.PI)
            {
                yJogTime -= 2 * Mathf.PI;
            }
        }

        lastPosition = transform.position;

        
        
    }

    public void setStall(bool stall)
    {
        this.stall = stall;
        if (stall)
        {
            if(stallStage == 0)
            {
                stallStage = 1;
            }
        }
        else
        {
            stallStage = 0;
            lakitu.SetActive(false);
            transform.GetChild(18).GetComponent<blinker>().setBlinkerOff();
            transform.GetChild(19).GetComponent<blinker>().setBlinkerOff();

            GetComponent<AudioSource>().enabled = true;
        }
    }

    float stallTime = 0f;
    GameObject stallSound;
    public AudioClip stallClip;

    int triggers = 0;

    public void addStallTime(float addTime)
    {
        if (stallStage == 0)
        {
            if (stallTime == 0f)
            {
                stallSound = Instantiate(new GameObject(), transform);
                stallSound.AddComponent<setVolume>();
                AudioSource toStall = stallSound.AddComponent<AudioSource>();
                toStall.clip = stallClip;
                toStall.loop = false;
                toStall.Play();
            }



            stallTime += addTime;

            if (stallTime > 8.0f)
            {
                setStall(true);
                stallTime = 0f;
                Destroy(stallSound);
            }
        }
    }

    public void addTrigger(int toAdd)
    {
        triggers += toAdd;
    }
}

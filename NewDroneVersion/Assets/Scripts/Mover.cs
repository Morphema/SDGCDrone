using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


public class Mover : MonoBehaviour
{
    public ParticleSystem fire;
	public ParticleSystem r1;
	public ParticleSystem r2;
	public ParticleSystem r3;
	public ParticleSystem r4;
	public ParticleSystem l1;
	public ParticleSystem l2;
	public ParticleSystem l3;
	public ParticleSystem l4;
	public ParticleSystem w1;
	public ParticleSystem w2;
	public ParticleSystem d1;
	public ParticleSystem d2;

    ParticleSystem.EmissionModule fire_em;
    ParticleSystem.EmissionModule r1_em;
    ParticleSystem.EmissionModule r2_em;
    ParticleSystem.EmissionModule r3_em;
    ParticleSystem.EmissionModule r4_em;
    ParticleSystem.EmissionModule l1_em;
    ParticleSystem.EmissionModule l2_em;
    ParticleSystem.EmissionModule l3_em;
    ParticleSystem.EmissionModule l4_em;
    ParticleSystem.EmissionModule w1_em;
    ParticleSystem.EmissionModule w2_em;
    ParticleSystem.EmissionModule d1_em;
    ParticleSystem.EmissionModule d2_em;

    Thread th = null;
    TcpClient tcp = null;
    NetworkStream ns = null;

    bool bj = true;

    double p = 0, r = 0, yw = 0;
    double vr = 0, hr = 0;
    double x, y, z;
    double xa, ya, za;
    double xr, yr, zr;

    double ang_x = 0, ang_y = 0;
    double ang_x_speed = 0, ang_y_speed = 0;

    double ang_za = 0;
    double ang_za_speed = 0;

    Vector3 obj;
    UnityEngine.Random rand = new UnityEngine.Random();
    
    void Start()

    {
        fire_em = fire.emission;
        fire_em.enabled = true;
        fire.Stop();

        r1_em = r1.emission;
        r1_em.enabled = true;
        r1.Stop();

        r2_em = r2.emission;
        r2_em.enabled = true;
        r2.Stop();

        r3_em = r3.emission;
        r3_em.enabled = true;
        r3.Stop();

        r4_em = r4.emission;
        r4_em.enabled = true;
        r4.Stop();

        l1_em = l1.emission;
        l1_em.enabled = true;
        l1.Stop();

        l2_em = l2.emission;
        l2_em.enabled = true;
        l2.Stop();

        l3_em = l3.emission;
        l3_em.enabled = true;
        l3.Stop();

        l4_em = l4.emission;
        l4_em.enabled = true;
        l4.Stop();

        w1_em = w1.emission;
        w1_em.enabled = true;
        w1.Stop();

        w2_em = w2.emission;
        w2_em.enabled = true;
        w2.Stop();

        d1_em = d1.emission;
        d1_em.enabled = true;
        d1.Stop();

        d2_em = d2.emission;
        d2_em.enabled = true;
        d2.Stop();

        x = 0;
        y = 0;
        z = 0;

        xa = 0;
        ya = 0;
        za = 0;

        xr = 0;
        yr = 0;

        //if(tcp == null)
        //{
        //    tcp = new TcpClient("192.168.4.1", 80);
        //    ns = tcp.GetStream();
        //    th = new Thread(rx_thread);
        //    th.Start();
        //}
        
    }

    double DeadSpace(double data, double range)
    {
        if (Math.Abs(data) > range)
        {
            if (data > 0)
                data -= range;

            if (data < 0)
                data += range;
        }

        else
        {
            data = 0;
        }

        return data;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 10, 100, 40), "xa:" + ang_x.ToString("0.##"));
        GUI.Label(new Rect(Screen.width - 100, 20, 100, 40), "ya:" + ya.ToString("0.##"));
        GUI.Label(new Rect(Screen.width - 100, 30, 100, 40), "za:" + ang_za.ToString("0.##"));

        GUI.Label(new Rect(Screen.width - 200, 10, 100, 40), "xs:" + ang_x_speed.ToString("0.##"));
        GUI.Label(new Rect(Screen.width - 200, 20, 100, 40), "y:" + y.ToString("0.##"));
        GUI.Label(new Rect(Screen.width - 200, 30, 100, 40), "z:" + z.ToString("0.##"));

        GUI.Label(new Rect(Screen.width - 200, 50, 100, 40), "bj:" + bj.ToString()); //
        //GUI.Label(new Rect(Screen.width - 200, 60, 100, 40), "c:" + Input.GetKey("c").ToString());
    }
 
    void FixedUpdate()
    {


        if (Input.GetKey("space"))
        { vr = 40; }
        else
        { vr = 0; }

        if (Input.GetKey("a"))
            r = -60;
        else if (Input.GetKey("d")) //Управление под ПК
            r = 60;
        else
            r = 0;

        if (Input.GetKey("w"))
            p = 60;
        else if (Input.GetKey("s"))
            p = -60;
        else
            p = 0;

        if (bj)
        {
            xa = 0;
            ya = 0;

            xr = -p;
            yr = r;

            DeadSpace(xr, 5);

            DeadSpace(yr, 5);
			
			/*		
			if(xr > 0.1){
				d1.SetActive(true);
				d2.SetActive(true);
			}
			else{
				d1.SetActive(false);
				d2.SetActive(false);
			}
			
			if(xr < -0.1){
				w1.SetActive(true);
				w2.SetActive(true);
			}
			else{
				w1.SetActive(false);
				w2.SetActive(false);
			}*/

            if (ang_y > 0.004)
            {
                if (r1.isStopped)
                    r1.Play();
                r1_em.rateOverTime = (float)(ang_y * 3000.0f);

                if (r2.isStopped)
                    r2.Play();
                r2_em.rateOverTime = (float)(ang_y * 3000.0f);

                if (r3.isStopped)
                    r3.Play();
                r3_em.rateOverTime = (float)(ang_y * 3000.0f);

                if (r4.isStopped)
                    r4.Play();
                r4_em.rateOverTime = (float)(ang_y * 3000.0f);
            }
            else
            {
                r1.Stop();
                r2.Stop();
                r3.Stop();
                r4.Stop();
            }

            if (ang_y < -0.004)
            {
                if (l1.isStopped)
                    l1.Play();
                l1_em.rateOverTime = (float)(-ang_y * 3000.0f);

                if (l2.isStopped)
                    l2.Play();
                l2_em.rateOverTime = (float)(-ang_y * 3000.0f);

                if (l3.isStopped)
                    l3.Play();
                l3_em.rateOverTime = (float)(-ang_y * 3000.0f);

                if (l4.isStopped)
                    l4.Play();
                l4_em.rateOverTime = (float)(-ang_y * 3000.0f);
            }
            else
            {
                l1.Stop();
                l2.Stop();
                l3.Stop();
                l4.Stop();
            }

            if (ang_x > 0.004)
            {
                if (d1.isStopped)
                    d1.Play();
                d1_em.rateOverTime = (float)(ang_x * 3000.0f);

                if (d2.isStopped)
                    d2.Play();
                d2_em.rateOverTime = (float)(ang_x * 3000.0f);
            }
            else
            {
                d1.Stop();
                d2.Stop();
            }

            if (ang_x < -0.004)
            {
                if (w1.isStopped)
                    w1.Play();
                w1_em.rateOverTime = (float)(-ang_x * 3000.0f);

                if (w2.isStopped)
                    w2.Play();
                w2_em.rateOverTime = (float)(-ang_x * 3000.0f);
            }
            else
            {
                w1.Stop();
                w2.Stop();
            }


            ang_x -= (ang_x - xr / 50.0f) / 10.0f;
            ang_x_speed += ang_x;

            ang_y -= (ang_y - yr / 50.0f) / 10.0f;
            ang_y_speed += ang_y;

            transform.Rotate(new Vector3((float)ang_x_speed / 100.0f, (float)ang_y_speed / 100.0f, 0));
        }

        else
        {
            xa = r;
            ya = p;

            DeadSpace(xa, 5);
            xa /= 100.0;

            DeadSpace(ya, 5);
            ya /= 100.0;

            xa *= Math.Abs(xa);
            xa /= 0.4;

            ya *= Math.Abs(ya);
            ya /= 0.4;

        }

        zr = hr;

        DeadSpace(zr, 10);
        zr /= 50.0;

        CameraControl.angle += (float)zr;

        za = vr;

        DeadSpace(za, 2);
        za /= 100.0;

        za *= Math.Abs(za);
        za /= 1.6;

        CameraControl.vbr = (float)z / 30.0f;

        if (x < xa)
            x += 0.0008;
        else if(x > xa)
            x -= 0.0008;

        if (y < ya)
            y += 0.0008;
        else if (y > ya)
            y -= 0.0008;

        /*if (z < za)
            z += 0.004;
        else if (z > za)
            z -= 0.004;*/

        ang_za -= (ang_za - za / 50.0f) / 10.0f;
        ang_za_speed += ang_za;

        if (ang_za > 0.00004)
        {
            if(fire.isStopped)
                fire.Play();
            fire_em.rateOverTime = (float)(ang_za * 300000.0f); 
        }
        else
        {
            fire.Stop();
        }

        transform.Translate((float)x, (float)y, (float)ang_za_speed);
    }

    void rx_thread()
    {
        string bf = "";
        while (true)
        {
            int tmp = ns.ReadByte();
            if (tmp != -1)
            {
                bf += (char)tmp;
                if (tmp == '\n')
                {
                    int stp = 0;
                    string st = "";
                    List<string> pr = new List<string> { };
                    for (int i = 0; i < bf.Length; i++)
                    {
                        if (stp == 0 && bf[i] == '\"')
                        {
                            stp = 1;
                        }
                        else if (stp == 1 && bf[i] == '\"')
                        {
                            stp = 0;
                            pr.Add(st);
                            st = "";
                        }
                        else if (stp == 1)
                        {
                            st += bf[i];
                        }
                    }

                    if (pr.Count == 9)
                    {

                        try
                        {
                            p = double.Parse(pr[0]) / 10;
                            r = double.Parse(pr[1]) / 10;
                            yw = double.Parse(pr[2]) / 5;
                            vr = double.Parse(pr[3]) / 2;
                            hr = double.Parse(pr[4]) / 2;

                            if (pr[5] == "1")
                                bj = true;
                            else
                                bj = false;

                            if (pr[7] == "1")
                                CameraControl.cam_rev = true;
                            else
                                CameraControl.cam_rev = false;

                            p = DeadSpace(p, 10);
                            r = DeadSpace(r, 10);
                            yw = DeadSpace(yw, 10);

                            vr = DeadSpace(vr, 3);
                            vr = DeadSpace(vr, 3);
                        }
                        catch
                        {

                        }
                        if (p > 50)
                            p = 50;
                        if (p < -50)
                            p = -50;

                        if (r > 50)
                            r = 50;
                        if (r < -50)
                            r = -50;

                        if (yw > 50)
                            yw = 50;
                        if (yw < -50)
                            yw = -50;

                        if (vr > 50)
                            vr = 50;
                        if (vr < -50)
                            vr = -50;

                        if (hr > 50)
                            hr = 50;
                        if (hr < -50)
                            hr = -50;

                    }

                    bf = "";
                }
            }
        }
    }
}

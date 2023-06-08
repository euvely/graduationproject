using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using UnityEngine;

using LSL;
using LSL4Unity.Utils;
using System.Collections;

public class Player : MonoBehaviour
{
    private Animator animator;

    #region LSL4Unity_inlet
    public string StreamName;
    ContinuousResolver resolver;
    double max_chunk_duration = 2.0;
    private StreamInlet inlet;

    // buffers to pass to LSL when pulling data
    private float[,] data_buffer;
    private double[] timestamp_buffer;
    public float baseline_pow = -999.0f;
    float EEGpow;
    bool isSatisfied = false;

    string StreamName_marker = "LSL4Unity_marker";
    string StreamType_marker = "Markers";
    public bool IrregularRate = true;
    public MomentForSampling moment;
    public static StreamOutlet outlet;
    public static int OV_marker = 811;
    public static double startTime;
    #endregion

    public int wave;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (!StreamName.Equals(""))
            resolver = new ContinuousResolver("name", StreamName);
        else
        {
            UnityEngine.Debug.LogError("Object must specify a name for resolver to lookup a stream.");
            this.enabled = false;
            return;
        }
        StartCoroutine(ResolveExpectedStream());

        var hash_marker = new Hash128();
        hash_marker.Append(StreamName_marker);
        hash_marker.Append(StreamType_marker);
        hash_marker.Append(gameObject.GetInstanceID());
        StreamInfo streamInfo_marker = new StreamInfo(StreamName_marker, StreamType_marker, 1, LSL.LSL.IRREGULAR_RATE,
        channel_format_t.cf_int32, hash_marker.ToString());
        outlet = new StreamOutlet(streamInfo_marker);
        startTime = LSL.LSL.local_clock();
    }

    IEnumerator ResolveExpectedStream()
    {
        var results = resolver.results();
        while (results.Length == 0)
        {
            yield return new WaitForSeconds(.1f);
            results = resolver.results();
        }
        inlet = new StreamInlet(results[0]);
        int n_channels = inlet.info().channel_count();
        // float s_freq = inlet.info().nominal_srate();
        int buf_samples = (int)Mathf.Ceil((float)(inlet.info().nominal_srate() * max_chunk_duration));
        // int buf_samples = (int)Mathf.Ceil((float)(inlet.info().nominal_srate()));
        data_buffer = new float[buf_samples, n_channels]; // float 배열
        timestamp_buffer = new double[buf_samples];
    }

    private void Update()
    {
            Process process = new Process();

            process.StartInfo.FileName = @"pythonw";
            process.StartInfo.Arguments = @"C:\Users\SM-PC\Desktop\졸업프로젝트\unity\cat_ver1\Assets\Suriyun\Scripts\bpf.py";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;

             #region LSL_inlet_update
            // LSL - inlet
            if (inlet != null)
            {
                process.Start();

                int samples_returned = inlet.pull_chunk(data_buffer, timestamp_buffer);
                //Debug.Log("data_buffer: " + data_buffer);
                //Debug.Log("timestamp_buffer: " + timestamp_buffer);
                
                if (samples_returned > 0)
                {
                    //UnityEngine.Debug.Log("timestamp_buffer: " + timestamp_buffer);

                    float x = data_buffer[samples_returned - 1, 0] * 100;
                    float y = data_buffer[samples_returned - 1, 1];
                    float z = data_buffer[samples_returned - 1, 2] * 100;
                    float a = data_buffer[samples_returned - 1, 3] * 100;

                    //Debug.Log("samples_returned: " + samples_returned);
                    //Debug.Log("x: " + data_buffer[samples_returned - 1, 0]);
                    //Debug.Log("x: " + (data_buffer[samples_returned - 1, 0] * 100));

                    var new_scale = new Vector3(x, y, z);
                    EEGpow = new_scale.y;

                    //process.StandardInput.WriteLine(EEGpow);
                    //process.StandardInput.Flush();
                    //process.StandardInput.Close();

                    string output = process.StandardOutput.ReadToEnd();
                    //double f_data = Double.Parse(output);
                    UnityEngine.Debug.Log(output);

                    int l = data_buffer.GetLength(1);

                    for (int i = 0; i < l; i++)
                    {
                        UnityEngine.Debug.Log(data_buffer[1, i]);
                    }
                    // UnityEngine.Debug.Log("EEGpow: " + output);
                    //double s_freq = #;
                    if (baseline_pow == -999.0f) // unset
                    {
                        /*if (EEGpow < 0.0f)
                        {
                            Debug.Log(EEGpow);
                            outlet.push_sample(new int[1] { OV_marker });
                            if (s_freq >= 15 && s_freq <= 30) // alpha
                            {
                                wave = 0;
                                isSatisfied = true;
                            }
                            if (s_freq >= 9 && s_freq <= 14) // beta
                            {
                                wave = 1;
                                isSatisfied = true;
                            }
                            if (s_freq >= 4 && s_freq <= 8) // theta
                            {
                                wave = 2;
                                isSatisfied = true;
                            }
                            if (s_freq >= 1 && s_freq <= 3) // delta
                            {
                                wave = 3;
                                isSatisfied = true;
                            }
                        }*/
                        isSatisfied = true;
                        if (EEGpow != 0.0f)
                        {
                            //Debug.Log(EEGpow);
                            outlet.push_sample(new int[1] { OV_marker });
                            if (EEGpow < -200.0f)
                            {
                                wave = 0;
                                isSatisfied = true;
                            }
                            else if (EEGpow < -160.0f)
                            {
                                wave = 1;
                                isSatisfied = true;
                            }
                            else if (EEGpow < -80.0f)
                            {
                                wave = 2;
                                isSatisfied = true;
                            }
                            else if (EEGpow < 0.0f)
                            {
                                wave = 3;
                                isSatisfied = true;
                            }
                        }
                        else
                            isSatisfied = false;
                    }
                    else
                    {
                        if (EEGpow < baseline_pow)
                        {
                            //UnityEngine.Debug.Log(LSL.LSL.local_clock() - startTime);
                            outlet.push_sample(new int[1] { OV_marker });
                            isSatisfied = true;
                        }
                        else
                            isSatisfied = false;
                    }
                }
            }
            #endregion

            if (isSatisfied)
            {
                if (wave == 0)
                {
                    animator.SetTrigger("doJump");
                }
                else if (wave == 1)
                {
                    animator.SetTrigger("doHi");

                }
                else if (wave == 2)
                {
                    animator.SetTrigger("doSit");

                }
                else if (wave == 3)
                {
                    animator.SetTrigger("isVictory");

                }
            }
        
    }

}


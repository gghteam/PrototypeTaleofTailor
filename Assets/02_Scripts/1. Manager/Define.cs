using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Define
{
    private static CinemachineVirtualCamera vCam;
    private static CinemachineFreeLook flCam;
    private static GameObject player;

    public static CinemachineVirtualCamera VCam
    {
        get
        {
            if(vCam == null)
            {
                vCam = GameObject.FindWithTag("VCam").GetComponent<CinemachineVirtualCamera>();
            }
            return vCam;
        }
    }

    public static CinemachineFreeLook FLCam
    {
        get
        {
            if(flCam == null)
            {
                flCam = GameObject.FindWithTag("FLCam").GetComponent<CinemachineFreeLook>();
            }
            return flCam;
        }
    }

    public static GameObject Player
    {
        get
        {
            if(player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            return player;
        }
    }
}

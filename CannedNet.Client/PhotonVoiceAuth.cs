using System;
using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;

namespace CannedNet.Client;

public class PhotonVoiceAuth : MonoBehaviour
{
    public PhotonVoiceAuth(IntPtr handle) : base(handle)
    {
    }
    
    public bool authValuesSet = false;
    
    private void Update()
    {
        if (Plugin.EnableAdvancedSettings.Value)
        {
            var realtimeAuth = PhotonNetwork.AuthValues;

            if (realtimeAuth != null)
            {
                if (authValuesSet) 
                    return;
                
                PhotonVoiceNetwork.Instance.Client.AuthValues = realtimeAuth;
                authValuesSet = true;
            }
            else
            {
                if (!authValuesSet) 
                    return;
                
                authValuesSet = false;
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class ReadQRCode : MonoBehaviour
{
    private WebCamTexture camTexture;
    private Rect screenRect;
    private Coroutine readValue = null;

    // Start is called before the first frame update
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = 480;
        camTexture.requestedWidth = 360;
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }

    void OnGUI()
    {
        // drawing the camera on screen
        GUIUtility.RotateAroundPivot(90f, new Vector2(Screen.width/2, Screen.height/2));
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        
        if (readValue == null)
        {
            StartCoroutine("ReadValue");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ReadValue()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            // decode the current frame
            var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);

            if (result != null)
            {
                //Doe hier iets met de decoded QR-code
                Debug.Log("DECODED TEXT FROM QR: " + result.Text);
            } else
            {
                Debug.LogFormat("decode was null");
            }
        } catch (Exception ex)
        {
            Debug.LogWarning(ex.Message);
        }

        yield return new WaitForSeconds(0.5f);

        readValue = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using System.Drawing;
using System;
using UnityEngine.UI;
using System.IO;

public class CreateQRCode : MonoBehaviour
{
    //public MeshRenderer material;
    //public GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        watch.Start();

        for (int i = 0; i < 500; i++)
        {
            SaveTextureToFile(GenerateQRCode(i + ""), "QRcode"+i);
        }

        watch.Stop();
        Debug.LogFormat("watch.ElapsedMilliseconds: {0}", watch.ElapsedMilliseconds);
    }

    private void SaveTextureToFile(Texture2D texture2D, string name)
    {
        byte[] bytes = texture2D.EncodeToJPG();
        File.WriteAllBytes(Application.dataPath + "/QRcodes~/" + name + ".jpg", bytes);
    }

    Texture2D GenerateQRCode(string text)
    {
        IBarcodeWriter writer = new BarcodeWriter { Format = BarcodeFormat.QR_CODE };
        var result = writer.Write(text);
        var root = (int)Mathf.Sqrt(result.Length);
        Texture2D qrCode = new Texture2D(root, root);

        for (int y = 0; y < qrCode.height; y++)
        {
            for (int x = 0; x < qrCode.width; x++)
            {
                qrCode.SetPixel(x, y, result[qrCode.width * y + x]);
            }
        }

        qrCode.Apply();

        return qrCode;
    }
}

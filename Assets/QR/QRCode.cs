using System;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCode : MonoBehaviour
{
    public string qrCodeText = "Hello, QR Code!";
    public int width = 256;
    public int height = 256;
    public bool taken = false; // qr ���� � ����
    public bool onTable = false; // ����� �� �����

    void Start()
    {

    }

    void GenerateQRCode()
    {
        // ������� QR-���
        var qrCodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width,
                Margin = 1
            }
        };

        // ���������� ����������� QR-����
        var color32 = qrCodeWriter.Write(qrCodeText);

        // ������������ Color32[] � Texture2D
        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels32(color32);
        tex.Apply();

        // ��������� �������� � ���������
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = tex;
        }
    }

    public void InIt(string text)
    {
        qrCodeText = text;
        GenerateQRCode();
    }
}
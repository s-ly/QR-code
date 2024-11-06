using System;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRCode : MonoBehaviour
{
    public string qrCodeText = "Hello, QR Code!";
    public int width = 256;
    public int height = 256;
    public bool taken = false; // qr взят в руки
    public bool onTable = false; // лежит на столе

    void Start()
    {

    }

    void GenerateQRCode()
    {
        // Создаем QR-код
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

        // Генерируем изображение QR-кода
        var color32 = qrCodeWriter.Write(qrCodeText);

        // Конвертируем Color32[] в Texture2D
        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels32(color32);
        tex.Apply();

        // Применяем текстуру к материалу
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
using System;
using System.Drawing;

namespace Page_Navigation_App.Helper;

public class RandomColor
{
    private string Color;
    private Random random;

    public RandomColor()
    {
        random = new Random();
    }

    public string GenerateColor()
    {
        int randomNumber = random.Next(0, 21);

        switch (randomNumber)
        {
            case 0:
                // Farbe Lila
                Color = "#8A2BE2";
                break;

            case 1:
                // Farbe Blau
                Color = "#00008B";
                break;

            case 2:
                // Farbe Türkis
                Color = "#00CED1";
                break;
            
            case 3:
                // Farbe Aquamarine
                Color = "#66CDAA";
                break;
            
            case 4:
                // Farbe Cyan
                Color = "#00FFFF";
                break;
            
            case 5:
                // Farbe Rose
                Color = "#FFC1C1";
                break;
            
            case 6:
                // Farbe Rot
                Color = "#FF4040";
                break;
            
            case 7:
                // Farbe Orange
                Color = "#FF7F24";
                break;
            
            case 8:
                // Farbe Braun
                Color = "#8B5A2B";
                break;
            
            case 9:
                // Farbe Grau
                Color = "#BEBEBE";
                break;
            
            case 10:
                // Farbe Grün
                Color = "#006400";
                break;
            
            case 11:
                // Farbe SeeGrün
                Color = "#20B2AA";
                break;
            case 12:
                // Farbe Pink
                Color = "#FF1493";
                break;
            case 13:
                // Farbe Gelb
                Color = "#FFD700";
                break;
            case 14:
                // Farbe Hellgelb
                Color = "#FFEC8B";
                break;
            case 15:
                // Farbe Rosa
                Color = "#EE82EE";
                break;
            case 16:
                // Farbe Khaki
                Color = "#CDC673";
                break;
            
            case 17:
                // Farbe Lime
                Color = "#C0FF3E";
                break;
            
            case 18:
                // Farbe Olivgrün
                Color = "#6E8B3D";
                break;
            
            case 19:
                // Farbe Hellblau
                Color = "#87CEFF";
                break;
            
            case 20:
                // Farbe Korall
                Color = "#FF7256";
                break;
        }

        return Color;
    }
}
using System;
using System.Text;

public class Base64Converter
{
    public string ConvertStringToBase64(string text)
    {
        byte[] textAsBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textAsBytes);
    }

    public string ConvertBase64ToString(string base64Text)
    {
        byte[] base64AsBytes = Convert.FromBase64String(base64Text);
        return Encoding.UTF8.GetString(base64AsBytes);
    }
}
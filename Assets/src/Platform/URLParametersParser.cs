using System.Collections.Generic;
using UnityEngine;

public class URLParametersParser : MonoBehaviour
{
    private Dictionary<string, string> parameters = new Dictionary<string, string>();

    [SerializeField] private StringScriptableValue uid;

    [SerializeField] private StringScriptableValue SKey;

    public void TestBase64Decoding(string base64uID) {
        Base64Converter base64Converter = new Base64Converter();
        Debug.Log("Base 64 conversion of base 64 id " + base64uID + " is " + base64Converter.ConvertBase64ToString(base64uID));
    }

    public void ReadUIDFromURL() {
        #if !UNITY_EDITOR && UNITY_WEBGL
        ParseParameters(Application.absoluteURL);
        Base64Converter base64Converter = new Base64Converter();
        uid.SetValue(base64Converter.ConvertBase64ToString(parameters["id"]));
        Debug.Log(uid);
        #endif
    }

    /*public void TestAESCipher(string thingToCipher) {
        string ciphered = Encrypt(thingToCipher, SKey.Value);
        Debug.Log("Ciphered content is " + ciphered);
        string deciphered = Decrypt(ciphered, SKey.Value);
        Debug.Log("Deciphered content is " + deciphered);
    }

    public static string Encrypt(string clearText, string key)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public static string Decrypt(string cipherText, string key)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }*/

    public void TestURL(string url) 
    {
        ParseParameters(url);
        uid.SetValue(parameters["id"]);
        Debug.Log(uid);
    }

    public void ParseParameters(string url)
    {
        string parametersString = url.Split('?')[1];
        string[] parameterPairs = parametersString.Split('&');
        for (int i = 0; i < parameterPairs.Length; i++)
        {
            string[] parameterValuePair = parameterPairs[i].Split("=");
            parameters.Add(parameterValuePair[0], parameterValuePair[1]);
        }
    }

    public void PrintParameters() {
        foreach(KeyValuePair<string, string> pair in parameters)
        {
            Debug.Log(pair.Key + " = " + pair.Value);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encryptor
{
    private static Encryptor instance;
    public static Encryptor Singleton
    {
        get
        {
            if(instance == null)
            {
                instance = new Encryptor();
            }
            return instance;
        }
    }

    public string EncryptOrDecrypt(string data, string key)
    {
        int dataLength = data.Length;
        int keyLength = key.Length;
        char[] output = new char[dataLength];

        for (int i = 0; i < dataLength; i++) 
        {
            output[i] = (char)(data[i] ^ key[i % keyLength]);
        }

        var result = new string(output);
        return result; 
    }
}

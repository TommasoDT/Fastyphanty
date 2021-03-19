using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class Tools
{
    public static GeneralData generalData = LoadOrNewGeneralData();

    public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            return min;
        if (value.CompareTo(max) > 0)
            return max;

        return value;
    }

    public static bool isInCameraView(Camera camera, Vector3 point)
    {
        if (isInCameraXView(camera, point) && isInCameraYView(camera, point) && isInCameraZView(camera, point))
            return true;
        return false;
    }

    public static bool isInCameraXView(Camera camera, Vector3 point)
    {
        if (xDistanceFromCamera(camera, point) == 0)
            return true;
        return false;
    }

    public static bool isInCameraYView(Camera camera, Vector3 point)
    {
        if (yDistanceFromCamera(camera, point) == 0)
            return true;
        return false;
    }

    public static bool isInCameraZView(Camera camera, Vector3 point)
    {
        if (zDistanceFromCamera(camera, point) == 0)
            return true;
        return false;
    }

    public static float xDistanceFromCamera(Camera camera, Vector3 point)
    {
        Vector3 viewportPosition = camera.WorldToViewportPoint(point);
        if (viewportPosition.x > 1)
            return viewportPosition.x - 1;
        else if (viewportPosition.x < 0)
            return viewportPosition.x;
        else
            return 0;
    }
    public static float yDistanceFromCamera(Camera camera, Vector3 point)
    {
        Vector3 viewportPosition = camera.WorldToViewportPoint(point);
        if (viewportPosition.y > 1)
            return viewportPosition.y - 1;
        else if (viewportPosition.y < 0)
            return viewportPosition.y;
        else
            return 0;
    }
    public static float zDistanceFromCamera(Camera camera, Vector3 point)
    {
        Vector3 viewportPosition = camera.WorldToViewportPoint(point);
        if (viewportPosition.z > 10)
            return viewportPosition.z - 10;
        else if (viewportPosition.z < 0)
            return viewportPosition.z;
        else
            return 0;
    }

    public static Vector2 findv2fromX(List<Vector2> lista,int offset,float x)
    {
        for(int i=offset;i<lista.Count;i++)
        {
            if(Math.Round(lista[i][0]) == Math.Round(x))
            {
                return lista[i];
            }
            
        }
        return lista[0];
    }

    public static void TraslateY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y+y, transform.position.z);
    }

    public static bool isAlmostEqualTo(float number1, float number2, float precision)
    {
        if(Math.Abs(number1 - number2) < precision)
            return true;
        return false;
    }

    public static void saveCar(CarControls car)
    {
       BinaryFormatter formatter =new BinaryFormatter();
       string path = Application.persistentDataPath + "/Car.dat";
       FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

       CarData data = new CarData(car);

       formatter.Serialize(stream,data );
       stream.Close();
    }

    public static CarData LoadCar()
    {
        string path = Application.persistentDataPath + "/Car.dat";
        CarData data = new CarData();
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = (CarData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("savefile not found " + path);
            return data;
        }
    }

    public static void SaveGeneralData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/GeneralData.dat", FileMode.OpenOrCreate);

        binaryFormatter.Serialize(stream, generalData);
        stream.Close();
    }

    public static GeneralData LoadOrNewGeneralData()
    {
        string path = Application.persistentDataPath + "/GeneralData.dat";
        if(File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            return (GeneralData)binaryFormatter.Deserialize(stream);
        }
        else
        {
            return new GeneralData();
        }
    }
}

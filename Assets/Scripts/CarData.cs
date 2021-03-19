using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarData 
{
    public int MaxtorquevariableR = 5000;//torque massimo variabile
    public int MaxtorquevariableF = 5000;//torque massimo variabile
    public int MaxtorqueairF = 4000;//torque massimo per l'aria
    public int MaxtorqueairR = 4000;//torque massimo per l'aria
    public float Brakefront = 30;//potenza freno anteriore
    public float Brakerear = 30;//potenza freno posteriore
    public float torqueFdv = 7;//valore che divide il max torque per ottenere il valore di accelerazione
    public float torqueRdv = 7;//valore che divide il max torque per ottenere il valore di accelerazione
    public float rotation_powa = 5f;//potenza di rotazione
    public float fuel=5000;//fuel di partenza
    public int fuelvalue=1000;
    public int maxfuel=6000;
    public int divisoreconsumo=3000;//determina il consumo del carburante rapportato alla potenza , insomma l'efficenza , più alto = più efficiente
    public float[] cmass= {0,1,0};//massa del centro di gravità
    public float frequency_front = 2;
    public float frequency_back = 2;
    public float dampening_front;
    public float dampening_back;
    public float height_front = -4;
    public float height_back = -4;
    public float gripR = 3;
    public float gripF = 3;
    public int lvFsh = 1;
    public int lvRsh = 1;
    public int lvFgr = 1 ;
    public int lvRgr = 1 ;
    public int lvE = 1 ;
    public int lvT = 1 ;
    public int lvG = 1 ;

    public CarData(CarControls car)
    {
        
        MaxtorquevariableR = car.MaxtorquevariableR;//torque massimo variabile
        MaxtorquevariableF = car.MaxtorquevariableF;//torque massimo variabile
        MaxtorqueairF = car.MaxtorqueairF;//torque massimo per l'aria
        MaxtorqueairR = car.MaxtorqueairR;//torque massimo per l'aria
        Brakefront = car.Brakefront;//potenza freno anteriore
        Brakerear = car.Brakerear;//potenza freno posteriore
        torqueFdv = car.torqueFdv;//valore che divide il max torque per ottenere il valore di accelerazione
        torqueRdv = car.torqueRdv;//valore che divide il max torque per ottenere il valore di accelerazione
        rotation_powa = car.rotation_powa;//potenza di rotazione
        fuel = car.fuel;//fuel di partenza
        fuelvalue = car.fuelvalue;
        maxfuel = car.maxfuel;
        divisoreconsumo = car.divisoreconsumo;//determina il consumo del carburante rapportato alla potenza , insomma l'efficenza , più alto = più efficiente
        cmass = car.cmass;//massa del centro di gravità
        frequency_front = car.frequency_front;
        frequency_back = car.frequency_back;
        dampening_front = car.dampening_front;
        dampening_back = car.dampening_back;
        height_front = car.height_front;
        height_back = car.height_back;
        lvFsh = car.lvFsh;
        lvRsh = car.lvRsh;
        lvFgr = car.lvFgr;
        lvE = car.lvE;
        lvT = car.lvT;
        lvG = car.lvG;
        gripF = car.gripF;
        gripR = car.gripR;
        
    }
    public CarData()
    {

    }
}

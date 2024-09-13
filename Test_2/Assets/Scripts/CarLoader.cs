using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] CarPrefabs;
    [SerializeField] private Transform CarParent;
    [SerializeField] private CarUI CarUI;

    private JsonData _data;
    private int _idx;
    private List<Car> _carList;

    private void Start()
    {
        _carList = new List<Car>();
        for(int i = 0; i < CarPrefabs.Length; i++)
        {
            var car = Instantiate(CarPrefabs[i], CarParent).GetComponent<Car>();
            car.gameObject.SetActive(false);
            _carList.Add(car);
        }

        var file = (TextAsset)Resources.Load("car_information");
        _data = JsonConvert.DeserializeObject<JsonData>(file.text);
        LoadCar();
    }

    public void ChangeCar()
    {
        _idx = (_idx + 1) % _data.cars.Length;
        LoadCar();
    }

    private void LoadCar()
    {
        Debug.Log(_data.cars[_idx].name);
        for(int i = 0; i < _carList.Count; i++)
        {
            _carList[i].gameObject.SetActive(i == _idx);
        }
        CarUI.SetCarInfo(_idx, _data.cars[_idx]);
    }
}

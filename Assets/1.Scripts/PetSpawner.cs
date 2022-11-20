using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    private Options _options;

    public static PetSpawner Instance { get; private set; }
    private PetConfig _curPetConfig;
    private Dictionary<PetConfig, Pet> _petMap = new Dictionary<PetConfig, Pet>();
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _options = GameConfig.Instance.Options;
        _options.OptionsChanged.AddListener(OnOptionsChanged);
        
        if (_options.Pet != null)
        {
            _curPetConfig = _options.Pet;
            Pet pet = Instantiate(_curPetConfig.PetPrefab, transform);
            _petMap.Add(_curPetConfig, pet);
        }
    }

    public Pet GetPet()
    {
        return _curPetConfig != null ? 
            _petMap[_curPetConfig] : null;
    }
    

    private void OnOptionsChanged()
    {
        if (_curPetConfig != _options.Pet)
        {
            if (_curPetConfig != null)
                _petMap[_curPetConfig].gameObject.SetActive(false);

            _curPetConfig = _options.Pet;
            
            if (_curPetConfig == null)
                return;

            if (_petMap.ContainsKey(_curPetConfig))
            {
                _petMap[_curPetConfig].gameObject.SetActive(true);
            }

            else
            {
                Pet pet = Instantiate(_curPetConfig.PetPrefab, transform);
                _petMap.Add(_curPetConfig, pet);
            }
        }
    }
}

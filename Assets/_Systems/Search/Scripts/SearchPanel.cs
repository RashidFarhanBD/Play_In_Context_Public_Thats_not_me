using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchPanel : Singleton<SearchPanel>
{
    [SerializeField] private GameObject _body;
    [SerializeField] private TMPro.TMP_InputField _inputField;
    [SerializeField] private Transform _profilesParent;
    [SerializeField] private Transform _friendsParent;
    [SerializeField] private PenguinProfileButton _penguinProfileButtonPrefab;
    [SerializeField] private string[] _unlockedPenguins;

    private Dictionary<PenguinData, bool> _penguinUnlockTable = new();
    private Dictionary<PenguinData, PenguinProfileButton> _penguinButtonsTable = new();

    public GameObject Body => _body;

    protected override void Awake()
    {
        base.Awake();

        _inputField.onValueChanged.AddListener(CheckInput);
    }

    public void RegisterPenguin(PenguinData penguin, Action ActionOnClick)
    {
        var result = _penguinUnlockTable.TryAdd(penguin, false);

        if (!result)
        {
            Debug.LogWarning($"No Duplicates please: {penguin.DisplayName}");

            var existingButton = _penguinButtonsTable[penguin];
            HandleButton(existingButton);
            return;
        }



        var context = penguin.ToContext();
        var profileButton = Instantiate(_penguinProfileButtonPrefab, _profilesParent);

        profileButton.Initialize(context);

        HandleButton(profileButton);

        _penguinButtonsTable.TryAdd(penguin, profileButton);

        profileButton.gameObject.SetActive(false);

        void GeneralActionOnClick()
        {
            UnlockPenguin(penguin);
        }

        void HandleButton(PenguinProfileButton penguinProfileButton)
        {
            penguinProfileButton.OnButtonClicked += ActionOnClick;
            penguinProfileButton.OnButtonClicked += GeneralActionOnClick;
        }
    }

    private void UnlockPenguin(PenguinData penguin)
    {
        _penguinUnlockTable[penguin] = true;

        _inputField.text = string.Empty;

        var correspondingButton = _penguinButtonsTable[penguin];

        var buttonTransform = correspondingButton.transform;
        buttonTransform.SetParent(_friendsParent);
        buttonTransform.SetAsFirstSibling();
    }

    private void CheckInput(string input)
    {
        var formattedInput = input.ToIdentifier();

        var lockedPenguins = _penguinUnlockTable.Where((x) => x.Value == false);

        foreach (var lockedPenguin in lockedPenguins)
        {
            var penguin = lockedPenguin.Key;

            var show = penguin.ID == formattedInput || "@" + penguin.ID == formattedInput;

            _penguinButtonsTable[penguin].gameObject.SetActive(show);
        }
    }
}

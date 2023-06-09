using LoETool.Infrastructure;
using LoETool.Models;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LoETool.ViewModels;

public class MainViewModel : ViewModelBase
{
    // properties

    private bool _greenFound = false;
    public bool GreenFound
    {
        get => _greenFound;
        set => SetProperty(ref _greenFound, value);
    }

    private bool _redFound = false;
    public bool RedFound
    {
        get => _redFound;
        set => SetProperty(ref _redFound, value);
    }

    private bool _bodyFound = false;
    public bool BodyFound
    {
        get => _bodyFound;
        set => SetProperty(ref _bodyFound, value);
    }

    private bool _weaponFound = false;
    public bool WeaponFound
    {
        get => _weaponFound;
        set => SetProperty(ref _weaponFound, value);
    }

    private List<string>? _bodyLocations;
    public List<string> BodyLocations
    {
        get => _bodyLocations!;
        set => SetProperty(ref _bodyLocations, value);
    }

    private string? _selectedLocation;
    public string? SelectedLocation
    {
        get => _selectedLocation;
        set => SetProperty(ref _selectedLocation, value);
    }

    private List<string>? _weapons;
    public List<string> Weapons
    {
        get => _weapons!;
        set => SetProperty(ref _weapons, value);
    }

    private string? _selectedWeapon;
    public string? SelectedWeapon
    {
        get => _selectedWeapon;
        set => SetProperty(ref _selectedWeapon, value);
    }

    private List<Murderer>? _murderers;
    public List<Murderer> Murderers
    {
        get => _murderers!;
        set => SetProperty(ref _murderers, value);
    }

    // Commmands

    public override void Cancel() => Application.Current.Shutdown();

    public override void Ok() { }

    private ICommand? _resetCommand;
    public ICommand ResetCommand
    {
        get
        {
            _resetCommand ??= new Command(execute: () => Reset());
            return _resetCommand;
        }
        set => _resetCommand = value;
    }

    private ICommand? _incrementCommand;
    public ICommand IncrementCommand
    {
        get
        {
            _incrementCommand ??= new Command<Murderer>(
                execute: x => x.Count++
                );
            return _incrementCommand;
        }
        set => _incrementCommand = value;
    }

    // methods

    private void Reset()
    {
        GreenFound = false;
        RedFound = false;
        BodyFound = false;
        SelectedLocation = null;
        WeaponFound = false;
        SelectedWeapon = null;
        Murderers.ForEach(x => x.Count = 0);
    }

    private void FatalError(string msg)
    {
        MessageBox.Show("Fatal Error", msg, MessageBoxButton.OK, MessageBoxImage.Stop);
        Cancel();
    }

    public MainViewModel() : base()
    {
        var config = ConfigurationFactory.Create();
        var locations = config.GetSection("Locations").Get<string[]>();
        if (locations is null || !locations.Any())
        {
            FatalError("No locations found in configuration file.");
        }
        BodyLocations = new(locations!);
        var weapons = config.GetSection("Weapons").Get<string[]>();
        if (weapons is null || !weapons.Any())
        {
            FatalError("No weapons found in configuration file.");
        }
        Weapons = new(weapons!);
        var perps = config.GetSection("Perpetrators").Get<string[]>();
        if (perps is null || !perps.Any())
        {
            FatalError("No perpetrators found in configuration file.");
        }
        if (perps!.Length != 8)
        {
            FatalError("There must be eight perpetrators.");
        }
        Murderers = new();
        perps!.ToList().ForEach(x => Murderers.Add(new(x, 0)));
    }
}

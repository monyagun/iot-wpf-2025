﻿using BusanRestaurantApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusanRestaurantApp.ViewModels
{
    public class GoogleMapViewModel : ObservableObject
    {
        private BusanItem _selectedMatjibItem;
        private string _matjibLoation;

        public GoogleMapViewModel()
        {
            MatjibLocation = "";

        }

        public BusanItem SelectedMatjibItem
        {
            get => _selectedMatjibItem;
            set {
                SetProperty(ref _selectedMatjibItem, value);
                MatjibLocation = $"https://google.com/maps/place/{SelectedMatjibItem.Lat},{SelectedMatjibItem.Lng}";
            }
        }

        public string MatjibLocation
        {
            get => _matjibLoation;
            set => SetProperty(ref _matjibLoation, value);
        }


    }
}

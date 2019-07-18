﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public NewItemPage()
        {
            InitializeComponent();
            viewModel = new NewItemViewModel(new Item());
            BindingContext = viewModel;
        }

        public NewItemPage(Item item)
        {
            InitializeComponent();
            viewModel = new NewItemViewModel(item);
            BindingContext = viewModel;
        }
    }
}
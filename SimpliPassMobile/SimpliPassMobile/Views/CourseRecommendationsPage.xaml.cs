﻿using System;
using System.Collections.Generic;
using SimpliPassMobile.ViewModels;
using Xamarin.Forms;

namespace SimpliPassMobile.Views
{
    public partial class CourseRecommendationsPage : ContentPage
    {
        public CourseRecommendationsPage()
        {
            InitializeComponent();
            BindingContext = new CourseRecommendationsViewModel();
        }
    }
}

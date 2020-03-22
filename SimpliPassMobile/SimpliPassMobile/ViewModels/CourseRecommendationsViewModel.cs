﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpliPassMobile.Models;

namespace SimpliPassMobile.ViewModels
{
    /// <summary>
    /// ViewModel for Course recommendations page
    /// </summary>
    class CourseRecommendationsViewModel
    {
        public ObservableCollection<CourseModel> RecommendationsList { get; set; }
        private List<object> courseList;

        public CourseRecommendationsViewModel()
        {
            RecommendationsList = new ObservableCollection<CourseModel>();
            GenerateRecommendationsList();
        }

        /// <summary>
        /// Method that requests the list of all course recommendations
        /// </summary>
        private void GenerateRecommendationsList()
        {
            RecommendationsList = new ObservableCollection<CourseModel>();

            var response = SimpliPassHttpConnection.GetResource(Constants.COURSE + Constants.RECOMMENDATIONS);
            courseList = JsonConvert.DeserializeObject<List<object>>(response);

            foreach (var crs in courseList)
            {
                var id = JObject.Parse(crs.ToString())["id"].ToObject<string>();
                var name = JObject.Parse(crs.ToString())["name"].ToObject<string>();
                var dept = JObject.Parse(crs.ToString())["department"].ToObject<string>();
                var diff = JObject.Parse(crs.ToString())["difficulty"].ToObject<double>();
                var diffCount = JObject.Parse(crs.ToString())["difficultyCount"].ToObject<int>();
                var secRatings = JObject.Parse(crs.ToString())["sectionRatings"].ToObject<Dictionary<string, Dictionary<string, double>>>();

                RecommendationsList.Add(new CourseModel { Id = id, Name = name, Department = dept, Difficulty = diff, DifficultyCount = diffCount, SectionRatings = secRatings });
            }
        }
    }
}

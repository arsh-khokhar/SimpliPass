﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;

namespace SimpliPassApi.Models
{
    public class Course
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("id")]
        public string Id { get; set; }

        [DynamoDBProperty("department")]
        public string Department { get; set; }

        [DynamoDBProperty("difficulty")]
        public double Difficulty { get; set; }

        [DynamoDBProperty("difficulty_count")]
        public int DifficultyCount { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("section_ratings")]
        public Dictionary<string, Dictionary<string, double>> SectionRatings { get; set; }

        public void UpdateDifficulty(int newDifficulty)
        {
            Difficulty = ComputeUpdatedDifficulty(newDifficulty);
            DifficultyCount = DifficultyCount + 1;
        }

        private double ComputeUpdatedDifficulty(double newDifficulty)
        {
            double result = ((Difficulty * DifficultyCount) + newDifficulty) / (DifficultyCount + 1);
            result = Math.Round(result, 1);

            return result;
        }

        public void UpdateSectionRating(string instructorName, int newRating)
        {
            Boolean flag = false;

            foreach (var item in SectionRatings)
            {
                if (item.Key.ToUpper() == instructorName.ToUpper())
                {
                    item.Value["rating"] = ComputeUpdatedRating(item.Value["rating"], newRating, item.Value["count"]);
                    item.Value["count"] = item.Value["count"] + 1;
                    flag = true;
                }
            }

            if (!flag) // This is a new instructor, add new item to section ratings list
            {
                var pairs = new Dictionary<string, double>();

                pairs.Add("count", 1);
                pairs.Add("rating", newRating);

                SectionRatings.Add(instructorName, pairs);
            }
        }

        private double ComputeUpdatedRating(double oldRating, double newRating, double count)
        {
            double result = ((oldRating * count) + newRating) / (count + 1);
            result = Math.Round(result, 1);

            return result;
        }

    }
}
﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using SimpliPassApi.Models;

namespace SimpliPassApi.Clients
{
    public interface IDynamoDBClient
    {
        public Task<List<Course>> GetCourses();
        public Task<Course> GetCourse(string key);
        public void UpdateCourseDifficulty(string key, int newDifficulty);
    }

    public class DynamoDBClient : IDynamoDBClient
    {
        private readonly IDynamoDBContext _context;

        public DynamoDBClient(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<List<Course>> GetCourses()
        {
            var courses = await _context.ScanAsync<Course>(new List<ScanCondition>()).GetRemainingAsync();
            return courses;
        }

        public async Task<Course> GetCourse(string key)
        {
            var item = await _context.LoadAsync<Course>(key);
            return item;
        }

        public async void UpdateCourseDifficulty(string key, int newDifficulty)
        {
            var item = await _context.LoadAsync<Course>(key);

            if (item != null)
            {
                item.Difficulty = item.ComputeUpdatedDifficulty(newDifficulty);
                item.DifficultyCount = item.DifficultyCount + 1;

                await _context.SaveAsync(item);
            }
        }
    }
}
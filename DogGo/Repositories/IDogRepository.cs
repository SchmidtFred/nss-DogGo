﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int Id);
        void DeleteDog(int dogId);
        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
    }
}
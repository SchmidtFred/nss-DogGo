using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetAllWalks();
        Walk GetWalkById(int id);
        List<Walk> GetWalksByWalker(int walkerId);
        void AddWalk(Walk walk);
        void DeleteWalk(int walkId);
    }
}

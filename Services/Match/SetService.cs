using DataAccessLayer.Data.DTO;
using DataAccessLayer.Data;
using Services.Match.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Match
{
    public class SetService : ISetService
    {
        private readonly ApplicationDbContext _dbContext;

        public SetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SetsDTO CreateSet(SetsDTO setDTO)
        {
            var set = new DataAccessLayer.Data.Models.TableTennisSet
            {
                Player1FirstName = setDTO.Player1FirstName,
                Player1LastName = setDTO.Player1LastName,
                Player2FirstName = setDTO.Player2FirstName,
                Player2LastName = setDTO.Player2LastName,
                Player1Age = setDTO.Player1Age,
                Player2Age = setDTO.Player2Age,
                SetGender = setDTO.SetGender,
                MatchDate = setDTO.MatchDate
            };
            _dbContext.Sets.Add(set);
            _dbContext.SaveChanges();

            return setDTO;
        }
    }
}

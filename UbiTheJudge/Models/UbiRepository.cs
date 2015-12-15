using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UbiTheJudge.Models
{
    public class UbiRepository
    {
        public UbiContext _context;

        public UbiRepository()
        {
            _context = new UbiContext();
        }

        public UbiRepository(UbiContext a_context)
        {
            _context = a_context;
        }

        public List<UbiUser> GetAllUsers()
        {
            var query = from users in _context.UbiUsers select users;
            return query.ToList();
        }
    }
}
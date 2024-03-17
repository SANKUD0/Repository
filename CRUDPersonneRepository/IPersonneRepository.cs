using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPersonneRepository
{
    public interface IPersonneRepository
    {
        IEnumerable<Personne> GetAll();
        Personne GetById(int Id);
        IEnumerable<Personne> GetByWork(string Work);
        IEnumerable<Personne> GetByCountry(string Country);
        void Add(Personne NouveauPersonne);
        void Update(Personne personne);
        void Delete(int Id);
    }
}

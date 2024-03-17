using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CRUDPersonneRepository
{
    public class PersonneRepository : IPersonneRepository
    {
        private readonly string _filePath = "..\\..\\..\\Données\\Personne.json";

        #region Méthodes
        /// <summary>
        /// Permet de récuperer les données dans la base de donnée
        /// </summary>
        /// <returns>Liste des personnes</returns>
        private List<Personne> Load()
        {
            if (!File.Exists(_filePath))
                return new List<Personne>();
            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Personne>>(jsonData) ?? new List<Personne>();
        }
        /// <summary>
        /// Permet de sauvegarder les modifications
        /// </summary>
        /// <param name="_lstPersonne">Liste des personnes</param>
        private void Save(List<Personne> _lstPersonne)
        {
            var jsonData = JsonConvert.SerializeObject(_lstPersonne, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
        /// <summary>
        /// Permet d'ajouter une personne dans la base de donnée
        /// </summary>
        /// <param name="NouveauPersonne">Nouveau personne à ajouter dans la base de donnée</param>
        public void Add(Personne NouveauPersonne)
        {
            var groupe = Load();
            NouveauPersonne.Id = groupe.Any() ? groupe.Max(p => p.Id) + 1 : 1;
            groupe.Add(NouveauPersonne);
            Save(groupe);
        }
        /// <summary>
        /// Permet de supprimer une personne de la base de donnée
        /// </summary>
        /// <param name="Id">Identifiant de la personne</param>
        public void Delete(int Id)
        {
            var  groupe = Load();
            var personne = groupe.FirstOrDefault(p => p.Id == Id);
            if (personne != null) 
            {
                groupe.Remove(personne);
                Save(groupe);
            }
        }
        /// <summary>
        /// Permet d'avoir la liste des personnes
        /// </summary>
        /// <returns>Liste des personnes dans la base de donnée</returns>
        public IEnumerable<Personne> GetAll()
        {
            return Load();
        }
        /// <summary>
        /// Permet d'avoir la liste des personnes dans le même pays
        /// </summary>
        /// <param name="Country">Nom du pays</param>
        /// <returns>Liste des personnes dans le même pays</returns>
        public IEnumerable<Personne> GetByCountry(string Country)
        {
            return Load().Where(p => p.Country == Country).ToList()!;
        }
        /// <summary>
        /// Permet d'avoir une personne spécifique
        /// </summary>
        /// <param name="Id">Identifiant de la personne</param>
        /// <returns>Personne choisi par son identifiant</returns>
        public Personne GetById(int Id)
        {
            return Load().FirstOrDefault(p => p.Id == Id)!;
        }
        /// <summary>
        /// Permet d'avoir la liste des personnes qui travail
        /// </summary>
        /// <param name="Work">Type de travail</param>
        /// <returns>Avoir la liste des personne qui on le même travail</returns>
        public IEnumerable<Personne> GetByWork(string Work)
        {
            return Load().Where(p => p.Work == Work).ToList()!;
        }
        /// <summary>
        /// Permet de faire des mise à jours sur la personne choisi
        /// </summary>
        /// <param name="personne">Personne choisi</param>
        public void Update(Personne personne)
        {
            var groupe = Load();
            var index = groupe.FindIndex(p => p.Id == personne.Id);
            if(index != -1)
            {
                groupe[index] = personne;
                Save(groupe);
            }
        }
        #endregion

    }
}

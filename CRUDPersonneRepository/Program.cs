namespace CRUDPersonneRepository
{
    internal class Program
    {
        private static IPersonneRepository repository = new PersonneRepository();
        static void Main(string[] args)
        {
            bool continuer = true;

            while (continuer)
            {
                Console.WriteLine("\n--- Gestion des Personnes ---");
                Console.WriteLine("1. Ajouter une personne");
                Console.WriteLine("2. Supprimer une personne");
                Console.WriteLine("3. Modifier une personne");
                Console.WriteLine("4. Trouver par ID");
                Console.WriteLine("5. Trouver par Pays");
                Console.WriteLine("6. Trouver par Travail");
                Console.WriteLine("7. Afficher toutes les personnes");
                Console.WriteLine("8. Quitter");
                Console.Write("Choisissez une option: ");
                string option = Console.ReadLine()!;

                switch (option)
                {
                    case "1":
                        AjouterPersonne();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        SupprimerPersonne();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        ModifierPersonne();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        TrouverParID();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        TrouverParPays();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "6":
                        TrouverParTravail();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "7":
                        AfficherToutesLesPersonnes();
                        Console.WriteLine("\nAppuyer sur une touche ...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "8":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Option non valide. Veuillez essayer à nouveau.");
                        break;
                }
            }
        }
        #region Méthodes

        static void AjouterPersonne()
        {
            var personne = new Personne();

            Console.Write("Prénom: ");
            personne.FistName = Console.ReadLine();

            Console.Write("Nom: ");
            personne.LastName = Console.ReadLine();

            Console.Write("Âge: ");
            personne.Age = int.Parse(Console.ReadLine());

            Console.Write("Adresse: ");
            personne.Address = Console.ReadLine();

            Console.Write("Travail (optionnel): ");
            personne.Work = Console.ReadLine();

            Console.Write("Pays: ");
            personne.Country = Console.ReadLine();

            repository.Add(personne);
            Console.WriteLine("Personne ajoutée avec succès.");
        }

        static void SupprimerPersonne()
        {
            Console.Write("Entrez l'ID de la personne à supprimer: ");
            int id = int.Parse(Console.ReadLine());
            repository.Delete(id);
            Console.WriteLine("Personne supprimée avec succès.");
        }

        static void ModifierPersonne()
        {
            bool error = false;
            Console.Write("Entrez l'ID de la personne à modifier: ");
            int id = int.Parse(Console.ReadLine());
            var personne = repository.GetById(id);

            if (personne == null)
            {
                Console.WriteLine("Personne non trouvée.");
                return;
            }
            do
            {
                error = false;
                Console.Write("Prénom (" + personne.FistName + "): ");
                var input0 = Console.ReadLine();
                if (!string.IsNullOrEmpty(input0))
                    personne.FistName = input0;
                else error = true;
            } while (error);

            do
            {
                error = false;
                Console.Write("Nom (" + personne.LastName + "): ");
                var input1 = Console.ReadLine();
                if (!string.IsNullOrEmpty(input1))
                    personne.LastName = input1;
                else error = true;
            } while (error);

            do
            {
                error = false;
                Console.Write("Âge (" + personne.Age + "): ");
                var input2 = Console.ReadLine();
                if (!string.IsNullOrEmpty(input2))
                    if (int.TryParse(input2, out int input2_1))
                        personne.Age = input2_1;
                    else Console.WriteLine("ERREUR : chiffre seulement !");
                else error = true;
            } while (error);

            do
            {
                error = false;
                Console.Write("Adresse (" + personne.Address + "): ");
                var input3 = Console.ReadLine();
                if (!string.IsNullOrEmpty(input3))
                    personne.Address = input3;
                else error = true;
            } while (error);

            Console.Write("Travail (" + (personne.Work ?? "N/A") + "): ");
            var input4 = Console.ReadLine();
            if (!string.IsNullOrEmpty(input4))
                personne.Work = input4;
            else
                personne.Work = null;

            do
            {
                error = false;
                Console.Write("Pays (" + personne.Country + "): ");
                var input5 = Console.ReadLine();
                if (!string.IsNullOrEmpty(input5))
                    personne.Country = input5;
                else error = true;
            } while (error);

            repository.Update(personne);
            Console.WriteLine("Personne modifiée avec succès.");
        }

        static void TrouverParID()
        {
            Console.Write("Entrez l'ID de la personne à trouver: ");
            int id = int.Parse(Console.ReadLine());
            var personne = repository.GetById(id);

            if (personne != null)
                AfficherPersonne(personne);
            else
                Console.WriteLine("Personne non trouvée.");
        }

        static void TrouverParPays()
        {
            Console.Write("Entrez le pays: ");
            string pays = Console.ReadLine();
            var personnes = repository.GetByCountry(pays);

            if (personnes.Any())
                foreach (var personne in personnes)
                    AfficherPersonne(personne);
            else
                Console.WriteLine("Aucune personne trouvée dans ce pays.");
        }

        static void TrouverParTravail()
        {
            Console.Write("Entrez le travail: ");
            string travail = Console.ReadLine();
            var personnes = repository.GetByWork(travail);

            if (personnes.Any())
                foreach (var personne in personnes)
                    AfficherPersonne(personne);
            else
                Console.WriteLine("Aucune personne trouvée avec ce travail.");
        }

        static void AfficherToutesLesPersonnes()
        {
            var personnes = repository.GetAll();

            if (personnes.Any())
                foreach (var personne in personnes)
                    AfficherPersonne(personne);
            else
                Console.WriteLine("Aucune personne à afficher.");
        }

        static void AfficherPersonne(Personne personne)
        {
            Console.WriteLine($"\n\nID: {personne.Id}," +
                              $"\nPrénom: {personne.FistName}" +
                              $"\nNom: {personne.LastName}" +
                              $"\nÂge: {personne.Age}" +
                              $"\nAdresse: {personne.Address}" +
                              $"\nTravail: {personne.Work}" +
                              $"\nPays: {personne.Country}");
        }
        #endregion

    }
}

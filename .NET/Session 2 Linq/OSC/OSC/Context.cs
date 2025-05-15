using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC
{
    public class Context
    {
        public List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR", Salary = 55000, Experience = 3 },
            new Employee { Id = 2, Name = "Bob", Department = "IT", Salary = 75000, Experience = 5 },
            new Employee { Id = 3, Name = "Charlie", Department = "Finance", Salary = 62000, Experience = 4 },
            new Employee { Id = 4, Name = "David", Department = "IT", Salary = 80000, Experience = 6 },
            new Employee { Id = 5, Name = "Eva", Department = "HR", Salary = 58000, Experience = 2 },
            new Employee { Id = 6, Name = "Frank", Department = "Marketing", Salary = 70000, Experience = 5 },
            new Employee { Id = 7, Name = "Grace", Department = "Finance", Salary = 60000, Experience = 3 }
        };

        public List<Movie> movies = new List<Movie>
        {
            new Movie { id = 1, title = "Inception", description = "A thief who steals corporate secrets through dream-sharing technology.", directorId = 101 },
            new Movie { id = 2, title = "The Dark Knight", description = "Batman faces the Joker, a criminal mastermind.", directorId = 101 },
            new Movie { id = 3, title = "Interstellar", description = "A team travels through a wormhole to find a new home for humanity.", directorId = 101 },
            new Movie { id = 4, title = "Pulp Fiction", description = "The lives of two hitmen and others intertwine in LA.", directorId = 102 },
            new Movie { id = 5, title = "Django Unchained", description = "A freed slave sets out to rescue his wife from a brutal plantation owner.", directorId = 102 },
            new Movie { id = 6, title = "The Grand Budapest Hotel", description = "A quirky concierge teams up with a lobby boy to prove his innocence.", directorId = 103 },
            new Movie { id = 7, title = "Tenet", description = "A secret agent manipulates the flow of time to prevent World War III.", directorId = 101 },
            new Movie { id = 8, title = "Kill Bill: Vol. 1", description = "A former assassin seeks revenge on her ex-colleagues.", directorId = 102 }
        };


        public List<Director> directors = new List<Director>
        {
            new Director { id = 101, name = "Christopher Nolan", nationalityId = 1 },
            new Director { id = 102, name = "Quentin Tarantino", nationalityId = 2 },
            new Director { id = 103, name = "Wes Anderson", nationalityId = 1 }
        };

        public List<Nationality> nationalities = new List<Nationality>
        {
            new Nationality { id = 1, name = "British" },
            new Nationality { id = 2, name = "American" }
        };
    }   
}

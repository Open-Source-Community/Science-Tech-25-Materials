using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
namespace OSC
{
    class Program
    {
        public static void Main(string[] args)
        {

            Context context = new Context();

            #region AnonymousTypes
            //Employee emp = new Employee() { Id = 1, Name = "Mahmoud", Department = "IT", Salary = 2000, Experience = 5 };

            //Console.WriteLine(emp.GetType().Name);
            //var employee = new { Id = 1, Name = "Mahmoud", Department = "IT", Salary = 2000, Experience = 5 };


            //var employee1 = new {   Id = 1 , Name = "Mahmoud1", Department = "IT", Salary = 2000, Experience = 5 };

            //Console.WriteLine(employee.GetType().Name);
            //Console.WriteLine(employee1.GetType().FullName);

            #endregion

            #region Linq

            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //foreach (int number in numbers)
            //{
            //    if (number % 2 == 0)
            //        Console.WriteLine(number);
            //}

            ////In Linq
            //var result = numbers.Where(i => i > 6);

            ////foreach (int number in result)
            ////{
            ////    Console.WriteLine(number);
            ////}

            //#endregion

            //#region DefferedExecution

            //numbers.Add(13);
            ////foreach (var i in result)
            ////{
            //    Console.WriteLine(result);
            ////}

            #endregion

            #region Where

            ////Select Employees in IT Department

            //var result = context.employees.Where(e => e.Department == "Marketing");
            //Console.WriteLine(result);

            #endregion

            #region Query Syntax
            //var result = from i in context.employees
            //             where i.Department == "IT"
            //             select i;

            #endregion

            #region Select

            ////Select Names And Departments of the first 5 employees

            //var result = context.employees.Where(e => e.Id <= 5).Select(e => new { e.Name, e.Department });
            //foreach (var e in result)
            //{
            //    Console.WriteLine(e);
            //}

            #endregion

            #region Distinct
            //var result = context.employees.Distinct();

            ////Get Employees With Distinct Experiences
            //result = context.employees.DistinctBy(e => e.Experience).ToList();

            //foreach (var i in result)
            //{
            //    Console.WriteLine(i.Name);
            //}
            #endregion

            #region OrderBy

            ////Select Emplyoees Orderd Desc By Their Salaries

            //var result = context.employees.OrderByDescending(e => e.Salary).ThenBy(e=>e.Experience);

            #endregion

            #region Single Element Operators - Imidiate Execution

            ////Select First Employee with Salary < 70000

            //var result = context.employees.First(e => e.Salary > 7000000);

            ////Console.WriteLine(result);

            //result = context.employees.Last(e => e.Name == "Bob");

            //var result = context.employees.FirstOrDefault(e => e.Salary > 1000000);

            //var result = context.employees.SingleOrDefault(e => e.Name == "Bob");

            #endregion

            #region Aggregate - Imidiate Execution
            //Count, Max, Min, Sum...

            ////Select Employee With The Most Expreience
            //var result = context.employees.Max(x => x.Salary);

            ////Select How Many Employees
            //result = context.employees.Count();

            ////Select Sum of Salaries of The First 5 Employees
            //result = context.employees.Where(e => e.Id <= 5).Sum(e => e.Salary);

            #endregion

            #region Take/Skip
            ////Select the First 4 Employees
            //var result = context.employees.Take(4);

            ////Select The Employees Without the least 3 salaries
            //result = context.employees.OrderBy(e => e.Salary).Skip(3);

            #endregion

            #region Generator Operators
            //Range 
            //var result = Enumerable.Range(1, 10);

            //foreach (var i in result)
            //{
            //    Console.WriteLine(i);
            //}



            ////Empty
            ////var result1 = Enumerable.Empty<string>();

            ////Repeat
            //var result2 = Enumerable.Repeat(1, 4);

            #endregion

            #region Select Many

            //var result = context.employees.SelectMany(e => e.Name.Split(' '));

            //var result1 = context.employees.SelectMany(e => e.Name.ToCharArray());

            #endregion

            #region Casting Operators - Imidiate Execution

            //var result = context.employees.Where(e => e.Salary > 70000).ToList();

            //var result1 = context.employees.Where(e => e.Salary > 70000).ToArray();

            //var result2 = context.employees.Where(e => e.Salary > 70000).ToDictionary(e => e.Id);

            #endregion

            #region Quantifiers
            //var result = context.employees.Any();

            ////Return True if All Employees With Salary < 100000


            //result = context.employees.All(e => e.Salary < 100000);


            #endregion

            #region Group By
            ////Get Count of the Departments That Have 2 Employees or More

            //var result = context.employees.GroupBy(e => e.Department).ToList(); //e is a Group of Employees
            //foreach (var item in result)
            //{
            //    foreach (var item2 in item)
            //    {
            //        Console.WriteLine(item2.Name);
            //    }
            //}

            #endregion

            #region Inner Join
            ////Select Movie Title With Director Name

            //var MovieWithDirector = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new
            //    {
            //        MovieTitle = m.title,
            //        DirectorName = d.name,
            //        NationalityId = d.nationalityId
            //    });

            //foreach (var item in MovieWithDirector)
            //{
            //    Console.WriteLine(item);
            //}

            //////Select Movie Title With Director Name and Add Nationality of Director
            //var result = MovieWithDirector
            //    .Join(
            //    context.nationalities,
            //    m => m.NationalityId,
            //    n => n.id,
            //    (m, n) => new
            //    {
            //        m.MovieTitle,
            //        m.DirectorName,
            //        Nationality = n.name
            //    });

            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            #endregion

            #region Left Join

            //var result = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new
            //    {
            //        MovieTitle = m.title,
            //        DirectorName = d.name,
            //        NationalityId = d.nationalityId,
            //    }).GroupJoin(
            //    context.nationalities,
            //    b => b.NationalityId,
            //    n => n.id,
            //    (b, n) => new
            //    {
            //        Movie = b,
            //        Nationality = n
            //    })
            //    .SelectMany(b => b.Nationality.DefaultIfEmpty(),
            //    (b, n) => new
            //    {
            //        b.Movie,
            //        Nationality = n
            //    });

            //foreach (var b in result)
            //{
            //    Console.WriteLine(b.Book+ " " + b.Nationality.name );
            //}

            #endregion

            #region Examples

            //1-Get a list of all movie titles that contain the word" The " and sort them alphabetically.
                

            //2-Retrieve a list of movies with a title length greater than 10 characters, and project only the title and ID.

            //3-Find the first movie(alphabetically) whose title starts with the letter "I".

            //4-Join the movies with their respective directors and return a list of anonymous objects with movie title and director name.

            //5-Find all movies directed by British directors by joining the Movie, Director, and Nationality collections.

            //6-Group movies by director and list each director's name along with the number of movies they directed.

            //7-Group all movies by their director’s nationality and calculate how many movies each nationality produced.

            //8-Calculate the average movie ID for each director (group by director name, then average their movie IDs).

            //9-Aggregate all movie titles into a single comma - separated string(use Aggregate).

            //10-Skip the first 2 mov+ies(ordered by title) and take the next 3.Return only their titles.

            //11-Get the top 2 directors(by number of movies directed) and list their names and counts.

            //12-Take all movies except those directed by Quentin Tarantino and return their titles.

            //13-Check if there are any movies directed by Wes Anderson.

            //14-Find the single movie(using Single) with the title "Inception" and throw an error if not exactly one match.

            //15-Use GroupJoin to produce a list of directors, each with their corresponding list of movie titles.

            //16-Create a list of all combinations of directors and movie titles using SelectMany.

            //17-Join all movies with their directors and nationalities and return an object with: Title, DirectorName, NationalityName.

            //18-Count the total number of unique directors who have made at least one movie.

            //19-Create a dictionary of movie ID as key and "Title by DirectorName" as value.

            #endregion

            #region Solutions

            ////Q-1
            //var titles = context.movies
            //    .Where(m => m.title.Contains("The"))
            //    .OrderBy(m => m.title)
            //    .Select(m => m.title);

            ////Q-2
            //var longTitles = context.movies
            //    .Where(m => m.title.Length > 10)
            //    .Select(m => new { m.title, m.id });

            //Q-3
            //var firstWithI = context.movies
            //    .Where(m => m.title.StartsWith("I"))
            //    .OrderBy(m => m.title)
            //    .FirstOrDefault();

            ////Q-4
            //var movieDetails = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new 
            //    {
            //        m.title,
            //        DirectorName = d.name
            //    });

            ////Q-5
            //var britishMovies = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new
            //    {
            //    m, d
            //    })
            //    .Join(
            //    context.nationalities,
            //    md => md.d.nationalityId,
            //    n => n.id,
            //    (md, n) => new
            //    {
            //    md.m,
            //    md.d,
            //    n
            //    })
            //    .Where(x => x.n.name == "British")
            //    .Select(x => new { x.m.title, x.d.name, Nationality = x.n.name });

            ////Q-6
            //var directorMovieCounts = context.movies
            //    .GroupBy(m => m.directorId)
            //    .Join(
            //    context.directors,
            //    g => g.Key,
            //    d => d.id,
            //    (g, d) => new
            //    {
            //        Director = d.name,
            //        MovieCount = g.Count()
            //    });

            ////Q-7
            //var nationalityMovieCount = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new 
            //    {
            //        m, d 
            //    })
            //    .Join(
            //    context.nationalities,
            //    md => md.d.nationalityId,
            //    n => n.id,
            //    (md, n) => new 
            //    {
            //        md.m, n 
            //    })
            //    .GroupBy(x => x.n.name)
            //    .Select(g => new { Nationality = g.Key, MovieCount = g.Count() });

            //Q-8
            //var directorAvgMovieId = context.movies
            //    .GroupBy(m => m.directorId)
            //    .Select(g => new
            //    {
            //        DirectorId = g.Key,
            //        AverageId = g.Average(m => m.id)
            //    })
            //    .Join(
            //    context.directors,
            //    g => g.DirectorId,
            //    d => d.id, (g, d) => new 
            //    {
            //        Director = d.name,
            //        g.AverageId 
            //    });

            ////Q-9
            //var allTitles = context.movies
            //.Select(m => m.title)
            //.Aggregate((a, b) => a + ", " + b);

            ////Q-10
            //var pagedTitles = context.movies
            //    .OrderBy(m => m.title)
            //    .Skip(2)
            //    .Take(3)
            //    .Select(m => m.title);

            ////Q-11
            //var topDirectors = context.movies
            //    .GroupBy(m => m.directorId)
            //    .Select(g => new { directorId = g.Key, movieCount = g.Count() })
            //    .OrderByDescending(g => g.movieCount)
            //    .Take(2)
            //    .Join(
            //    context.directors,
            //    g => g.directorId,
            //    d => d.id,
            //    (g, d) => new 
            //    {
            //        director = d.name,
            //        g.movieCount 
            //    });

            ////Q-12
            //var nonTarantinoMovies = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new 
            //    { 
            //        m, d 
            //    })
            //    .Where(md => md.d.name != "Quentin Tarantino")
            //    .Select(md => md.m.title);

            ////Q-13
            //var hasWesAndersonMovies = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new 
            //    {
            //        m, d 
            //    })
            //    .Any(md => md.d.name == "Wes Anderson");

            ////Q-14
            //var inceptionMovie = context.movies.Single(m => m.title == "Inception");

            ////Q-15
            //var directorMovieList = context.directors
            //    .GroupJoin(
            //    context.movies,
            //    d => d.id,
            //    m => m.directorId,
            //    (d, ms) => new 
            //    { 
            //        director = d.name,
            //        movies = ms.Select(m => m.title) 
            //    });

            ////Q-16
            //var directorMovieCombinations = context.directors
            //    .SelectMany(d => context.movies
            //    .Where(m => m.directorId == d.id)
            //    .Select(m => new { director = d.name, movieTitle = m.title }));

            ////Q-17
            //var movieDetails = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id,
            //    (m, d) => new 
            //    {
            //        m,
            //        d 
            //    })
            //    .Join(
            //    context.nationalities,
            //    md => md.d.nationalityId,
            //    n => n.id, 
            //    (md, n) => new 
            //    {
            //        md.m,
            //        md.d, 
            //        n 
            //    })
            //    .Select(x => new { x.m.title, directorName = x.d.name, nationalityName = x.n.name });

            ////Q-18
            //var uniqueDirectorsCount = context.movies
            //    .Select(m => m.directorId)
            //    .Distinct()
            //    .Count();

            ////Q-19
            //var movieDictionary = context.movies
            //    .Join(
            //    context.directors,
            //    m => m.directorId,
            //    d => d.id, (m, d) => new 
            //    {
            //        m.id,
            //        m.title,
            //        directorName = d.name 
            //    })
            //    .ToDictionary(x => x.id, x => $"{x.title} by {x.directorName}");
            #endregion
        }
    }
}

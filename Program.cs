using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYouColter
{
    class Program
    {
        // SQL Database for this project = DatabaseForRhythmsGonnaGetYouColter
        class AlbumItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool IsExplicit { get; set; }
            public DateTime ReleaseDate { get; set; }
            public int BandId { get; set; }
        }

        class BandItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CountryOfOrigin { get; set; }
            public int NumberOfMembers { get; set; }
            public string Web { get; set; }
            public string Style { get; set; }
            public bool IsSigned { get; set; }
            public string ContactName { get; set; }
            public string ContactPhone { get; set; }
        }

        class BandDatabaseContext : DbContext
        {
            public DbSet<BandItem> Band { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("server=localhost;database=DatabaseForRhythmsGonnaGetYouColter");
            }
        }

        class AlbumDatabaseContext : DbContext
        {
            public DbSet<AlbumItem> Album { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("server=localhost;database=DatabaseForRhythmsGonnaGetYouColter");
            }
        }
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 0 as your answer.");
                return 0;
            }
        }

        static DateTime PromptForDateTime(string prompt)
        {
            Console.Write(prompt);
            var userInputAsString = Console.ReadLine();
            DateTime userInputAsDateTime = DateTime.Parse(userInputAsString);
            var isThisGoodInput = DateTime.TryParse(userInputAsString, out userInputAsDateTime);

            if (isThisGoodInput)
            {
                return userInputAsDateTime;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using 1/1/0001 as your answer.");
                return new DateTime(1, 1, 1);
            }

        }

        static bool PromptForBool(string prompt)
        {
            Console.Write(prompt);
            var userInputAsString = Console.ReadLine();
            bool userInputAsBool = bool.Parse(userInputAsString);
            var isThisGoodInput = bool.TryParse(userInputAsString, out userInputAsBool);

            if (isThisGoodInput)
            {
                return userInputAsBool;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input. I'm using true as your answer.");
                return true;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome to my record label's interactive client catalogue. Please select one of the following options:");

            var userHasChosenToQuit = false;

            while (userHasChosenToQuit == false)
            {
                Console.WriteLine();
                Console.WriteLine("Choose:");
                Console.WriteLine("(A)dd a new band");
                Console.WriteLine("(V)iew all the bands");
                Console.WriteLine("A(d)d an album for a band");
                Console.WriteLine("(L)et a band go");
                Console.WriteLine("(R)esign a band");
                Console.WriteLine("(E)nter a band's name to view all its albums");
                Console.WriteLine("V(i)ew all albums ordered by release date");
                Console.WriteLine("Vie(w) all bands that are signed");
                Console.WriteLine("View all (b)ands that are not signed");
                Console.WriteLine();

                var choice = PromptForString("Choice: ");
                Console.WriteLine();

                if (choice == "Q")
                {
                    userHasChosenToQuit = true;
                }

                if (choice == "A")
                {
                    var context = new BandDatabaseContext();

                    var newName = PromptForString("Name: ");
                    var newCountryofOrigin = PromptForString("Country of Origin: ");
                    var newNumberOfMembers = PromptForInteger("Number of Members: ");
                    var newWeb = PromptForString("Email Address: ");
                    var newStyle = PromptForString("Musical Style: ");
                    var newIsSigned = PromptForBool("Is the band signed? (Answer as true or false): ");
                    var newContactName = PromptForString("Name of Band Contact: ");
                    var newContactPhone = PromptForString("Band Contact Phone Number: ");

                    var newBandItem = new BandItem
                    {
                        Name = newName,
                        CountryOfOrigin = newCountryofOrigin,
                        NumberOfMembers = newNumberOfMembers,
                        Web = newWeb,
                        Style = newStyle,
                        IsSigned = newIsSigned,
                        ContactName = newContactName,
                        ContactPhone = newContactPhone,
                    };

                    context.Band.Add(newBandItem);
                    context.SaveChanges();

                }

                if (choice == "V")
                {
                    var context = new BandDatabaseContext();
                    var allTheBands = context.Band;

                    foreach (var band in allTheBands)

                    {
                        Console.WriteLine($"Band ID Number: {band.Id}, Name: {band.Name}, Country of Origin: {band.CountryOfOrigin}, Number of Members: {band.NumberOfMembers}, Email: {band.Web}, Signed?: {band.IsSigned}, Contact Name: {band.ContactName}, Contact Phone Number: {band.ContactPhone}");
                    }
                }

                if (choice == "d")
                {
                    var context = new AlbumDatabaseContext();

                    var newTitle = PromptForString("Title: ");
                    var newIsExplicit = PromptForBool("Is the album explicit? (Answer as true or false): ");
                    var newReleaseDate = PromptForDateTime("What is the Album's release date in the format of MM/DD/YYYY?: ");
                    var newBandId = PromptForInteger("What is the ID number for the band that recorded this album: ");

                    var newAlbumItem = new AlbumItem
                    {
                        Title = newTitle,
                        IsExplicit = newIsExplicit,
                        ReleaseDate = newReleaseDate,
                        BandId = newBandId
                    };

                    context.Album.Add(newAlbumItem);
                    context.SaveChanges();
                }

                if (choice == "L")
                {
                    var context = new BandDatabaseContext();
                    var bandWithNewSignedStatus = PromptForString("Which band would you like to select? ");
                    var retrieveBandItem = context.Band.FirstOrDefault(entry => entry.Name == $"{bandWithNewSignedStatus}");

                    if (retrieveBandItem == null)
                    {
                        Console.WriteLine("Sorry, that band does not exist in this database.");
                    }
                    else
                    {
                        retrieveBandItem.IsSigned = false;

                        context.SaveChanges();
                    }
                }

                if (choice == "R")
                {
                    var context = new BandDatabaseContext();
                    var bandWithNewSignedStatus = PromptForString("Which band would you like to select? ");
                    var retrieveBandItem = context.Band.FirstOrDefault(entry => entry.Name == $"{bandWithNewSignedStatus}");

                    if (retrieveBandItem == null)
                    {
                        Console.WriteLine("Sorry, that band does not exist in this database.");
                    }
                    else
                    {
                        retrieveBandItem.IsSigned = true;

                        context.SaveChanges();
                    }

                }

                if (choice == "E")
                {
                    var bandContext = new BandDatabaseContext();
                    var albumContext = new AlbumDatabaseContext();
                    var bandToSearchFor = PromptForString("Enter the name of the band you'd like to search for? ");
                    var retrieveBandItem = bandContext.Band.FirstOrDefault(entry => entry.Name == $"{bandToSearchFor}");

                    if (retrieveBandItem == null)
                    {
                        Console.WriteLine("Sorry, that band does not exist in this database.");
                    }
                    else
                    {
                        var findAlbums = albumContext.Album.Where(entry => entry.BandId == retrieveBandItem.Id);

                        foreach (var album in findAlbums)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Title: {album.Title}, Is Explicit?: {album.IsExplicit}, Release Date: {album.ReleaseDate}");
                        }

                    }

                }
                if (choice == "i")
                {
                    var context = new AlbumDatabaseContext();
                    var allTheAlbums = context.Album.OrderBy(entry => entry.ReleaseDate);

                    foreach (var album in allTheAlbums)

                    {
                        Console.WriteLine($"Album Title: {album.Title}, Release Date: {album.ReleaseDate}");
                    }
                }
                if (choice == "w")
                {
                    var context = new BandDatabaseContext();
                    var allTheBands = context.Band.Where(band => band.IsSigned == true);

                    foreach (var band in allTheBands)

                    {
                        Console.WriteLine(band.Name);
                    }
                }

                if (choice == "b")
                {
                    var context = new BandDatabaseContext();
                    var allTheBands = context.Band.Where(band => band.IsSigned == false);

                    foreach (var band in allTheBands)

                    {
                        Console.WriteLine(band.Name);
                    }
                }
            }
        }
    }
}
# RhythmsGonnaGetYouColter
# Rhythm's gonna get you

For this project, we will model and create a database. We are starting a record
label company, and we a place to store our bands, albums, and eventually songs.
You are creating a console app that stores our information in a database.

## Objectives

- Practice working with SQL
- Practice working with ORMs (EF Core)

## Setup

Create a console that allows a user to store and manage the company's bands,
albums, and (eventually) songs.

## Top Tips

Although in reality an album could be done by more than one band, our system
will just have an album involving **one** band. That is, an album belongs to one
band.

### Explorer Mode

- [ ] Create a database that stores `Albums`, and `Bands`. They should have the following properties, use your best judgment for types. (We will add foreign keys in the next step)

  - [ ] Album
    - Id
    - Title
    - IsExplicit
    - ReleaseDate
  - [ ] Band

    - Id
    - Name
    - CountryOfOrigin
    - NumberOfMembers
    - Website
    - Style
    - IsSigned
    - ContactName
    - ContactPhoneNumber

  - Add foreign keys to fulfill the following

    - [ ] One Band has many Albums

  - [ ] Create an interface to let the user:

    - [ ] Add a new band
    - [ ] View all the bands
    - [ ] Add an album for a band
    - [ ] Let a band go (update isSigned to false)
    - [ ] Resign a band (update isSigned to true)
    - [ ] Prompt for a band name and view all their albums
    - [ ] View all albums ordered by ReleaseDate
    - [ ] View all bands that are signed
    - [ ] View all bands that are not signed

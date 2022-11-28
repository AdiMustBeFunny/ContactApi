## Simple application where you can add new contacts and modify them

![ContactApi](assets/1.png)

## Setup

* Open project in Microsoft Visual Studio 2022 
* In ContactApi project open appsettings.json
    * Fill in connection to the database in DefaultConnection
    * Fill in secret for JWT (some random characters like: asdanviuhxcasdasdasdasdasdasdasdv - it has to be at least 128 bit long)
* Open Package Manager Console
    * Set Default project to Database
    * Run update-database
* Set multiple startup projects: 
    * 1st - ContactApi 
    * 2nd - ContactUI
* Run the app
* Default user credential needed to log into the app 
    * Username: test 
    * Password: Test.123
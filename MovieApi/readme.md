# Task Requirements

- ✅ You should solve this problem using either .NET framework, .NET Core 3.0 or latest .NET framework. 
- ✅ You should provide any required instructions to get the API up and running. 
- ✅ Please upload the solution to a public GitHub repo. 
- ✅ Your request and response formats MUST match the examples below
- ✅ Three endpoints
	1.	POST /metadata
		- ✅ Saves a new piece of metadata
	2.	GET /metadata/:movieId
		- ✅ Returns all metadata for a given movie. 
		- ✅ Only the latest piece of metadata (highest Id) should be returned where there are multiple metadata records for a given language.
		- ✅ Only metadata with all data fields present should be returned, otherwise it should be considered invalid.
		- ✅ If no metadata has been POSTed for a specified movie, a 404 should be returned.
		- ✅ Results are ordered alphabetically by language.
	3.	GET /movies/stats
		- ✅ Returns the viewing statistics for all movies.
		- ✅ The movies are ordered by most watched.
		- ✅ The data returned only needs to contain information from the supplied csv documents and does not need to return data provided by the POST metadata endpoint.

# To Run Project

- Build and run using visual studio
- 

# Folder/Project Structure

- BL (Business Logic):
	- **Contracts**: Interfaces defining the operations that can be performed by our movie-related services.
	- **Managers**: Implementation of the contracts, where the core business logic resides.

- DAL (Data Access Layer):
	- **Contracts**: Contains interfaces for data access operations.
	- **Repositories**: Implements the contracts, providing the logic to interact with the data source.
	- **metadata.csv** and **stats.csv**: CSV files that serve as the database sources for the project.

- Controllers:
	- Movies Controller: The controller that handles HTTP requests related to movies and directs them to the appropriate business logic services.

- Shared:
	- Dtos (Data Transfer Objects): Defines objects for transferring data between processes.
	- Helpers: Contains utility classes like CsvHelper.cs for common tasks across the project.
	- Models: Classes representing the data we work with, such as Movie, MovieStats, and Stats.

# What could be improved

- Data validations to handle complex scenarios
- Error handling
- Logging
- Add Unit Tests
- Separate BL,DAL,Shared in separate projects
- retrieve the file paths of the excel from appsettings.json instead of hardcoding them in startup.js

# BookManagement

Project created to be maintainable  reusable, testable ,scalable , DRY principle , union architecture , auto mapper used for all

## EndPoints
  #### Accounts
	- Accounts/register
	- Accounts/Login
	- Accounts/user authorized

#### Books
	- Books  [ you can make any filter , sort , Search in this Endpoint]
	- Books/add
	- Books/Update
	- Books/Delete
	- Books/CountOfBooksPerGenre
	- Books/popular
	- Books/totalReviews
#### Rating
	- Ratings/add


## Project Structure
## consist of 4 layers
####	1 - API Layer or called { Presentation Layer} 	 Authorization Applied
		- Configurations
		- Helpers [ MappingProfile , Response class]
		- Controllers
		- DTOs
    		- Extensions for some methods
    		- Helpers
        		- MappingProfile AutomMapper
####	2 - Domain Layer 
		- Entities  [ SQL Server Provider ]
    		- ISpecification Pattern
   		- IUnitOfWork
    		- Enums
###	3 - Repository Layer or called 'Infrastructure'
		- packages 
    		- GenericRepository Interfaces and Implementations
    		- Specifications Implementation of ISpecification at Domain
    		- Unit Of work
		- Config
			- To apply constraints of Entity at Domain using fluent API instead of Data Annotation [Code Style ]
   		- Data
     			- DBcontext
			- Data Seeding [ roles , Admin User , and genres]
		- Migrations
		- Implementation to Access Data Source
###	4 - Service Layer 
		- Business logic on data before sending to repository
    		- Book Service [ Interface and Implementation ]
    		- Token Service [ Interface and implementation  ]


###  Execute  update-database automatically without make it manually [ manually => 'update-database' ]  
### AutoMapper Used
### Generic Repository pattern ,  Specification pattern 
### Clean Code
### unit Test - XUnit package  ,  InMemory package for testing

### If I find other ideas, I will write them  here.
### Thanks  😊  
	

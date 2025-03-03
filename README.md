# SISONKE_Invoicing_System

## Description
An Invoicing system that provides an intuitive and user-friendly interface that enables users to create professional invoices quickly, manage client information, and track payments related to tech products. By leveraging modern technology, the system will enhance productivity, reduce manual errors, and ensure timely payments, thereby improving cash flow and customer satisfaction.

Customer  mode - View invoices,  make payment, and print invoices related to tech products

Admin Employee mode - Manage all invoicing operations including reading, writing, editing, and removing invoices

General Employee mode -  Manage invoicing operations including reading and writing invoices


Refer to the [Project Documentation](/SISONKE_Invoicing_Group_Project.xlsx) for a detailed explanation 

## Tools
![Static Badge](https://img.shields.io/badge/Visual%20Studio-2022%20or%20later-green) ![Static Badge](https://img.shields.io/badge/.Net%20Framework-6.0-blue) ![Static Badge](https://img.shields.io/badge/MSSQL-v18%20or%20later-red)

## Installation
The ASP.NET MVC Web App project depends on the Entity Framework (EF) Code 1st and Rest API projects to run effectively. Hence, the EF Code 1st project should be run first, and then add migrations and then update the database. This will create the database for the entities involved in the system. After the initial process the Rest API Project needs to be opened and run the migration commands in package manager console making sure to state the context to add migration to and making sure to database is updated with the migrations so that the identity database tables are created in order to enable registration and login functionality and to be able to access critical data. The ASP.NET MVC front end needs to have api url so that it can access its endpoints. Make sure to keep the Rest API running and open the Angular Web app now to use it.


### Steps

1. Go to the documents folder and then clone the project there (you can use git bash or any cmd):
```
cd Documents
```
```
git clone https://github.com/LuckyMaley/Sisonke-Invoicing-System.git
```

2. Open [EF Code 1st project](/SISONKE_Invoicing_System_EFCODE1ST) on Visual Studio, and navigate to app.config in order to update the connection string
```
<connectionStrings>
  <add name="Model1" connectionString="data source="Enter yor db source";initial catalog=eCommerce_EFDB;integrated security=True;encrypt=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>
```
**Note**: Your Connection String might differ depending on whether you have a password or using a local version without a password.

3. Once the connection string is updated, go to the toolbar and look for tools >> Nuget Package Manager >> Package Manager Console and type below.
```
update-database
```
**Note**: This will populate the MSSQL database with the models. 

> In the case you update any of the models and you want to update the database again just do as follows on the Package Manager Console:
> ```
> add-migration 'Give the migration a name'
> ```
> Then press enter and once it has been updated, update the database:
> ```
> update-database
> ```

4. Now that the database is sorted, you can close this project.

5. Open up the [Rest Api project](/SISONKE_Invoicing_RESTAPI) on Visual Studio to configure it.

6. Go to appsettings.json file and configure the connection string for the two databases, one is for authorization and the other is for crud operations, see below.
```
"ConnectionStrings": {
  "IdentityConnection": "Server='your db source'; Database=SISONKE_Invoicing_System_IDENTITYDB; Trusted_Connection=True; MultipleActiveResultSets=True;",
  "CRUDConnection": "Server='your db source'; Database=SISONKE_Invoicing_System_EFDB; Trusted_Connection=True; MultipleActiveResultSets=True;"
}
```

7. Once the connection string is updated, go to the toolbar and look for tools >> Nuget Package Manager >> Package Manager Console and type below.
```
update-database -Context AuthenticationContext
```
Then press enter and once it has been updated, update the database:
```
update-database -Context SISONKE_Invoicing_System_EFDBContext
```
**Note**: This will populate the MSSQL database with the models from both contexts. 

> In the case you update any of the models and you want to update the database again just do as follows on the Package Manager Console:
> ```
> add-migration -Context 'Contextname' 'Give the migration a name'
> ```
> Then press enter and once it has been updated, update the database:
> ```
> update-database -Context 'name context you updated'
> ```
8. Run the application by clicking the green play button, if you are having an issue running just click the dropdown button next to the play button and run the api using IIS express.

9. Open the [ASP.NET MVC front end web application](/SISONKE_Invoicing_ASPNET) using Visual Studio.

10. Go to appsettings.json file and change the property called 'ClientBaseUrl' to the url of the REST API and save your changes and run the front end application.

11. Run the application by clicking the green play button, if you are having an issue running just click the dropdown button next to the play button and run the api using IIS express.

12. Make sure to go to the [Rest Api project](/SISONKE_Invoicing_RESTAPI) and open the program.cs file and make sure that the front end url is allowed by CORS. If it's not there then add it.
```
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigin",
		builder =>
		{
			builder.WithOrigins("add the front end url here")
								.AllowAnyHeader()
								.AllowAnyMethod();
		});

});
```

13. Save your changes and run the Rest API again. Now you can go to the running front end and browse freely.

**Note**: The Rest API must always be running inorder to be able to properly use the front-end application.

## Sample data for User Login	
```	
	               Username	        Password	         Role
Customer	       Efronz	          Efron@123456	     Customer 
Employee	       Jkhalid          Khalidj@1234	     Employee
Administrator	   ZzimelaAdmin	    zimelaZ@1234	     Administrator
```

## Visuals

![Screenshot 2025-03-03 143822](https://github.com/user-attachments/assets/4b4508d7-9521-4578-9384-0e4a25b5afd8)

![Screenshot 2025-03-03 143857](https://github.com/user-attachments/assets/e8e7b852-368d-41a5-a437-2002d40a9c9c)

![Screenshot 2025-03-03 144005](https://github.com/user-attachments/assets/062c6485-7389-4ed3-b6a3-89a3400ce037)

![Screenshot 2025-03-03 144021](https://github.com/user-attachments/assets/34d44bb8-6faa-4163-b714-ef438ca14403)

![Screenshot 2025-03-03 144044](https://github.com/user-attachments/assets/9b56987b-aa82-4740-95ad-319c8bb58a68)

![Screenshot 2025-03-03 144100](https://github.com/user-attachments/assets/f94214cf-18f1-4d3d-b927-ba6781d7835c)

![Screenshot 2025-03-03 144121](https://github.com/user-attachments/assets/fc393cd6-d77d-49d7-a960-d580cb84b326)

![Screenshot 2025-03-03 144135](https://github.com/user-attachments/assets/bd9092e1-19ab-49eb-9ed4-e88bcfec1750)

![Screenshot 2025-03-03 144159](https://github.com/user-attachments/assets/8ab691b0-219b-4a40-8b11-3dcce66d07bb)

![Screenshot 2025-03-03 144215](https://github.com/user-attachments/assets/8058a303-6165-4fea-acd3-6292df4a3631)

![Screenshot 2025-03-03 144239](https://github.com/user-attachments/assets/9128fc65-3f57-43d3-b287-8a1e5dc21546)

![Screenshot 2025-03-03 144258](https://github.com/user-attachments/assets/c1bdaa5a-e898-48c8-9a6a-8baa15cffed2)








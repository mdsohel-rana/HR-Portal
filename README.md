# HR-Portal
The HR Portal is a comprehensive web application designed to streamline and enhance the management of employee information within an organization. Developed using ASP.NET with C#, this application provides a user-friendly interface for HR professionals to access, manage, and update employee records efficiently.

Functional Specifications:

List Employees:
The application provides a List Employees function that retrieves and displays essential employee information, including:
Employee ID
First Name
Last Name
Division
Building
Title
Room

Search By Employee ID:
Users can quickly locate specific employee records using the Search By Employee ID feature, which utilizes the Employee ID as the primary key.

Update Employee Record:
The application allows users to update existing employee records. By searching for employees based on their names, HR professionals can modify details such as:
First Name
Last Name
Division
Building
Title
Room
This functionality is achieved through the UpdateEmployee method.

Delete Employee Record:
Users can efficiently remove employee records from the database using the DeleteEmployee function, ensuring that the system maintains accurate and up-to-date information.

Import XML Data:
The HR Portal includes a feature for importing employee data from an XML file. This data is processed and stored in the database using JDBC collections and is subsequently displayed in the user interface for easy access.

Technical Specifications:

Web Application:
The HR Portal is built as an ASP.NET web application utilizing C#, Web Forms, and the Oracle.DataAccess library for seamless interaction with an Oracle Database.

Design Pattern:
The application is designed using industry-standard design patterns to promote maintainability, scalability, and code reusability, leveraging the developer's knowledge and expertise in software architecture.

# Simple-Blog
It is a simple blog with anyone can post a blog[title, description], view, edit and delete option. It has two part, one is a Web API and other part is UI with Blazor Web Server and for DB it uses MSSQL.

# Objectives
Your task is to create a full-stack web application that includes a user interface (UI), a backend API, and an SQL database. The application should run locally on your machine, allowing you to demonstrate your skills in .NET development.

# Project Description
You will develop a simple blogging platform where users can create, read, update, and delete blog posts. Here’s what you need to include in your project:
  # 1. Frontend (UI)
    Build a blogging website. While we prefer you use Blazor, you are free to choose any other framework you are comfortable with (e.g., React, Angular, Vue.js). While you are free to choose UI framework, you should keep a strict focus on componentization.
    Implement the following features:
      ▪ A homepage that displays a list of blog posts.
      ▪ A form for creating new blog posts.
      ▪ Functionality to edit and delete existing posts.
      ▪ Each post should show its title, content, and the date it was created.
  # 2.Backend (API)
    o Create an ASP.NET Core RESTful Web API to handle CRUD operations for the 
      blog posts.
        o Implement endpoints for:
          ▪ Retrieving all blog posts.
          ▪ Retrieving a single blog post.
          ▪ Creating a new blog post.
          ▪ Updating an existing post.
          ▪ Deleting a post

  # 3.Database
    o Use an SQL database (e.g., SQL Server, SQLite) to store the blog post data.
    o Database schema is up to you to design
 # 4. Local Deployment
    o Ensure that your entire solution can run on localhost without any external dependencies.
    o Provide brief and clear instructions on how to set up and run the application locally.


# How to use this git

1. Pull this git to your local derive.
2. Create a Database "SimpleBlog" or anyone as you feel comfortable and change the connection from "appsettings.json" of API project named <b>"Simple-Blog"</b>
3. Now need to migrate DB, for this run these two command in <b>"Simple-Blog's"</b> Package Manager Console <br/>
     i. Add-Migration migrationName<br/>
     ii. Update-Database<br/>
4. Now the <b>"Simple-Blog's"</b> is ready to use.
5. Then you need to change the API connection from <b>Simple-BlogUI</b> appsettings.json "ApiConnections"
   

  


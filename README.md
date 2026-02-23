# AirBnB Camp – Backend

This is the backend API for the AirBnB Camp project. It provides user authentication, camp management, reviews, and database interactions using MySQL. The API is built with Node.js and Express, following RESTful architecture. It is designed to integrate with the AirBnB Camp frontend application.

## Features

- RESTful API design

- User registration and authentication

- JWT-based authentication

- Camp listing creation, retrieval, update, and deletion

- MySQL database integration using an ORM or query builder

- Middleware for validation and error handling

- Environment variable support

- Secure password hashing

## Tech Stack

- Node.js

- Express.js

- MySQL

- Sequelize / MySQL2 (depending on your implementation)

- JSON Web Tokens (JWT)

- Bcrypt

- Dotenv

- Nodemon (for development)

## Folder Structure (Typical)
AirBnB-Camp_backend/
│── src/
│   │── controllers/
│   │── routes/
│   │── models/
│   │── middleware/
│   │── config/
│   │── utils/
│   │── server.js
│── .env
│── package.json
│── README.md
## Getting Started
1. Clone the Repository
git clone https://github.com/Sneha-neupane/AirBnB-Camp_backend.git
2. Navigate to the Project Directory
cd AirBnB-Camp_backend
3. Install Dependencies
npm install
## Environment Variables

Create a .env file in the root directory and configure it according to your project.

Example .env file:
PORT=5000

DB_HOST=localhost
DB_USER=root
DB_PASSWORD=your_mysql_password
DB_NAME=airbnbcamp
DB_PORT=3306

JWT_SECRET=your_jwt_secret_key

Adjust names and values based on your database setup.

## MySQL Database Setup

Install MySQL on your system

Create a database:

CREATE DATABASE airbnbcamp;

Ensure your .env file contains correct MySQL credentials

Run migrations or allow Sequelize/ORM to auto-create tables

## Running the Server
Development Mode
npm run dev
Production Mode
npm start

The server will run at:

http://localhost:5000
API Endpoints (Example Overview)

You may adjust based on your actual project.

## Authentication
POST   /api/auth/register
POST   /api/auth/login
Camps
GET    /api/camps
GET    /api/camps/:id
POST   /api/camps
PATCH  /api/camps/:id
DELETE /api/camps/:id
Reviews (if included)
POST   /api/camps/:id/reviews
DELETE /api/reviews/:id
## Scripts

Inside package.json:

"scripts": {
  "start": "node src/server.js",
  "dev": "nodemon src/server.js"
}
## Deployment

You can deploy this backend on platforms such as:

Render

Railway

AWS

DigitalOcean

Ensure environment variables are added in the hosting platform.

## Related Repository

Frontend repository:

https://github.com/Sneha-neupane/AirBnB-Camp_frontend
## Contribution

Fork the repository

Create a new branch

Commit your changes

Open a pull request

## License

This project is licensed under the MIT License.

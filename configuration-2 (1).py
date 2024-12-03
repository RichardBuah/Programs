import mysql.connector

db = mysql.connector.connect(
    host = "localhost",
    user = "root",
    passwd = "Cre.100.dit"
)
mycursor = db.cursor()
mycursor.execute("CREATE DATABASE IF NOT EXISTS groceryShopDatabase")
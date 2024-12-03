# How to use my program. Save all files in one folder
        # Step 1
# Open the configuration.py file
# Create a root user with password Cre.100.dit in your Mysql workbench or 
# If you already have a user modify the configuration.py file to suit your credentials.
# run the configuraion.py to create the database called groceryShopDatabase.

        # Step 2
# open groceryDatabase.py file
# Look for the function called "connector" and make the neccessary changes to the root user
# credentials as you did for the configuration.py file
# Check the function called product_table() and change the first argument in make_product_list 
# which is the file location to your file directoryfor the products.csv file
# Run groceryDatabase.py file

        # Step 3
# Open the test_groceryDatabase.py file
# Check the function called test_product_table() and change the first argument in make_product_list() 
# which is the file location to your file directoryfor the products.csv file
# Run the test_groceryDatabase.py 
# Always run the groceryDatabase if you want to run the test file
# this is because the test_delete_record function will remove records the database permanentlyy
# hence if run without first running the groceryDatabase.py it would raise an error 

import mysql.connector
import csv

def main():
    try:
        customer_table()
        product_table()
        # UPDATING CUSTOMER TABLE
        initiat = 0
        ds = int(input("Do you want to add a new customer? if yes enter 1 else enter 0: "))
        if ds == 1:
            ds_num = int(input("Enter the number of customers you want to add: "))
            while initiat <= ds_num - 1:
                cname = input("Enter customer name: ").capitalize()
                cadd = input("Enter customer address: ")
                cnum = input("Enter customer number: ")
                Add_to_Customer_table(cname, cadd, cnum)
                initiat += 1
        elif ds == 0:
            print("no new customer")
        else:
            print("Invalid input")
        
        # DELETING Records from TABLES
        dt = int(input("Do you want to delete any table? yes '1' or no '0': "))
        if dt == 1:
            dname = int(input("Enter '1' for customer table or '2' for products table: "))
            record_handler(dname, delete_records)
        else:
            print("Okay. Done!")

        # SEARCHING through tables for records
        print("")
        sds = int(input("Do you want to run a quick search for client or product data? yes '1' or no '0': "))
        if sds == 1:
            dname = int(input("Enter '1' for customer table or '2' for products table: "))
            record_handler(dname, search_records)
            print("Done!")
        else:
            print("Okay. Done!")
        
        # Show All Available Data from Data Base
        print("")
        print("Showing all entries in groceryDatabase ... ")
        show_dataBase("Customers", "Products")
    except KeyError as keyrr:
        print(type(KeyError).__name__, keyrr, "not found")   
    except FileNotFoundError as fileNotFound:
        print(fileNotFound)
    except PermissionError as perm_err:
        print(perm_err)
    return 

def record_handler(dname, action):
    if dname == 1:
        drecord = input("Enter the exact customer name: ").capitalize()
        table = 'Customers'
        record = 'name'
        reference = drecord
        action(table, record, reference)
    elif dname == 2:
        drecord = input("Enter product ID: ")
        table = 'Products'
        record = 'prodID'
        reference = drecord
        action(table, record, reference)
    else:
        print("wrong selection")
    return

def connector():
    db = mysql.connector.connect(
    host = "localhost",
    user = "root",
    passwd = "Cre.100.dit",
    database = "groceryShopDatabase"
    )
    mycursor = db.cursor()
    mycursor.execute("CREATE DATABASE IF NOT EXISTS groceryShopDatabase")
    return db, mycursor

def Add_to_Customer_table(cname,cadd,cnum):
    db, mycursor = connector()
    sql = "INSERT INTO Customers (name, address, number) VALUES (%s, %s, %s)"
    val = (cname,cadd,cnum)
    mycursor.execute(sql,val)
    db.commit()
    print(f"Customer {cname} at {cadd} has been added.")
    return

def product_table():
    db, mycursor = connector()
    mycursor.execute("CREATE TABLE IF NOT EXISTS Products (prodID VARCHAR(255), product VARCHAR(255), price VARCHAR(255), PRIMARY KEY(prodID))")
    sql = "INSERT IGNORE INTO Products (prodID, product, price) VALUES (%s, %s, %s)"
    products = make_product_list("Team Activity\week07\products.csv", 0, 1, 2)
    mycursor.executemany(sql, products)
    db.commit()
    return

def customer_table():
    db, mycursor = connector()
    mycursor.execute("CREATE TABLE IF NOT EXISTS Customers (name VARCHAR(255), address VARCHAR(255), number VARCHAR(255), PRIMARY KEY(name))")

    sql = "INSERT IGNORE INTO Customers (name, address, number) VALUES (%s, %s, %s)"
    Initial_customers = [("Richard", "Accra, Ghana", "+233241564048"), ("Evans", "Cape Coast, Ghana", "+233545989040"), ("Peter", "Kumasi, Ghana", "+233556573751")]
    mycursor.executemany(sql, Initial_customers)
    db.commit()
    return

def delete_records(table, record, reference):
    db, mycursor = connector()
    sql = f"DELETE FROM {table} WHERE {record} = '{reference}'"
    mycursor.execute(sql)
    db.commit()
    prt = f'{record}: {reference} has been deleted.'
    print(f"{record}: {reference} has been deleted.")
    return prt

def search_records(table, record, reference):
    _, mycursor = connector()
    sql = f"SELECT * FROM {table} WHERE {record} LIKE '%{reference}%'"
    mycursor.execute(sql)
    search_results = mycursor.fetchall()

    for x in search_results:
        print(x)
    return

def show_dataBase(d1,d2):
    _,mycursor = connector()
    mycursor.execute(f"SELECT * FROM {d1}")
    table_results = mycursor.fetchall()
    for x in table_results:
        print(x)

    mycursor.execute(f"SELECT * FROM {d2}")
    table_result = mycursor.fetchall()
    for x in table_result:
        print(x)
    return


def make_product_list(filename, idIndex, nameIndex, priceIndex):
    with open(filename, "rt") as grocery_prod_file:
        reader = csv.reader(grocery_prod_file)
        next(reader)
        products = []

        for row in reader:
            if len(row) != 0:
                id = row[idIndex]
                name = row[nameIndex]
                price = row[priceIndex]
                products.append((id,name,price))
    return products

if __name__ == "__main__":
    main()
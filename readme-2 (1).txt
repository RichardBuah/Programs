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
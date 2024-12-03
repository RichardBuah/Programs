from groceryDatabase import make_product_list, delete_records
import pytest

def test_make_product_list():
    """ 
     The make product list returns a list that contains product ids, product names and its corresponding price. 
     """
    product_table_list = make_product_list("Team Activity\week07\products.csv", 0, 1, 2)

    # Chech for the list of items in the product table list
    assert product_table_list[0] == ('D150','1 gallon milk', '2.85')
    assert product_table_list[1] == ('D083', '1 cup yogurt', '0.75')
    assert product_table_list[2] == ('D215', '1 lb cheddar cheese', '3.35')
    assert product_table_list[3] == ('P019', 'iceberg lettuce', '1.15')
    assert product_table_list[4] == ('P020', 'green leaf lettuce', '1.79')
    assert product_table_list[5] == ('P021', 'butterhead lettuce', '1.83')
    assert product_table_list[6] == ('P025', '8 oz arugula', '2.19')
    assert product_table_list[7] == ('P143', '1 lb baby carrots', '1.39')
    assert product_table_list[8] == ('W231', '32 oz granola', '3.21')
    assert product_table_list[9] == ('W112', 'wheat bread', '2.55')
    assert product_table_list[10] == ('C013', 'twix candy bar', '0.85')
    assert product_table_list[11] == ('H001', '8 rolls toilet tissue', '6.45')
    assert product_table_list[12] == ('H014', 'facial tissue', '2.49')
    assert product_table_list[13] == ('H020', 'aluminum foil', '2.39')
    assert product_table_list[14] == ('H021', '12 oz dish soap', '3.19')
    assert product_table_list[15] == ('H025', 'toilet cleaner', '4.50')


def test_delete_record():
    """ Products and Customers Data"""
    assert delete_records("Products", "prodID", "D150") == "prodID: D150 has been deleted."
    assert delete_records("Products", "prodID", "D083") == "prodID: D083 has been deleted."
    assert delete_records("Products", "prodID", "H025") == "prodID: H025 has been deleted."
    assert delete_records("Products", "prodID", "H021") == "prodID: H021 has been deleted."
    assert delete_records("Products", "prodID", "H020") == "prodID: H020 has been deleted."

pytest.main(["-v", "--tb=line", "-rN", __file__])